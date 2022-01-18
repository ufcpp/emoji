using System;
using System.Collections.Generic;

namespace RgiSequenceFinder.TableGenerator
{
    public struct CategorizedEmoji
    {
        /// <summary>
        /// <see cref="EmojiSequenceType.Keycap"/> なやつ。
        /// </summary>
        public List<(Keycap key, int index)> Keycaps { get; }

        /// <summary>
        /// <see cref="EmojiSequenceType.Flag"/> なやつ。
        /// </summary>
        public List<(RegionalIndicator code, int index)> RegionFlags { get; }

        /// <summary>
        /// <see cref="EmojiSequenceType.Tag"/> なやつ。
        /// </summary>
        public List<(TagSequence tag, int index)> TagFlags { get; }

        /// <summary>
        /// <see cref="EmojiSequenceType.SkinTone"/> なやつ。
        /// </summary>
        public int[] SkinTones { get; }

        // 1段目のインデックス = 文字数 (EmojiDataRow.EmojiString の方の長さ)
        // 2段目のインデックス = skin variation のタイプ(0: なし, 1: 1種, 2: 2種(通常), 3: 2種(👫 系特殊対応))

        /// <summary>
        /// <see cref="EmojiSequenceType.Other"/> なやつのうち、 skin variation がないやつ。
        ///
        /// 文字数(<see cref="EmojiDataRow.EmojiString"/> の Length - 1 )で事前に仕分け。
        /// 最大長4のはず。
        /// </summary>
        public List<(ushort[] emoji, int index)>[] OtherNoSkin { get; }

        /// <summary>
        /// <see cref="EmojiSequenceType.Other"/> なやつのうち、 skin variation が skin tone 1個なやつ。
        ///
        /// <see cref="OtherNoSkin"/> 同様。
        /// 最大長2のはず。
        /// </summary>
        public List<(ushort[] emoji, int index)>[] OtherOneSkin { get; }

        /// <summary>
        /// <see cref="EmojiSequenceType.Other"/> なやつのうち、 skin variation が skin tone 2個なやつ。
        ///
        /// ただ、🧑‍🤝‍🧑 (1F9D1 200D 1F91D 200D 1F9D1)系のやつのみ。
        /// gender neutral なやつは1符号点の文字がないので、skin variation が素直に並んでる。
        ///
        /// Unicode 13.0 だと 🧑‍🤝‍🧑 しかないし、その結果 Length == 3 のやつしかない。
        /// 13.1 で 💑 couple with heart (Length == 3) と 💏 kiss (Length == 4) の geneder neutral 版が増えるはず。
        /// いったん new[4] で作って Length - 1 に格納するけど、0, 1 は null のままになるはず。
        /// </summary>
        public List<(ushort[] emoji, int index)>[] OtherTwoSkin { get; }

        /// <summary>
        /// <see cref="EmojiSequenceType.Other"/> なやつのうち、 skin variation が skin tone 2個なやつ。
        ///
        /// こっちは 👫 (1F46B)系のやつ。
        /// 元々1符号点でカップルを作ってしまったけども skin variation を足すにあたって 
        /// 1F469 200D 1F91D 200D 1F468 にした上でこれの両端に skin tone を付けるみたいにしたやつがいる。
        ///
        /// 両端が同性のやつ×5 と、異性のやつ×20 とかに分かれてる。
        /// 両端が同性のやつ(1F46B に直接 skin tone 1個付いたバージョン)は <see cref="OtherOneSkin"/> に入って、
        /// こっちは異性のやつだけ入る。
        ///
        /// 1F469 200D 1F91D 200D 1F468  の方を使って「skin tone 2個が異なる種類の場合の20種」だけが並んでて、
        /// それをこのリストに入れる。
        ///
        /// Unicode 13.0 だと 👫 系3文字(man woman, man man, woman woman)しかないし、その結果 Length == 3 のやつしかない。
        /// <see cref="OtherTwoSkin"/> と同じ持ち方。
        /// </summary>
        public List<(ushort[] emoji, int index)>[] OtherVarTwoSkin { get; }

        public CategorizedEmoji(IEnumerable<EmojiDataRow> emojis)
        {
            var keycaps = Keycaps = new();
            var regionFlags = RegionFlags = new();
            var tagFlags = TagFlags = new();
            var skinTones = SkinTones = new int[5];
            var otherNoSkin = OtherNoSkin = new List<(ushort[] emoji, int index)>[4];
            var otherOneSkin = OtherOneSkin = new List<(ushort[] emoji, int index)>[2];
            var otherTwoSkin = OtherTwoSkin = new List<(ushort[] emoji, int index)>[4];
            var otherVarTwoSkin = OtherVarTwoSkin = new List<(ushort[] emoji, int index)>[4];

            static void addOther(List<(ushort[] emoji, int index)>[] list, EmojiString emoji, int index)
            {
                (list[emoji.Raw.Length - 1] ??= new()).Add((emoji.Raw.ToArray(), index));
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
                            addOther(otherOneSkin, row.EmojiString, index);
                            addOther(otherVarTwoSkin, row.VariantEmojiString, index);
                        }
                        else
                        {
                            var list = row.SkinVariation switch
                            {
                                0 => otherNoSkin,
                                1 => otherOneSkin,
                                2 => otherTwoSkin,
                                _ => throw new InvalidOperationException("ないはず"),
                            };
                            addOther(list, row.EmojiString, index);
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
