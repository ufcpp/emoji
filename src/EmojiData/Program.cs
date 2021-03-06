using EmojiData;

//HashCode.CheckHashCollision(); return;

//var json = await Loader.LoadStringAsync();
//RegexChecker.CountImages(json);

var doc = await Loader.LoadJsonDocAsync();
//JsonDocChecker.Check(doc);
//JsonDocChecker.CheckSkinVariations(doc); return;

var emojiSequenceList = EmojiSequence.EnumerateRgiEmojiSequence(doc).ToArray();
var unvariedEmojiSequenceList = EmojiSequence.EnumerateUnvariedRgiEmojiSequence(doc).ToArray();

System.Console.WriteLine(emojiSequenceList.Length);

//Inspector.CountRunes(emojiSequenceList);
//Inspector.Keycaps(emojiSequenceList);
//Inspector.Category(emojiSequenceList);
//Inspector.GraphemeBreak(emojiSequenceList);
Inspector.Compare(emojiSequenceList, unvariedEmojiSequenceList); return;
//Inspector.FlagIndex(emojiSequenceList); return;
//Inspector.SkinToneIndex(emojiSequenceList); return;
