namespace RgiSequenceFinder
{
    /// <summary>
    /// RGI 判定において、
    /// <see cref="SkinTone"/> 0～2個詰め込んだ構造体。
    /// </summary>
    /// <remarks>
    /// <see cref="SkinTone"/> の使い方、RGI テーブルを引くときには、
    /// - まず skin tone を抽出
    /// - skin tone を削った文字列でテーブル引き
    /// - skin tone から計算できるオフセットを足す
    /// - RGI ZWJ sequence 中にある skin tone は1個か2個
    /// みたいな前提があるし、他のデータと一緒に1つの構造体にパッキングするんで1バイトに2 <see cref="SkinTone"/> を詰め込むことに。
    /// </remarks>
    public readonly struct SkinTonePair
    {
        public readonly byte Value;

        public SkinTonePair(byte value) => Value = value;

        public SkinTonePair(SkinTone tone1, SkinTone tone2)
        {
            // SkinTone に -1 みたいな元々想定していない整数値が来ると狂うけど、直にエラー処理はしてない。
            Value = (byte)((byte)tone1 | ((byte)tone2 << 3));
        }

        /// <summary>
        /// 長さ。0～2。
        /// </summary>
        /// <remarks>
        /// この構造体が default のときに 0 になるようにしてある。
        /// </remarks>
        public int Length =>
            Value > 0b111 ? 2 :
            Value > 0 ? 1 :
            0;

        /// <summary>
        /// <see cref="SkinTone"/> 1個目。
        /// holding hands 系絵文字とかで1人目の人の肌色。
        /// ZWJ sequence 的に、2符号点目に出てくる。
        /// </summary>
        public SkinTone Tone1 => (SkinTone)(Value & 0b111);

        /// <summary>
        /// <see cref="SkinTone"/> 2個目。
        /// holding hands 系絵文字とかで2人目の人の肌色。
        /// ZWJ sequence 的に、最後の符号点に出てくる。
        /// </summary>
        public SkinTone Tone2 => (SkinTone)((Value >> 3) & 0b111);

        public void Deconstruct(out int tone1, out int tone2) => (tone1, tone2) = ((int)Tone1, (int)Tone2);
    }
}
