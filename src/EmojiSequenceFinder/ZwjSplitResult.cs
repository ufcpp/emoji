namespace RgiSequenceFinder
{
    /// <summary>
    /// 書記素分割の際に ZWJ の位置を記録しておいて再探索を要らなくする。
    /// ZWJ 分割するときに一緒に skin tone の抽出をしておいた方が2度手間にならなくて計算量お得なので一緒に詰め込む。
    /// </summary>
    /// <remarks>
    /// <see cref="Byte8"/> の 0～5 要素目に位置を記録。
    /// ZWJ の数もすぐに再利用したく、6 要素目に入れておくことにする。
    /// 7 要素目に <see cref="SkinTonePair"/> を格納。
    /// (ZWJ 7つ以上入った文字、RGI には入らないだろうし、末尾2要素を特殊用途に利用。)
    /// </remarks>
    public readonly struct ZwjSplitResult
    {
        public const int MaxLength = 6;

        private readonly Byte8 _bytes;

        public ZwjSplitResult(Byte8 zwjPositions, SkinTonePair skinTones)
        {
            int len = 0;
            for (; len < MaxLength; len++) if (zwjPositions[len] == 0) break;

            zwjPositions.V6 = (byte)len;
            zwjPositions.V7 = skinTones.Value;
            _bytes = zwjPositions;
        }

        public byte this[int index] => _bytes[index];

        public int Length => _bytes.V6;

        public SkinTonePair SkinTones => new SkinTonePair(_bytes.V7);
    }
}
