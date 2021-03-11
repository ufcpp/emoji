﻿using EmojiData;
using System.Linq;

//var json = await Loader.LoadStringAsync();
//RegexChecker.CountImages(json);

var doc = await Loader.LoadJsonDocAsync();
//JsonDocChecker.Check(doc);

var emojiSequenceList = EmojiSequence.EnumerateRgiEmojiSequence(doc).ToArray();

//System.Console.WriteLine(emojiSequenceList.Length);

//Inspector.CountRunes(emojiSequenceList);
//Inspector.Keycaps(emojiSequenceList);
//Inspector.Category(emojiSequenceList);
Inspector.GraphemeBreak(emojiSequenceList);
