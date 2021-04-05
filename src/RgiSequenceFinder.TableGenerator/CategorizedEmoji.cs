using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RgiSequenceFinder.TableGenerator
{
    //todo: GroupedEmoji の置き換えなので動作確認取れたら GroupedEmoji の方消す。

    public struct CategorizedEmoji
    {
        public List<(Keycap key, int index)> Keycaps { get; }
        public List<(RegionalIndicator code, int index)> RegionFlags { get; }
        public List<(TagSequence tag, int index)> TagFlags { get; }
        public int[] SkinTones { get; }

        // 1段目のインデックス = 文字数 (EmojiDataRow.EmojiString の方の長さ)
        // 2段目のインデックス = skin variation のタイプ(0: なし, 1: 1種, 2: 2種(通常), 3: 2種(👫 系特殊対応))
        public List<(ushort[] emoji, int index)>[,] Others { get; }

        public CategorizedEmoji(IEnumerable<EmojiDataRow> emojis)
        {
            var keycaps = Keycaps = new();
            var regionFlags = RegionFlags = new();
            var tagFlags = TagFlags = new();
            var skinTones = SkinTones = new int[5];
            var others = Others = new List<(ushort[] emoji, int index)>[4, 4];

            void addOther(EmojiString emoji, int skin, int index)
            {
                (others[emoji.Raw.Length - 1, skin] ??= new()).Add((emoji.Raw.ToArray(), index));
            }

            foreach (var row in emojis)
            {
                var index = row.Index;
                var emoji = GraphemeBreak.GetEmojiSequence(row.Utf16);

                if (emoji.LengthInUtf16 != row.Utf16.Length) throw new InvalidOperationException("ないはず");

                switch (emoji.Type)
                {
                    default:
                    case EmojiSequenceType.NotEmoji:
                        throw new InvalidOperationException("ないはず");
                    case EmojiSequenceType.Other:
                        if (row.VariantEmojiString.Raw.Length != 0)
                        {
                            addOther(row.EmojiString, 1, index);
                            addOther(row.VariantEmojiString, 3, index);
                        }
                        else
                        {
                            addOther(row.EmojiString, row.SkinVariation, index);
                        }
                        break;
                    case EmojiSequenceType.Keycap:
                        keycaps.Add((emoji.Keycap, index));
                        break;
                    case EmojiSequenceType.Flag:
                        regionFlags.Add((emoji.Region, index));
                        break;
                    case EmojiSequenceType.Tag:
                        tagFlags.Add((emoji.Tags, index));
                        break;
                    case EmojiSequenceType.SkinTone:
                        skinTones[(int)emoji.SkinTone - 1] = index;
                        break;
                }
            }
        }
    }
}
