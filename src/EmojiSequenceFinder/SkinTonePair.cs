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
    ///
    /// あと、RGI 判定の際に FE0F を無視する(含んでいたら削る)って処理もしたいけど、
    /// 削る処理が必要かどうかを2度手間で探索したくないのでこの構造体に一緒に記録する。
    /// skin tone と FE0F の判定は常に近い位置にある(grapheme breaking の仕様上、同じ Extend っていうくくりになってる)ので。
    ///
    /// ビットの使い方(上位ビットから順に):
    /// - 1ビット: FE0F を含むかどうか
    /// - 1ビット: 未使用
    /// - 3ビット: tone2 + 1
    /// - 3ビット: tone1 + 1
    ///
    /// tone を +1 してるのは、「付いていないときに 0」になるようにして長さを別途持たなくてもよくしてる。
    /// </remarks>
    public readonly struct SkinTonePair
    {
        public readonly byte Value;

        public SkinTonePair(byte value) => Value = value;

        public SkinTonePair(SkinTone tone1, SkinTone tone2)
        {
            // tone がない時 -1 が来る前提。
            // -2 とかみたいなのが来ると処理が狂う。
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
        /// カップル絵文字とかで前の人の肌色。
        /// ZWJ sequence 的に、2符号点目に出てくる。
        /// </summary>
        public SkinTone Tone1 => (SkinTone)(Value & 0b111);

        /// <summary>
        /// <see cref="SkinTone"/> 2個目。
        /// カップル絵文字とかで後の人の肌色。
        /// ZWJ sequence 的に、最後の符号点に出てくる。
        /// </summary>
        public SkinTone Tone2 => (SkinTone)((Value >> 3) & 0b111);

        public void Deconstruct(out int tone1, out int tone2) => (tone1, tone2) = ((int)Tone1, (int)Tone2);
    }
}
