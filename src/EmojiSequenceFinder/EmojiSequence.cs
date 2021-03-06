using System.Runtime.InteropServices;

namespace RgiSequenceFinder
{
    /// <summary>
    /// RGI 絵文字シーケンス判定の結果。
    /// </summary>
    /// <remarks>
    /// keycap と国旗は特殊なので別テーブル参照したく、単に文字列長だけじゃなくて <see cref="EmojiSequenceType"/> を一緒に返す。
    ///
    /// keycap と国旗の時には
    /// <see cref="RgiSequenceFinder.Keycap"/>、<see cref="RegionalIndicator"/>、<see cref="TagSequence"/>
    /// を使って ASCII (byte 列)ベースのテーブル引きをするのでこれも一緒に返す。
    ///
    /// 絵文字判定を受けなかった場合は <see cref="NotEmoji"/>、
    /// 空文字のときは default。
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct EmojiSequence
    {
        [FieldOffset(0)]
        public readonly EmojiSequenceType Type;

        [FieldOffset(4)]
        public readonly int LengthInUtf16;

        [FieldOffset(8)]
        public readonly Keycap Keycap;

        [FieldOffset(8)]
        public readonly RegionalIndicator Region;

        [FieldOffset(8)]
        public readonly TagSequence Tags;

        [FieldOffset(8)]
        public readonly SkinTone SkinTone;

        [FieldOffset(8)]
        public readonly ZwjSplitResult ZwjPositions;

        public EmojiSequence(EmojiSequenceType type, int length) : this()
        {
            Type = type;
            LengthInUtf16 = length;
        }

        public EmojiSequence(Keycap keycap) : this(EmojiSequenceType.Keycap, 3)
        {
            Keycap = keycap;
        }

        public EmojiSequence(RegionalIndicator region) : this(EmojiSequenceType.Flag, 4)
        {
            Region = region;
        }

        public EmojiSequence(TagSequence tags)
            : this(
                  tags.Length == Byte8.MaxLength ? EmojiSequenceType.MoreBufferRequired : EmojiSequenceType.Tag,
                  2 * tags.Length + 2)
        {
            Tags = tags;
        }

        public EmojiSequence(SkinTone skinTone) : this(EmojiSequenceType.SkinTone, 2)
        {
            SkinTone = skinTone;
        }

        public EmojiSequence(int count, ZwjSplitResult zwjPositions) : this(EmojiSequenceType.Other, count)
        {
            ZwjPositions = zwjPositions;
        }

        /// <summary>
        /// 絵文字シーケンス判定を受けなかった1文字。
        /// default が長さ0 (空文字列、文末)を表すのに対してこっちは <see cref="LengthInUtf16"/> が1。
        /// </summary>
        public static readonly EmojiSequence NotEmoji = new EmojiSequence(EmojiSequenceType.NotEmoji, 1);

        public void Deconstruct(out EmojiSequenceType type, out int lengthInUtf16) => (type, lengthInUtf16) = (Type, LengthInUtf16);
    }
}
