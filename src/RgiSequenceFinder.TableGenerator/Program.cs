using RgiSequenceFinder.TableGenerator;

// 2段コード生成になってる。
// RgiEmojiSequenceList はそんな複雑なデータでもないんで、こっちも emoji-data.json から直接読み込んでもいいんだけど。
// 2段くらいなら大した負担でもないし、とうめん2段コード生成する。

//await RgiSequenceFinder.TableGenerator.Experimental.WriteEmojiDataRow.WriteAsync(); return;
//await RgiSequenceFinder.TableGenerator.Experimental.WriteEmojiDataRow.Categorized(); return;

//RgiSequenceFinder.TableGenerator.Experimental.HashCode.CollisionCount(); return;
//RgiSequenceFinder.TableGenerator.Experimental.SingularEmoji.CheckCount(); return;
//RgiSequenceFinder.TableGenerator.Experimental.SingularEmoji.CollisionCount(); return;
//RgiSequenceFinder.TableGenerator.Experimental.Compaction.CheckConversion(); return;

SourceGenerator.Write("../../../../RgiSequenceFinder/");
