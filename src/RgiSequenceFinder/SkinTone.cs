namespace RgiSequenceFinder
{
    /// <summary>
    /// 絵文字の肌色。
    /// </summary>
    /// <remarks>
    /// 絵文字の闇その3。
    ///
    /// 医療法面で紫外線耐性の分類で Fitzpatrick skin typing ってのがあるらしく、それを元に肌色を付けることにしたらしい。
    /// 絵文字的には typ1-2, 3, 4, 5, 6 (Fitzpatrick type の1-2を一緒くた)の5種。
    ///
    /// 1F3FB-1F3FF の連番。
    /// (UTF-16 だと high surrogate が D83C 固定、low surrogate が DFFB-DFFF の連番。)
    /// テーブル的にもそのままこの順で0開始の連番で並べて置く想定。
    ///
    /// 本来、skin tone は単体の絵文字扱いしないんだけど…
    /// 絵文字 + skin tone の組み合わせに対応していないとき、skin tone 部分をその色に対応する四角を表示するみたいな仕様があって、
    /// 肌色四角の絵を独立して用意することがほとんど。
    /// この実装でも肌色四角を出す想定。
    ///
    /// 後々追加された髪型選択 1F9B0～1F9B3 は単独表示しなくてもよさそうなんで、Unicode 仕様上も skin tone だけ浮いてたりする。
    /// </remarks>
    public enum SkinTone : sbyte
    {
        /// <summary>
        /// 構造体にぎちぎちにパッキングしたいことがあるので <see cref="System.Nullable{T}"/> を避けて、「skin tone が見つからなかった時は0」みたいな運用する。
        /// </summary>
        None = 0,

        // 残りは 1 からの連番。
        Type2 = 1,
        Type3,
        Type4,
        Type5,
        Type6,
    }

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
    }
}
