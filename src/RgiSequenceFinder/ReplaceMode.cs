using System;

namespace RgiSequenceFinder
{
    /// <summary>
    /// <see cref="Finder.Replace(ReadOnlySpan{char}, Span{char}, bool)"/>
    /// </summary>
    public enum ReplaceMode
    {
        /// <summary>
        /// 単純置換。
        /// </summary>
        None,

        /// <summary>
        /// 外字への置き換えによって元の文字よりも短くなる時、ゼロ幅スペースで埋めることで文字列長を保つ。
        /// </summary>
        FillsZwsp,

        /// <summary>
        /// 絵文字を削除。
        /// </summary>
        /// <remarks>
        /// 「表示できない環境では単純に絵文字を削除」要件で使う。
        /// </remarks>
        Remove,
    }
}
