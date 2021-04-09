# 絵文字関連

調査用なので結構適当。

色々確認取った結果は[ドキュメント](docs/)に残してる。

## emoji-data 読み

[src/EmojiData](src/EmojiData)

https://github.com/iamcal/emoji-data の emoji.json の中身を眺めてみてるコード。

この emoji-data は、UCD を元に、RGI な絵文字をスプライトシート化 & 何行何列目にどの絵文字の画像が入ってるかを所定の形式の JSON にまとめてくれてるリポジトリ。

UCD 内にも [emoji-data.txt](https://www.unicode.org/Public/UCD/latest/ucd/emoji/emoji-data.txt) ってファイルがあって紛らわしいので、emoji.json って(拡張子を付けて)呼ぶようにしてる。

## RgiSequenceFinder.TableGenerator

[RgiSequenceFinder.TableGenerator](https://github.com/ufcpp/emoji/tree/main/src/RgiSequenceFinder.TableGenerator)

JSON を読んで、string キーで辞書を引くみたいな処理がやりたくなくて、C# バイナリ化(`byte[]` とか `ushort[]` とかの定数が入った C# ソースコード生成)をしてる。

今、律儀に文字列比較をしてるけど、事前に計算したハッシュ値だけのテーブルでもいいかも。
[書記素判定](docs/grapheme-breaking.md)を受ける文字列で32ビットのハッシュ値が被ることもそうそうないと思うので。

## RgiSequenceFinder

[RgiSequenceFinder](https://github.com/ufcpp/emoji/tree/main/src/RgiSequenceFinder)

上記テーブルを使って RGI 絵文字シーケンスを特定するライブラリ。

emoji.json の並び通りのインデックス番号を返す。

「`string` を読んで `string` を返したい」みたいな場面もあるので、RGI 絵文字シーケンスを U+E000 から始まる外字領域(private use area)にマッピングするみたいなメソッドもある。
あと、その前後で文字列長が変わって欲しくないみたいな場面もあったので、「外字への置き換えで縮んだ分はゼロ幅スペース(U+200B)で埋める」みたいなオプションもある。

## 他

他は、単体テストとか、依存関係の整理でプロジェクトを独立させたもの。
