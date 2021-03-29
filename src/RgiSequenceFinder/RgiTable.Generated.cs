﻿// <auto-generated>
// RgiSequenceFinder.TableGenerator
// </auto-generated>

namespace RgiSequenceFinder
{
    partial class RgiTable
    {
        private static int FindKeycap(Keycap key) => key.Value switch
        {
            (byte)'#' => 0,
            (byte)'*' => 1,
            (byte)'0' => 2,
            (byte)'1' => 3,
            (byte)'2' => 4,
            (byte)'3' => 5,
            (byte)'4' => 6,
            (byte)'5' => 7,
            (byte)'6' => 8,
            (byte)'7' => 9,
            (byte)'8' => 10,
            (byte)'9' => 11,
            _ => -1,
        };

        private static System.ReadOnlySpan<byte> _regionTable1 => new byte[] { 0, 0, 31, 32, 33, 34, 35, 0, 36, 0, 0, 37, 38, 0, 39, 0, 40, 41, 42, 43, 44, 0, 45, 46, 0, 47, 48, 49, 0, 50, 51, 52, 53, 54, 55, 56, 0, 57, 58, 59, 60, 0, 61, 62, 63, 64, 0, 65, 66, 0, 67, 68, 69, 0, 70, 71, 0, 72, 73, 74, 75, 0, 76, 77, 78, 79, 80, 81, 0, 82, 0, 0, 83, 84, 85, 86, 87, 88, 0, 0, 0, 0, 89, 0, 90, 0, 0, 91, 92, 0, 93, 0, 94, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 95, 96, 0, 97, 0, 98, 0, 99, 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 101, 102, 103, 104, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 105, 106, 107, 0, 108, 0, 109, 0, 0, 110, 0, 0, 0, 0, 0, 0, 0, 0, 111, 112, 0, 113, 114, 115, 116, 117, 118, 0, 0, 119, 120, 121, 0, 122, 123, 124, 125, 126, 127, 0, 128, 0, 129, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 130, 0, 131, 132, 0, 0, 0, 133, 0, 134, 135, 0, 0, 0, 0, 0, 0, 0, 136, 137, 138, 0, 0, 0, 0, 0, 0, 139, 140, 141, 142, 0, 143, 144, 145, 146, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 147, 0, 0, 0, 0, 0, 0, 0, 148, 0, 149, 150, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 151, 0, 152, 153, 154, 0, 0, 0, 155, 156, 0, 157, 0, 158, 0, 0, 0, 0, 159, 0, 160, 161, 162, 163, 164, 0, 0, 0, 0, 0, 165, 0, 166, 0, 0, 0, 0, 0, 0, 167, 168, 169, 170, 171, 0, 0, 172, 0, 173, 0, 174, 175, 176, 177, 178, 179, 0, 0, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, };
        private static System.ReadOnlySpan<byte> _regionTable2 => new byte[] { 68, 0, 69, 0, 70, 71, 72, 0, 73, 0, 0, 74, 0, 0, 75, 76, 0, 77, 0, 0, 78, 0, 0, 0, 0, 79, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 80, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 81, 0, 0, 0, 82, 83, 84, 85, 0, 0, 86, 87, 88, 89, 0, 0, 0, 90, 91, 92, 0, 0, 93, 0, 94, 0, 95, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 96, 0, 0, 0, 0, 0, 0, 0, 0, 0, 97, 0, 0, 0, 98, 0, 99, 0, 100, 0, 0, 0, 101, 102, 103, 104, 105, 0, 106, 107, 108, 109, 110, 111, 112, 113, 114, 0, 0, 115, 116, 117, 0, 118, 0, 119, 120, 121, 122, 0, 123, 124, 0, 125, 126, 127, 0, 128, 129, 130, 131, 132, 133, 0, 0, 134, 0, 135, 0, 136, 137, 0, 0, 138, 139, 0, 0, 0, 0, 0, 140, 0, 0, 0, 0, 0, 141, 142, 0, 0, 0, 0, 143, 0, 0, 0, 0, 0, 144, 145, 146, 0, 147, 0, 148, 0, 149, 0, 150, 0, 0, 0, 0, 151, 0, 0, 0, 0, 0, 0, 152, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 153, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 154, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 155, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 156, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 157, 0, 0, 0, 0, 0, 0, 158, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 159, 0, 0, 0, 0, 0, 0, 0, 0, 0, 160, 0, 0, 0, };

        private static int FindRegion(RegionalIndicator region)
        {
            var v = (ushort)(region.Value - 1);
            if (v >= 26 * 26) return -1;

            if (v < 13 * 26)
            {
                var i = _regionTable1[v];
                if (i == 0) return -1;
                else return i;
            }
            else
            {
                var i = _regionTable2[v - 13 * 26];
                if (i == 0) return -1;
                else return i + 128;
            }
        }

        private static int FindTag(TagSequence tags) => tags.LongValue switch
        {
            0x7F676E656267UL => 642,
            0x7F7463736267UL => 643,
            0x7F736C776267UL => 644,
            _ => -1,
        };

        private const int _skinToneFirstIndex = 651;

        private static int FindSkinTone(SkinTone skinTone) => _skinToneFirstIndex + (int)skinTone;

        private static CharDictionary[,] _singularTable = new CharDictionary[,]
        {
            {
                new(256,
                    new ushort[] { 0x231A, 0x231B, 0x23E9, 0x23EA, 0x23EB, 0x23EC, 0x23F0, 0x23F3, 0x25FD, 0x25FE, 0x2614, 0x2615, 0x261D, 0x2648, 0x2649, 0x264A, 0x264B, 0x264C, 0x264D, 0x264E, 0x264F, 0x2650, 0x2651, 0x2652, 0x2653, 0x267F, 0x2693, 0x26A1, 0x26AA, 0x26AB, 0x26BD, 0x26BE, 0x26C4, 0x26C5, 0x26CE, 0x26D4, 0x26EA, 0x26F2, 0x26F3, 0x26F5, 0x26F9, 0x26FA, 0x26FD, 0x2705, 0x270A, 0x270B, 0x270C, 0x270D, 0x2728, 0x274C, 0x274E, 0x2753, 0x2754, 0x2755, 0x2757, 0x2795, 0x2796, 0x2797, 0x27B0, 0x27BF, 0x2B1B, 0x2B1C, 0x2B50, 0x2B55, },
                    new ushort[] { 3102, 3103, 3106, 3107, 3108, 3109, 3113, 3116, 3127, 3128, 3136, 3137, 3139, 3157, 3158, 3159, 3160, 3161, 3162, 3163, 3164, 3165, 3166, 3167, 3168, 3177, 3179, 3188, 3190, 3191, 3194, 3195, 3196, 3197, 3199, 3203, 3205, 3208, 3209, 3211, 3226, 3232, 3233, 3235, 3238, 3244, 3250, 3256, 3268, 3273, 3274, 3275, 3276, 3277, 3278, 3281, 3282, 3283, 3285, 3286, 3292, 3293, 3294, 3295, }),
                new(512,
                    new ushort[] { 0xDC04, 0xDCCF, 0xDD8E, 0xDD91, 0xDD92, 0xDD93, 0xDD94, 0xDD95, 0xDD96, 0xDD97, 0xDD98, 0xDD99, 0xDD9A, 0xDE01, 0xDE1A, 0xDE2F, 0xDE32, 0xDE33, 0xDE34, 0xDE35, 0xDE36, 0xDE38, 0xDE39, 0xDE3A, 0xDE50, 0xDE51, 0xDF00, 0xDF01, 0xDF02, 0xDF03, 0xDF04, 0xDF05, 0xDF06, 0xDF07, 0xDF08, 0xDF09, 0xDF0A, 0xDF0B, 0xDF0C, 0xDF0D, 0xDF0E, 0xDF0F, 0xDF10, 0xDF11, 0xDF12, 0xDF13, 0xDF14, 0xDF15, 0xDF16, 0xDF17, 0xDF18, 0xDF19, 0xDF1A, 0xDF1B, 0xDF1C, 0xDF1D, 0xDF1E, 0xDF1F, 0xDF20, 0xDF2D, 0xDF2E, 0xDF2F, 0xDF30, 0xDF31, 0xDF32, 0xDF33, 0xDF34, 0xDF35, 0xDF37, 0xDF38, 0xDF39, 0xDF3A, 0xDF3B, 0xDF3C, 0xDF3D, 0xDF3E, 0xDF3F, 0xDF40, 0xDF41, 0xDF42, 0xDF43, 0xDF44, 0xDF45, 0xDF46, 0xDF47, 0xDF48, 0xDF49, 0xDF4A, 0xDF4B, 0xDF4C, 0xDF4D, 0xDF4E, 0xDF4F, 0xDF50, 0xDF51, 0xDF52, 0xDF53, 0xDF54, 0xDF55, 0xDF56, 0xDF57, 0xDF58, 0xDF59, 0xDF5A, 0xDF5B, 0xDF5C, 0xDF5D, 0xDF5E, 0xDF5F, 0xDF60, 0xDF61, 0xDF62, 0xDF63, 0xDF64, 0xDF65, 0xDF66, 0xDF67, 0xDF68, 0xDF69, 0xDF6A, 0xDF6B, 0xDF6C, 0xDF6D, 0xDF6E, 0xDF6F, 0xDF70, 0xDF71, 0xDF72, 0xDF73, 0xDF74, 0xDF75, 0xDF76, 0xDF77, 0xDF78, 0xDF79, 0xDF7A, 0xDF7B, 0xDF7C, 0xDF7E, 0xDF7F, 0xDF80, 0xDF81, 0xDF82, 0xDF83, 0xDF84, 0xDF85, 0xDF86, 0xDF87, 0xDF88, 0xDF89, 0xDF8A, 0xDF8B, 0xDF8C, 0xDF8D, 0xDF8E, 0xDF8F, 0xDF90, 0xDF91, 0xDF92, 0xDF93, 0xDFA0, 0xDFA1, 0xDFA2, 0xDFA3, 0xDFA4, 0xDFA5, 0xDFA6, 0xDFA7, 0xDFA8, 0xDFA9, 0xDFAA, 0xDFAB, 0xDFAC, 0xDFAD, 0xDFAE, 0xDFAF, 0xDFB0, 0xDFB1, 0xDFB2, 0xDFB3, 0xDFB4, 0xDFB5, 0xDFB6, 0xDFB7, 0xDFB8, 0xDFB9, 0xDFBA, 0xDFBB, 0xDFBC, 0xDFBD, 0xDFBE, 0xDFBF, 0xDFC0, 0xDFC1, 0xDFC2, 0xDFC3, 0xDFC4, 0xDFC5, 0xDFC6, 0xDFC7, 0xDFC8, 0xDFC9, 0xDFCA, 0xDFCB, 0xDFCC, 0xDFCF, 0xDFD0, 0xDFD1, 0xDFD2, 0xDFD3, 0xDFE0, 0xDFE1, 0xDFE2, 0xDFE3, 0xDFE4, 0xDFE5, 0xDFE6, 0xDFE7, 0xDFE8, 0xDFE9, 0xDFEA, 0xDFEB, 0xDFEC, 0xDFED, 0xDFEE, 0xDFEF, 0xDFF0, 0xDFF4, 0xDFF8, 0xDFF9, 0xDFFA, },
                    new ushort[] { 14, 15, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 289, 291, 292, 293, 294, 295, 296, 297, 299, 300, 301, 302, 303, 304, 305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 320, 321, 322, 323, 324, 325, 326, 327, 328, 329, 330, 331, 332, 333, 334, 335, 336, 347, 348, 349, 350, 351, 352, 353, 354, 355, 357, 358, 359, 360, 361, 362, 363, 364, 365, 366, 367, 368, 369, 370, 371, 372, 373, 374, 375, 376, 377, 378, 379, 380, 381, 382, 383, 384, 385, 386, 387, 388, 389, 390, 391, 392, 393, 394, 395, 396, 397, 398, 399, 400, 401, 402, 403, 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 415, 416, 417, 418, 419, 420, 421, 422, 423, 424, 425, 426, 428, 429, 430, 431, 432, 433, 434, 435, 441, 442, 443, 444, 445, 446, 447, 448, 449, 450, 451, 452, 453, 454, 462, 463, 464, 465, 466, 467, 468, 469, 470, 471, 472, 473, 474, 475, 476, 477, 478, 479, 480, 481, 482, 483, 484, 485, 486, 487, 488, 489, 490, 491, 492, 493, 494, 495, 496, 514, 532, 538, 539, 540, 546, 547, 560, 578, 596, 604, 605, 606, 607, 608, 621, 622, 623, 624, 625, 626, 627, 628, 629, 630, 631, 632, 633, 634, 635, 636, 637, 645, 648, 649, 650, }),
                new(1024,
                    new ushort[] { 0xDC00, 0xDC01, 0xDC02, 0xDC03, 0xDC04, 0xDC05, 0xDC06, 0xDC07, 0xDC08, 0xDC09, 0xDC0A, 0xDC0B, 0xDC0C, 0xDC0D, 0xDC0E, 0xDC0F, 0xDC10, 0xDC11, 0xDC12, 0xDC13, 0xDC14, 0xDC15, 0xDC16, 0xDC17, 0xDC18, 0xDC19, 0xDC1A, 0xDC1B, 0xDC1C, 0xDC1D, 0xDC1E, 0xDC1F, 0xDC20, 0xDC21, 0xDC22, 0xDC23, 0xDC24, 0xDC25, 0xDC26, 0xDC27, 0xDC28, 0xDC29, 0xDC2A, 0xDC2B, 0xDC2C, 0xDC2D, 0xDC2E, 0xDC2F, 0xDC30, 0xDC31, 0xDC32, 0xDC33, 0xDC34, 0xDC35, 0xDC36, 0xDC37, 0xDC38, 0xDC39, 0xDC3A, 0xDC3B, 0xDC3C, 0xDC3D, 0xDC3E, 0xDC40, 0xDC42, 0xDC43, 0xDC44, 0xDC45, 0xDC46, 0xDC47, 0xDC48, 0xDC49, 0xDC4A, 0xDC4B, 0xDC4C, 0xDC4D, 0xDC4E, 0xDC4F, 0xDC50, 0xDC51, 0xDC52, 0xDC53, 0xDC54, 0xDC55, 0xDC56, 0xDC57, 0xDC58, 0xDC59, 0xDC5A, 0xDC5B, 0xDC5C, 0xDC5D, 0xDC5E, 0xDC5F, 0xDC60, 0xDC61, 0xDC62, 0xDC63, 0xDC64, 0xDC65, 0xDC66, 0xDC67, 0xDC68, 0xDC69, 0xDC6A, 0xDC6B, 0xDC6C, 0xDC6D, 0xDC6E, 0xDC6F, 0xDC70, 0xDC71, 0xDC72, 0xDC73, 0xDC74, 0xDC75, 0xDC76, 0xDC77, 0xDC78, 0xDC79, 0xDC7A, 0xDC7B, 0xDC7C, 0xDC7D, 0xDC7E, 0xDC7F, 0xDC80, 0xDC81, 0xDC82, 0xDC83, 0xDC84, 0xDC85, 0xDC86, 0xDC87, 0xDC88, 0xDC89, 0xDC8A, 0xDC8B, 0xDC8C, 0xDC8D, 0xDC8E, 0xDC8F, 0xDC90, 0xDC91, 0xDC92, 0xDC93, 0xDC94, 0xDC95, 0xDC96, 0xDC97, 0xDC98, 0xDC99, 0xDC9A, 0xDC9B, 0xDC9C, 0xDC9D, 0xDC9E, 0xDC9F, 0xDCA0, 0xDCA1, 0xDCA2, 0xDCA3, 0xDCA4, 0xDCA5, 0xDCA6, 0xDCA7, 0xDCA8, 0xDCA9, 0xDCAA, 0xDCAB, 0xDCAC, 0xDCAD, 0xDCAE, 0xDCAF, 0xDCB0, 0xDCB1, 0xDCB2, 0xDCB3, 0xDCB4, 0xDCB5, 0xDCB6, 0xDCB7, 0xDCB8, 0xDCB9, 0xDCBA, 0xDCBB, 0xDCBC, 0xDCBD, 0xDCBE, 0xDCBF, 0xDCC0, 0xDCC1, 0xDCC2, 0xDCC3, 0xDCC4, 0xDCC5, 0xDCC6, 0xDCC7, 0xDCC8, 0xDCC9, 0xDCCA, 0xDCCB, 0xDCCC, 0xDCCD, 0xDCCE, 0xDCCF, 0xDCD0, 0xDCD1, 0xDCD2, 0xDCD3, 0xDCD4, 0xDCD5, 0xDCD6, 0xDCD7, 0xDCD8, 0xDCD9, 0xDCDA, 0xDCDB, 0xDCDC, 0xDCDD, 0xDCDE, 0xDCDF, 0xDCE0, 0xDCE1, 0xDCE2, 0xDCE3, 0xDCE4, 0xDCE5, 0xDCE6, 0xDCE7, 0xDCE8, 0xDCE9, 0xDCEA, 0xDCEB, 0xDCEC, 0xDCED, 0xDCEE, 0xDCEF, 0xDCF0, 0xDCF1, 0xDCF2, 0xDCF3, 0xDCF4, 0xDCF5, 0xDCF6, 0xDCF7, 0xDCF8, 0xDCF9, 0xDCFA, 0xDCFB, 0xDCFC, 0xDCFF, 0xDD00, 0xDD01, 0xDD02, 0xDD03, 0xDD04, 0xDD05, 0xDD06, 0xDD07, 0xDD08, 0xDD09, 0xDD0A, 0xDD0B, 0xDD0C, 0xDD0D, 0xDD0E, 0xDD0F, 0xDD10, 0xDD11, 0xDD12, 0xDD13, 0xDD14, 0xDD15, 0xDD16, 0xDD17, 0xDD18, 0xDD19, 0xDD1A, 0xDD1B, 0xDD1C, 0xDD1D, 0xDD1E, 0xDD1F, 0xDD20, 0xDD21, 0xDD22, 0xDD23, 0xDD24, 0xDD25, 0xDD26, 0xDD27, 0xDD28, 0xDD29, 0xDD2A, 0xDD2B, 0xDD2C, 0xDD2D, 0xDD2E, 0xDD2F, 0xDD30, 0xDD31, 0xDD32, 0xDD33, 0xDD34, 0xDD35, 0xDD36, 0xDD37, 0xDD38, 0xDD39, 0xDD3A, 0xDD3B, 0xDD3C, 0xDD3D, 0xDD4B, 0xDD4C, 0xDD4D, 0xDD4E, 0xDD50, 0xDD51, 0xDD52, 0xDD53, 0xDD54, 0xDD55, 0xDD56, 0xDD57, 0xDD58, 0xDD59, 0xDD5A, 0xDD5B, 0xDD5C, 0xDD5D, 0xDD5E, 0xDD5F, 0xDD60, 0xDD61, 0xDD62, 0xDD63, 0xDD64, 0xDD65, 0xDD66, 0xDD67, 0xDD74, 0xDD75, 0xDD7A, 0xDD90, 0xDD95, 0xDD96, 0xDDA4, 0xDDFB, 0xDDFC, 0xDDFD, 0xDDFE, 0xDDFF, 0xDE00, 0xDE01, 0xDE02, 0xDE03, 0xDE04, 0xDE05, 0xDE06, 0xDE07, 0xDE08, 0xDE09, 0xDE0A, 0xDE0B, 0xDE0C, 0xDE0D, 0xDE0E, 0xDE0F, 0xDE10, 0xDE11, 0xDE12, 0xDE13, 0xDE14, 0xDE15, 0xDE16, 0xDE17, 0xDE18, 0xDE19, 0xDE1A, 0xDE1B, 0xDE1C, 0xDE1D, 0xDE1E, 0xDE1F, 0xDE20, 0xDE21, 0xDE22, 0xDE23, 0xDE24, 0xDE25, 0xDE26, 0xDE27, 0xDE28, 0xDE29, 0xDE2A, 0xDE2B, 0xDE2C, 0xDE2D, 0xDE2E, 0xDE2F, 0xDE30, 0xDE31, 0xDE32, 0xDE33, 0xDE34, 0xDE35, 0xDE36, 0xDE37, 0xDE38, 0xDE39, 0xDE3A, 0xDE3B, 0xDE3C, 0xDE3D, 0xDE3E, 0xDE3F, 0xDE40, 0xDE41, 0xDE42, 0xDE43, 0xDE44, 0xDE45, 0xDE46, 0xDE47, 0xDE48, 0xDE49, 0xDE4A, 0xDE4B, 0xDE4C, 0xDE4D, 0xDE4E, 0xDE4F, 0xDE80, 0xDE81, 0xDE82, 0xDE83, 0xDE84, 0xDE85, 0xDE86, 0xDE87, 0xDE88, 0xDE89, 0xDE8A, 0xDE8B, 0xDE8C, 0xDE8D, 0xDE8E, 0xDE8F, 0xDE90, 0xDE91, 0xDE92, 0xDE93, 0xDE94, 0xDE95, 0xDE96, 0xDE97, 0xDE98, 0xDE99, 0xDE9A, 0xDE9B, 0xDE9C, 0xDE9D, 0xDE9E, 0xDE9F, 0xDEA0, 0xDEA1, 0xDEA2, 0xDEA3, 0xDEA4, 0xDEA5, 0xDEA6, 0xDEA7, 0xDEA8, 0xDEA9, 0xDEAA, 0xDEAB, 0xDEAC, 0xDEAD, 0xDEAE, 0xDEAF, 0xDEB0, 0xDEB1, 0xDEB2, 0xDEB3, 0xDEB4, 0xDEB5, 0xDEB6, 0xDEB7, 0xDEB8, 0xDEB9, 0xDEBA, 0xDEBB, 0xDEBC, 0xDEBD, 0xDEBE, 0xDEBF, 0xDEC0, 0xDEC1, 0xDEC2, 0xDEC3, 0xDEC4, 0xDEC5, 0xDECC, 0xDED0, 0xDED1, 0xDED2, 0xDED5, 0xDED6, 0xDED7, 0xDEEB, 0xDEEC, 0xDEF4, 0xDEF5, 0xDEF6, 0xDEF7, 0xDEF8, 0xDEF9, 0xDEFA, 0xDEFB, 0xDEFC, 0xDFE0, 0xDFE1, 0xDFE2, 0xDFE3, 0xDFE4, 0xDFE5, 0xDFE6, 0xDFE7, 0xDFE8, 0xDFE9, 0xDFEA, 0xDFEB, },
                    new ushort[] { 656, 657, 658, 659, 660, 661, 662, 663, 665, 666, 667, 668, 669, 670, 671, 672, 673, 674, 675, 676, 677, 679, 680, 681, 682, 683, 684, 685, 686, 687, 688, 689, 690, 691, 692, 693, 694, 695, 696, 697, 698, 699, 700, 701, 702, 703, 704, 705, 706, 707, 708, 709, 710, 711, 712, 713, 714, 715, 716, 718, 719, 720, 721, 723, 726, 732, 738, 739, 740, 746, 752, 758, 764, 770, 776, 782, 788, 794, 800, 806, 807, 808, 809, 810, 811, 812, 813, 814, 815, 816, 817, 818, 819, 820, 821, 822, 823, 824, 825, 826, 827, 833, 1000, 1164, 1170, 1171, 1197, 1223, 1261, 1269, 1282, 1300, 1306, 1324, 1330, 1336, 1342, 1360, 1366, 1372, 1373, 1374, 1375, 1381, 1382, 1383, 1384, 1397, 1415, 1421, 1427, 1428, 1446, 1464, 1470, 1471, 1472, 1473, 1474, 1475, 1476, 1477, 1478, 1479, 1480, 1481, 1482, 1483, 1484, 1485, 1486, 1487, 1488, 1489, 1490, 1491, 1492, 1493, 1494, 1495, 1496, 1497, 1498, 1499, 1500, 1501, 1502, 1503, 1504, 1510, 1511, 1512, 1513, 1514, 1515, 1516, 1517, 1518, 1519, 1520, 1521, 1522, 1523, 1524, 1525, 1526, 1527, 1528, 1529, 1530, 1531, 1532, 1533, 1534, 1535, 1536, 1537, 1538, 1539, 1540, 1541, 1542, 1543, 1544, 1545, 1546, 1547, 1548, 1549, 1550, 1551, 1552, 1553, 1554, 1555, 1556, 1557, 1558, 1559, 1560, 1561, 1562, 1563, 1564, 1565, 1566, 1567, 1568, 1569, 1570, 1571, 1572, 1573, 1574, 1575, 1576, 1577, 1578, 1579, 1580, 1581, 1582, 1583, 1584, 1585, 1586, 1587, 1588, 1589, 1590, 1591, 1593, 1594, 1595, 1596, 1597, 1598, 1599, 1600, 1601, 1602, 1603, 1604, 1605, 1606, 1607, 1608, 1609, 1610, 1611, 1612, 1613, 1614, 1615, 1616, 1617, 1618, 1619, 1620, 1621, 1622, 1623, 1624, 1625, 1626, 1627, 1628, 1629, 1630, 1631, 1632, 1633, 1634, 1635, 1636, 1637, 1638, 1639, 1640, 1641, 1642, 1643, 1644, 1645, 1646, 1647, 1648, 1649, 1650, 1651, 1652, 1653, 1654, 1655, 1658, 1659, 1660, 1661, 1662, 1663, 1664, 1665, 1666, 1667, 1668, 1669, 1670, 1671, 1672, 1673, 1674, 1675, 1676, 1677, 1678, 1679, 1680, 1681, 1682, 1683, 1684, 1685, 1689, 1707, 1717, 1728, 1734, 1740, 1746, 1767, 1768, 1769, 1770, 1771, 1772, 1773, 1774, 1775, 1776, 1777, 1778, 1779, 1780, 1781, 1782, 1783, 1784, 1785, 1786, 1787, 1788, 1789, 1790, 1791, 1792, 1793, 1794, 1795, 1796, 1797, 1798, 1799, 1800, 1801, 1802, 1803, 1804, 1805, 1806, 1807, 1808, 1809, 1810, 1811, 1812, 1813, 1814, 1815, 1816, 1817, 1818, 1819, 1820, 1821, 1822, 1823, 1824, 1825, 1826, 1827, 1828, 1829, 1830, 1831, 1832, 1833, 1834, 1835, 1836, 1837, 1838, 1839, 1840, 1853, 1871, 1889, 1895, 1896, 1897, 1910, 1916, 1934, 1952, 1958, 1964, 1965, 1966, 1967, 1968, 1969, 1970, 1971, 1972, 1973, 1974, 1975, 1976, 1977, 1978, 1979, 1980, 1981, 1982, 1983, 1984, 1985, 1986, 1987, 1988, 1989, 1990, 1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 2011, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030, 2031, 2032, 2045, 2063, 2081, 2087, 2088, 2089, 2090, 2091, 2092, 2093, 2094, 2095, 2096, 2102, 2103, 2104, 2105, 2106, 2108, 2117, 2118, 2119, 2120, 2121, 2122, 2130, 2131, 2134, 2135, 2136, 2137, 2138, 2139, 2140, 2141, 2142, 2143, 2144, 2145, 2146, 2147, 2148, 2149, 2150, 2151, 2152, 2153, 2154, }),
                new(512,
                    new ushort[] { 0xDD0C, 0xDD0D, 0xDD0E, 0xDD0F, 0xDD10, 0xDD11, 0xDD12, 0xDD13, 0xDD14, 0xDD15, 0xDD16, 0xDD17, 0xDD18, 0xDD19, 0xDD1A, 0xDD1B, 0xDD1C, 0xDD1D, 0xDD1E, 0xDD1F, 0xDD20, 0xDD21, 0xDD22, 0xDD23, 0xDD24, 0xDD25, 0xDD26, 0xDD27, 0xDD28, 0xDD29, 0xDD2A, 0xDD2B, 0xDD2C, 0xDD2D, 0xDD2E, 0xDD2F, 0xDD30, 0xDD31, 0xDD32, 0xDD33, 0xDD34, 0xDD35, 0xDD36, 0xDD37, 0xDD38, 0xDD39, 0xDD3A, 0xDD3C, 0xDD3D, 0xDD3E, 0xDD3F, 0xDD40, 0xDD41, 0xDD42, 0xDD43, 0xDD44, 0xDD45, 0xDD47, 0xDD48, 0xDD49, 0xDD4A, 0xDD4B, 0xDD4C, 0xDD4D, 0xDD4E, 0xDD4F, 0xDD50, 0xDD51, 0xDD52, 0xDD53, 0xDD54, 0xDD55, 0xDD56, 0xDD57, 0xDD58, 0xDD59, 0xDD5A, 0xDD5B, 0xDD5C, 0xDD5D, 0xDD5E, 0xDD5F, 0xDD60, 0xDD61, 0xDD62, 0xDD63, 0xDD64, 0xDD65, 0xDD66, 0xDD67, 0xDD68, 0xDD69, 0xDD6A, 0xDD6B, 0xDD6C, 0xDD6D, 0xDD6E, 0xDD6F, 0xDD70, 0xDD71, 0xDD72, 0xDD73, 0xDD74, 0xDD75, 0xDD76, 0xDD77, 0xDD78, 0xDD7A, 0xDD7B, 0xDD7C, 0xDD7D, 0xDD7E, 0xDD7F, 0xDD80, 0xDD81, 0xDD82, 0xDD83, 0xDD84, 0xDD85, 0xDD86, 0xDD87, 0xDD88, 0xDD89, 0xDD8A, 0xDD8B, 0xDD8C, 0xDD8D, 0xDD8E, 0xDD8F, 0xDD90, 0xDD91, 0xDD92, 0xDD93, 0xDD94, 0xDD95, 0xDD96, 0xDD97, 0xDD98, 0xDD99, 0xDD9A, 0xDD9B, 0xDD9C, 0xDD9D, 0xDD9E, 0xDD9F, 0xDDA0, 0xDDA1, 0xDDA2, 0xDDA3, 0xDDA4, 0xDDA5, 0xDDA6, 0xDDA7, 0xDDA8, 0xDDA9, 0xDDAA, 0xDDAB, 0xDDAC, 0xDDAD, 0xDDAE, 0xDDAF, 0xDDB4, 0xDDB5, 0xDDB6, 0xDDB7, 0xDDB8, 0xDDB9, 0xDDBA, 0xDDBB, 0xDDBC, 0xDDBD, 0xDDBE, 0xDDBF, 0xDDC0, 0xDDC1, 0xDDC2, 0xDDC3, 0xDDC4, 0xDDC5, 0xDDC6, 0xDDC7, 0xDDC8, 0xDDC9, 0xDDCA, 0xDDCB, 0xDDCD, 0xDDCE, 0xDDCF, 0xDDD0, 0xDDD1, 0xDDD2, 0xDDD3, 0xDDD4, 0xDDD5, 0xDDD6, 0xDDD7, 0xDDD8, 0xDDD9, 0xDDDA, 0xDDDB, 0xDDDC, 0xDDDD, 0xDDDE, 0xDDDF, 0xDDE0, 0xDDE1, 0xDDE2, 0xDDE3, 0xDDE4, 0xDDE5, 0xDDE6, 0xDDE7, 0xDDE8, 0xDDE9, 0xDDEA, 0xDDEB, 0xDDEC, 0xDDED, 0xDDEE, 0xDDEF, 0xDDF0, 0xDDF1, 0xDDF2, 0xDDF3, 0xDDF4, 0xDDF5, 0xDDF6, 0xDDF7, 0xDDF8, 0xDDF9, 0xDDFA, 0xDDFB, 0xDDFC, 0xDDFD, 0xDDFE, 0xDDFF, 0xDE70, 0xDE71, 0xDE72, 0xDE73, 0xDE74, 0xDE78, 0xDE79, 0xDE7A, 0xDE80, 0xDE81, 0xDE82, 0xDE83, 0xDE84, 0xDE85, 0xDE86, 0xDE90, 0xDE91, 0xDE92, 0xDE93, 0xDE94, 0xDE95, 0xDE96, 0xDE97, 0xDE98, 0xDE99, 0xDE9A, 0xDE9B, 0xDE9C, 0xDE9D, 0xDE9E, 0xDE9F, 0xDEA0, 0xDEA1, 0xDEA2, 0xDEA3, 0xDEA4, 0xDEA5, 0xDEA6, 0xDEA7, 0xDEA8, 0xDEB0, 0xDEB1, 0xDEB2, 0xDEB3, 0xDEB4, 0xDEB5, 0xDEB6, 0xDEC0, 0xDEC1, 0xDEC2, 0xDED0, 0xDED1, 0xDED2, 0xDED3, 0xDED4, 0xDED5, 0xDED6, },
                    new ushort[] { 2155, 2161, 2162, 2163, 2169, 2170, 2171, 2172, 2173, 2174, 2175, 2176, 2177, 2183, 2189, 2195, 2201, 2207, 2208, 2214, 2220, 2221, 2222, 2223, 2224, 2225, 2238, 2244, 2245, 2246, 2247, 2248, 2249, 2250, 2251, 2252, 2253, 2259, 2265, 2271, 2277, 2295, 2301, 2319, 2337, 2355, 2361, 2364, 2377, 2395, 2401, 2402, 2403, 2404, 2405, 2406, 2407, 2408, 2409, 2410, 2411, 2412, 2413, 2414, 2415, 2416, 2417, 2418, 2419, 2420, 2421, 2422, 2423, 2424, 2425, 2426, 2427, 2428, 2429, 2430, 2431, 2432, 2433, 2434, 2435, 2436, 2437, 2438, 2439, 2440, 2441, 2442, 2443, 2444, 2445, 2446, 2447, 2448, 2449, 2450, 2451, 2452, 2453, 2454, 2455, 2456, 2462, 2463, 2464, 2465, 2466, 2467, 2468, 2469, 2470, 2471, 2472, 2473, 2474, 2475, 2476, 2477, 2478, 2479, 2480, 2481, 2482, 2483, 2484, 2485, 2486, 2487, 2488, 2489, 2490, 2491, 2492, 2493, 2494, 2495, 2496, 2497, 2498, 2499, 2500, 2501, 2502, 2503, 2504, 2505, 2506, 2507, 2508, 2509, 2510, 2511, 2512, 2513, 2514, 2515, 2516, 2517, 2518, 2524, 2530, 2543, 2561, 2567, 2568, 2574, 2575, 2576, 2577, 2578, 2579, 2580, 2581, 2582, 2583, 2584, 2585, 2586, 2587, 2588, 2589, 2602, 2620, 2638, 2644, 2821, 2827, 2833, 2839, 2845, 2863, 2881, 2899, 2917, 2935, 2953, 2971, 2989, 2997, 3000, 3001, 3002, 3003, 3004, 3005, 3006, 3007, 3008, 3009, 3010, 3011, 3012, 3013, 3014, 3015, 3016, 3017, 3018, 3019, 3020, 3021, 3022, 3023, 3024, 3025, 3026, 3027, 3028, 3029, 3030, 3031, 3032, 3033, 3034, 3035, 3036, 3037, 3038, 3039, 3040, 3041, 3042, 3043, 3044, 3045, 3046, 3047, 3048, 3049, 3050, 3051, 3052, 3053, 3054, 3055, 3056, 3057, 3058, 3059, 3060, 3061, 3062, 3063, 3064, 3065, 3066, 3067, 3068, 3069, 3070, 3071, 3072, 3073, 3074, 3075, 3076, 3077, 3078, 3079, 3080, 3081, 3082, 3083, 3084, 3085, 3086, 3087, 3088, 3089, }),
            },
            {
                new(512,
                    new ushort[] { 0x00A9, 0x00AE, 0x203C, 0x2049, 0x2122, 0x2139, 0x2194, 0x2195, 0x2196, 0x2197, 0x2198, 0x2199, 0x21A9, 0x21AA, 0x2328, 0x23CF, 0x23ED, 0x23EE, 0x23EF, 0x23F1, 0x23F2, 0x23F8, 0x23F9, 0x23FA, 0x24C2, 0x25AA, 0x25AB, 0x25B6, 0x25C0, 0x25FB, 0x25FC, 0x2600, 0x2601, 0x2602, 0x2603, 0x2604, 0x260E, 0x2611, 0x2618, 0x261D, 0x2620, 0x2622, 0x2623, 0x2626, 0x262A, 0x262E, 0x262F, 0x2638, 0x2639, 0x263A, 0x2640, 0x2642, 0x265F, 0x2660, 0x2663, 0x2665, 0x2666, 0x2668, 0x267B, 0x267E, 0x2692, 0x2694, 0x2695, 0x2696, 0x2697, 0x2699, 0x269B, 0x269C, 0x26A0, 0x26A7, 0x26B0, 0x26B1, 0x26C8, 0x26CF, 0x26D1, 0x26D3, 0x26E9, 0x26F0, 0x26F1, 0x26F4, 0x26F7, 0x26F8, 0x26F9, 0x2702, 0x2708, 0x2709, 0x270C, 0x270D, 0x270F, 0x2712, 0x2714, 0x2716, 0x271D, 0x2721, 0x2733, 0x2734, 0x2744, 0x2747, 0x2763, 0x2764, 0x27A1, 0x2934, 0x2935, 0x2B05, 0x2B06, 0x2B07, 0x3030, 0x303D, 0x3297, 0x3299, },
                    new ushort[] { 12, 13, 3090, 3091, 3092, 3093, 3094, 3095, 3096, 3097, 3098, 3099, 3100, 3101, 3104, 3105, 3110, 3111, 3112, 3114, 3115, 3117, 3118, 3119, 3120, 3121, 3122, 3123, 3124, 3125, 3126, 3129, 3130, 3131, 3132, 3133, 3134, 3135, 3138, 3139, 3145, 3146, 3147, 3148, 3149, 3150, 3151, 3152, 3153, 3154, 3155, 3156, 3169, 3170, 3171, 3172, 3173, 3174, 3175, 3176, 3178, 3180, 3181, 3182, 3183, 3184, 3185, 3186, 3187, 3189, 3192, 3193, 3198, 3200, 3201, 3202, 3204, 3206, 3207, 3210, 3212, 3213, 3226, 3234, 3236, 3237, 3250, 3256, 3262, 3263, 3264, 3265, 3266, 3267, 3269, 3270, 3271, 3272, 3279, 3280, 3284, 3287, 3288, 3289, 3290, 3291, 3296, 3297, 3298, 3299, }),
                new(128,
                    new ushort[] { 0xDD70, 0xDD71, 0xDD7E, 0xDD7F, 0xDE02, 0xDE37, 0xDF21, 0xDF24, 0xDF25, 0xDF26, 0xDF27, 0xDF28, 0xDF29, 0xDF2A, 0xDF2B, 0xDF2C, 0xDF36, 0xDF7D, 0xDF96, 0xDF97, 0xDF99, 0xDF9A, 0xDF9B, 0xDF9E, 0xDF9F, 0xDFCB, 0xDFCC, 0xDFCD, 0xDFCE, 0xDFD4, 0xDFD5, 0xDFD6, 0xDFD7, 0xDFD8, 0xDFD9, 0xDFDA, 0xDFDB, 0xDFDC, 0xDFDD, 0xDFDE, 0xDFDF, 0xDFF3, 0xDFF5, 0xDFF7, },
                    new ushort[] { 16, 17, 18, 19, 290, 298, 337, 338, 339, 340, 341, 342, 343, 344, 345, 346, 356, 427, 455, 456, 457, 458, 459, 460, 461, 578, 596, 602, 603, 609, 610, 611, 612, 613, 614, 615, 616, 617, 618, 619, 620, 640, 646, 647, }),
                new(256,
                    new ushort[] { 0xDC3F, 0xDC41, 0xDCFD, 0xDD49, 0xDD4A, 0xDD6F, 0xDD70, 0xDD73, 0xDD74, 0xDD75, 0xDD76, 0xDD77, 0xDD78, 0xDD79, 0xDD87, 0xDD8A, 0xDD8B, 0xDD8C, 0xDD8D, 0xDD90, 0xDDA5, 0xDDA8, 0xDDB1, 0xDDB2, 0xDDBC, 0xDDC2, 0xDDC3, 0xDDC4, 0xDDD1, 0xDDD2, 0xDDD3, 0xDDDC, 0xDDDD, 0xDDDE, 0xDDE1, 0xDDE3, 0xDDE8, 0xDDEF, 0xDDF3, 0xDDFA, 0xDECB, 0xDECD, 0xDECE, 0xDECF, 0xDEE0, 0xDEE1, 0xDEE2, 0xDEE3, 0xDEE4, 0xDEE5, 0xDEE9, 0xDEF0, 0xDEF3, },
                    new ushort[] { 722, 725, 1592, 1656, 1657, 1686, 1687, 1688, 1689, 1707, 1713, 1714, 1715, 1716, 1723, 1724, 1725, 1726, 1727, 1728, 1747, 1748, 1749, 1750, 1751, 1752, 1753, 1754, 1755, 1756, 1757, 1758, 1759, 1760, 1761, 1762, 1763, 1764, 1765, 1766, 2107, 2114, 2115, 2116, 2123, 2124, 2125, 2126, 2127, 2128, 2129, 2132, 2133, }),
                null,
            },
        };
        private static StringDictionary _otherTable = new(
            "\uD83C\uDF85\uD83C\uDFC2\uD83C\uDFC3\u200D\u2640\uFE0F\uD83C\uDFC3\u200D\u2642\uFE0F\uD83C\uDFC3\uD83C\uDFC4\u200D\u2640\uFE0F\uD83C\uDFC4\u200D\u2642\uFE0F\uD83C\uDFC4\uD83C\uDFC7\uD83C\uDFCA\u200D\u2640\uFE0F\uD83C\uDFCA\u200D\u2642\uFE0F\uD83C\uDFCA\uD83C\uDFCB\uFE0F\u200D\u2640\uFE0F\uD83C\uDFCB\u200D\u2640\uFE0F\uD83C\uDFCB\uFE0F\u200D\u2642\uFE0F\uD83C\uDFCB\u200D\u2642\uFE0F\uD83C\uDFCB\uFE0F\uD83C\uDFCB\uD83C\uDFCC\uFE0F\u200D\u2640\uFE0F\uD83C\uDFCC\u200D\u2640\uFE0F\uD83C\uDFCC\uFE0F\u200D\u2642\uFE0F\uD83C\uDFCC\u200D\u2642\uFE0F\uD83C\uDFCC\uFE0F\uD83C\uDFCC\uD83C\uDFF3\uFE0F\u200D\uD83C\uDF08\uD83C\uDFF3\uFE0F\u200D\u26A7\uFE0F\uD83C\uDFF4\u200D\u2620\uFE0F\uD83D\uDC08\u200D\u2B1B\uD83D\uDC15\u200D\uD83E\uDDBA\uD83D\uDC3B\u200D\u2744\uFE0F\uD83D\uDC41\uFE0F\u200D\uD83D\uDDE8\uFE0F\uD83D\uDC42\uD83D\uDC43\uD83D\uDC46\uD83D\uDC47\uD83D\uDC48\uD83D\uDC49\uD83D\uDC4A\uD83D\uDC4B\uD83D\uDC4C\uD83D\uDC4D\uD83D\uDC4E\uD83D\uDC4F\uD83D\uDC50\uD83D\uDC66\uD83D\uDC67\uD83D\uDC68\u200D\uD83C\uDF3E\uD83D\uDC68\u200D\uD83C\uDF73\uD83D\uDC68\u200D\uD83C\uDF7C\uD83D\uDC68\u200D\uD83C\uDF93\uD83D\uDC68\u200D\uD83C\uDFA4\uD83D\uDC68\u200D\uD83C\uDFA8\uD83D\uDC68\u200D\uD83C\uDFEB\uD83D\uDC68\u200D\uD83C\uDFED\uD83D\uDC68\u200D\uD83D\uDC66\u200D\uD83D\uDC66\uD83D\uDC68\u200D\uD83D\uDC66\uD83D\uDC68\u200D\uD83D\uDC67\u200D\uD83D\uDC66\uD83D\uDC68\u200D\uD83D\uDC67\u200D\uD83D\uDC67\uD83D\uDC68\u200D\uD83D\uDC67\uD83D\uDC68\u200D\uD83D\uDC68\u200D\uD83D\uDC66\uD83D\uDC68\u200D\uD83D\uDC68\u200D\uD83D\uDC66\u200D\uD83D\uDC66\uD83D\uDC68\u200D\uD83D\uDC68\u200D\uD83D\uDC67\uD83D\uDC68\u200D\uD83D\uDC68\u200D\uD83D\uDC67\u200D\uD83D\uDC66\uD83D\uDC68\u200D\uD83D\uDC68\u200D\uD83D\uDC67\u200D\uD83D\uDC67\uD83D\uDC68\u200D\uD83D\uDC69\u200D\uD83D\uDC66\uD83D\uDC68\u200D\uD83D\uDC69\u200D\uD83D\uDC66\u200D\uD83D\uDC66\uD83D\uDC68\u200D\uD83D\uDC69\u200D\uD83D\uDC67\uD83D\uDC68\u200D\uD83D\uDC69\u200D\uD83D\uDC67\u200D\uD83D\uDC66\uD83D\uDC68\u200D\uD83D\uDC69\u200D\uD83D\uDC67\u200D\uD83D\uDC67\uD83D\uDC68\u200D\uD83D\uDCBB\uD83D\uDC68\u200D\uD83D\uDCBC\uD83D\uDC68\u200D\uD83D\uDD27\uD83D\uDC68\u200D\uD83D\uDD2C\uD83D\uDC68\u200D\uD83D\uDE80\uD83D\uDC68\u200D\uD83D\uDE92\uD83D\uDC68\u200D\uD83E\uDDAF\uD83D\uDC68\u200D\uD83E\uDDB0\uD83D\uDC68\u200D\uD83E\uDDB1\uD83D\uDC68\u200D\uD83E\uDDB2\uD83D\uDC68\u200D\uD83E\uDDB3\uD83D\uDC68\u200D\uD83E\uDDBC\uD83D\uDC68\u200D\uD83E\uDDBD\uD83D\uDC68\u200D\u2695\uFE0F\uD83D\uDC68\u200D\u2696\uFE0F\uD83D\uDC68\u200D\u2708\uFE0F\uD83D\uDC68\u200D\u2764\uFE0F\u200D\uD83D\uDC68\uD83D\uDC68\u200D\u2764\uFE0F\u200D\uD83D\uDC8B\u200D\uD83D\uDC68\uD83D\uDC68\uD83D\uDC69\u200D\uD83C\uDF3E\uD83D\uDC69\u200D\uD83C\uDF73\uD83D\uDC69\u200D\uD83C\uDF7C\uD83D\uDC69\u200D\uD83C\uDF93\uD83D\uDC69\u200D\uD83C\uDFA4\uD83D\uDC69\u200D\uD83C\uDFA8\uD83D\uDC69\u200D\uD83C\uDFEB\uD83D\uDC69\u200D\uD83C\uDFED\uD83D\uDC69\u200D\uD83D\uDC66\u200D\uD83D\uDC66\uD83D\uDC69\u200D\uD83D\uDC66\uD83D\uDC69\u200D\uD83D\uDC67\u200D\uD83D\uDC66\uD83D\uDC69\u200D\uD83D\uDC67\u200D\uD83D\uDC67\uD83D\uDC69\u200D\uD83D\uDC67\uD83D\uDC69\u200D\uD83D\uDC69\u200D\uD83D\uDC66\uD83D\uDC69\u200D\uD83D\uDC69\u200D\uD83D\uDC66\u200D\uD83D\uDC66\uD83D\uDC69\u200D\uD83D\uDC69\u200D\uD83D\uDC67\uD83D\uDC69\u200D\uD83D\uDC69\u200D\uD83D\uDC67\u200D\uD83D\uDC66\uD83D\uDC69\u200D\uD83D\uDC69\u200D\uD83D\uDC67\u200D\uD83D\uDC67\uD83D\uDC69\u200D\uD83D\uDCBB\uD83D\uDC69\u200D\uD83D\uDCBC\uD83D\uDC69\u200D\uD83D\uDD27\uD83D\uDC69\u200D\uD83D\uDD2C\uD83D\uDC69\u200D\uD83D\uDE80\uD83D\uDC69\u200D\uD83D\uDE92\uD83D\uDC69\u200D\uD83E\uDDAF\uD83D\uDC69\u200D\uD83E\uDDB0\uD83D\uDC69\u200D\uD83E\uDDB1\uD83D\uDC69\u200D\uD83E\uDDB2\uD83D\uDC69\u200D\uD83E\uDDB3\uD83D\uDC69\u200D\uD83E\uDDBC\uD83D\uDC69\u200D\uD83E\uDDBD\uD83D\uDC69\u200D\u2695\uFE0F\uD83D\uDC69\u200D\u2696\uFE0F\uD83D\uDC69\u200D\u2708\uFE0F\uD83D\uDC69\u200D\u2764\uFE0F\u200D\uD83D\uDC68\uD83D\uDC69\u200D\u2764\uFE0F\u200D\uD83D\uDC69\uD83D\uDC69\u200D\u2764\uFE0F\u200D\uD83D\uDC8B\u200D\uD83D\uDC68\uD83D\uDC69\u200D\u2764\uFE0F\u200D\uD83D\uDC8B\u200D\uD83D\uDC69\uD83D\uDC69\uD83D\uDC6B\uD83D\uDC69\u200D\uD83E\uDD1D\u200D\uD83D\uDC68\uD83D\uDC6C\uD83D\uDC68\u200D\uD83E\uDD1D\u200D\uD83D\uDC68\uD83D\uDC6D\uD83D\uDC69\u200D\uD83E\uDD1D\u200D\uD83D\uDC69\uD83D\uDC6E\u200D\u2640\uFE0F\uD83D\uDC6E\u200D\u2642\uFE0F\uD83D\uDC6E\uD83D\uDC6F\u200D\u2640\uFE0F\uD83D\uDC6F\u200D\u2642\uFE0F\uD83D\uDC70\u200D\u2640\uFE0F\uD83D\uDC70\u200D\u2642\uFE0F\uD83D\uDC70\uD83D\uDC71\u200D\u2640\uFE0F\uD83D\uDC71\u200D\u2642\uFE0F\uD83D\uDC71\uD83D\uDC72\uD83D\uDC73\u200D\u2640\uFE0F\uD83D\uDC73\u200D\u2642\uFE0F\uD83D\uDC73\uD83D\uDC74\uD83D\uDC75\uD83D\uDC76\uD83D\uDC77\u200D\u2640\uFE0F\uD83D\uDC77\u200D\u2642\uFE0F\uD83D\uDC77\uD83D\uDC78\uD83D\uDC7C\uD83D\uDC81\u200D\u2640\uFE0F\uD83D\uDC81\u200D\u2642\uFE0F\uD83D\uDC81\uD83D\uDC82\u200D\u2640\uFE0F\uD83D\uDC82\u200D\u2642\uFE0F\uD83D\uDC82\uD83D\uDC83\uD83D\uDC85\uD83D\uDC86\u200D\u2640\uFE0F\uD83D\uDC86\u200D\u2642\uFE0F\uD83D\uDC86\uD83D\uDC87\u200D\u2640\uFE0F\uD83D\uDC87\u200D\u2642\uFE0F\uD83D\uDC87\uD83D\uDCAA\uD83D\uDD74\uFE0F\uD83D\uDD74\uD83D\uDD75\uFE0F\u200D\u2640\uFE0F\uD83D\uDD75\u200D\u2640\uFE0F\uD83D\uDD75\uFE0F\u200D\u2642\uFE0F\uD83D\uDD75\u200D\u2642\uFE0F\uD83D\uDD75\uFE0F\uD83D\uDD75\uD83D\uDD7A\uD83D\uDD90\uFE0F\uD83D\uDD90\uD83D\uDD95\uD83D\uDD96\uD83D\uDE45\u200D\u2640\uFE0F\uD83D\uDE45\u200D\u2642\uFE0F\uD83D\uDE45\uD83D\uDE46\u200D\u2640\uFE0F\uD83D\uDE46\u200D\u2642\uFE0F\uD83D\uDE46\uD83D\uDE47\u200D\u2640\uFE0F\uD83D\uDE47\u200D\u2642\uFE0F\uD83D\uDE47\uD83D\uDE4B\u200D\u2640\uFE0F\uD83D\uDE4B\u200D\u2642\uFE0F\uD83D\uDE4B\uD83D\uDE4C\uD83D\uDE4D\u200D\u2640\uFE0F\uD83D\uDE4D\u200D\u2642\uFE0F\uD83D\uDE4D\uD83D\uDE4E\u200D\u2640\uFE0F\uD83D\uDE4E\u200D\u2642\uFE0F\uD83D\uDE4E\uD83D\uDE4F\uD83D\uDEA3\u200D\u2640\uFE0F\uD83D\uDEA3\u200D\u2642\uFE0F\uD83D\uDEA3\uD83D\uDEB4\u200D\u2640\uFE0F\uD83D\uDEB4\u200D\u2642\uFE0F\uD83D\uDEB4\uD83D\uDEB5\u200D\u2640\uFE0F\uD83D\uDEB5\u200D\u2642\uFE0F\uD83D\uDEB5\uD83D\uDEB6\u200D\u2640\uFE0F\uD83D\uDEB6\u200D\u2642\uFE0F\uD83D\uDEB6\uD83D\uDEC0\uD83D\uDECC\uD83E\uDD0C\uD83E\uDD0F\uD83E\uDD18\uD83E\uDD19\uD83E\uDD1A\uD83E\uDD1B\uD83E\uDD1C\uD83E\uDD1E\uD83E\uDD1F\uD83E\uDD26\u200D\u2640\uFE0F\uD83E\uDD26\u200D\u2642\uFE0F\uD83E\uDD26\uD83E\uDD30\uD83E\uDD31\uD83E\uDD32\uD83E\uDD33\uD83E\uDD34\uD83E\uDD35\u200D\u2640\uFE0F\uD83E\uDD35\u200D\u2642\uFE0F\uD83E\uDD35\uD83E\uDD36\uD83E\uDD37\u200D\u2640\uFE0F\uD83E\uDD37\u200D\u2642\uFE0F\uD83E\uDD37\uD83E\uDD38\u200D\u2640\uFE0F\uD83E\uDD38\u200D\u2642\uFE0F\uD83E\uDD38\uD83E\uDD39\u200D\u2640\uFE0F\uD83E\uDD39\u200D\u2642\uFE0F\uD83E\uDD39\uD83E\uDD3C\u200D\u2640\uFE0F\uD83E\uDD3C\u200D\u2642\uFE0F\uD83E\uDD3D\u200D\u2640\uFE0F\uD83E\uDD3D\u200D\u2642\uFE0F\uD83E\uDD3D\uD83E\uDD3E\u200D\u2640\uFE0F\uD83E\uDD3E\u200D\u2642\uFE0F\uD83E\uDD3E\uD83E\uDD77\uD83E\uDDB5\uD83E\uDDB6\uD83E\uDDB8\u200D\u2640\uFE0F\uD83E\uDDB8\u200D\u2642\uFE0F\uD83E\uDDB8\uD83E\uDDB9\u200D\u2640\uFE0F\uD83E\uDDB9\u200D\u2642\uFE0F\uD83E\uDDB9\uD83E\uDDBB\uD83E\uDDCD\u200D\u2640\uFE0F\uD83E\uDDCD\u200D\u2642\uFE0F\uD83E\uDDCD\uD83E\uDDCE\u200D\u2640\uFE0F\uD83E\uDDCE\u200D\u2642\uFE0F\uD83E\uDDCE\uD83E\uDDCF\u200D\u2640\uFE0F\uD83E\uDDCF\u200D\u2642\uFE0F\uD83E\uDDCF\uD83E\uDDD1\u200D\uD83C\uDF3E\uD83E\uDDD1\u200D\uD83C\uDF73\uD83E\uDDD1\u200D\uD83C\uDF7C\uD83E\uDDD1\u200D\uD83C\uDF84\uD83E\uDDD1\u200D\uD83C\uDF93\uD83E\uDDD1\u200D\uD83C\uDFA4\uD83E\uDDD1\u200D\uD83C\uDFA8\uD83E\uDDD1\u200D\uD83C\uDFEB\uD83E\uDDD1\u200D\uD83C\uDFED\uD83E\uDDD1\u200D\uD83D\uDCBB\uD83E\uDDD1\u200D\uD83D\uDCBC\uD83E\uDDD1\u200D\uD83D\uDD27\uD83E\uDDD1\u200D\uD83D\uDD2C\uD83E\uDDD1\u200D\uD83D\uDE80\uD83E\uDDD1\u200D\uD83D\uDE92\uD83E\uDDD1\u200D\uD83E\uDD1D\u200D\uD83E\uDDD1\uD83E\uDDD1\u200D\uD83E\uDDAF\uD83E\uDDD1\u200D\uD83E\uDDB0\uD83E\uDDD1\u200D\uD83E\uDDB1\uD83E\uDDD1\u200D\uD83E\uDDB2\uD83E\uDDD1\u200D\uD83E\uDDB3\uD83E\uDDD1\u200D\uD83E\uDDBC\uD83E\uDDD1\u200D\uD83E\uDDBD\uD83E\uDDD1\u200D\u2695\uFE0F\uD83E\uDDD1\u200D\u2696\uFE0F\uD83E\uDDD1\u200D\u2708\uFE0F\uD83E\uDDD1\uD83E\uDDD2\uD83E\uDDD3\uD83E\uDDD4\uD83E\uDDD5\uD83E\uDDD6\u200D\u2640\uFE0F\uD83E\uDDD6\u200D\u2642\uFE0F\uD83E\uDDD6\uD83E\uDDD7\u200D\u2640\uFE0F\uD83E\uDDD7\u200D\u2642\uFE0F\uD83E\uDDD7\uD83E\uDDD8\u200D\u2640\uFE0F\uD83E\uDDD8\u200D\u2642\uFE0F\uD83E\uDDD8\uD83E\uDDD9\u200D\u2640\uFE0F\uD83E\uDDD9\u200D\u2642\uFE0F\uD83E\uDDD9\uD83E\uDDDA\u200D\u2640\uFE0F\uD83E\uDDDA\u200D\u2642\uFE0F\uD83E\uDDDA\uD83E\uDDDB\u200D\u2640\uFE0F\uD83E\uDDDB\u200D\u2642\uFE0F\uD83E\uDDDB\uD83E\uDDDC\u200D\u2640\uFE0F\uD83E\uDDDC\u200D\u2642\uFE0F\uD83E\uDDDC\uD83E\uDDDD\u200D\u2640\uFE0F\uD83E\uDDDD\u200D\u2642\uFE0F\uD83E\uDDDD\uD83E\uDDDE\u200D\u2640\uFE0F\uD83E\uDDDE\u200D\u2642\uFE0F\uD83E\uDDDF\u200D\u2640\uFE0F\uD83E\uDDDF\u200D\u2642\uFE0F\u261D\uFE0F\u261D\u26F9\uFE0F\u200D\u2640\uFE0F\u26F9\u200D\u2640\uFE0F\u26F9\uFE0F\u200D\u2642\uFE0F\u26F9\u200D\u2642\uFE0F\u26F9\uFE0F\u26F9\u270A\u270B\u270C\uFE0F\u270C\u270D\uFE0F\u270D",
            new byte[] { 2, 2, 5, 5, 2, 5, 5, 2, 2, 5, 5, 2, 6, 5, 6, 5, 3, 2, 6, 5, 6, 5, 3, 2, 6, 6, 5, 4, 5, 5, 7, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 5, 5, 5, 5, 8, 5, 8, 8, 5, 8, 11, 8, 11, 11, 8, 11, 8, 11, 11, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 8, 11, 2, 5, 5, 5, 5, 5, 5, 5, 5, 8, 5, 8, 8, 5, 8, 11, 8, 11, 11, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 8, 8, 11, 11, 2, 2, 8, 2, 8, 2, 8, 5, 5, 2, 5, 5, 5, 5, 2, 5, 5, 2, 2, 5, 5, 2, 2, 2, 2, 5, 5, 2, 2, 2, 5, 5, 2, 5, 5, 2, 2, 2, 5, 5, 2, 5, 5, 2, 2, 3, 2, 6, 5, 6, 5, 3, 2, 2, 3, 2, 2, 2, 5, 5, 2, 5, 5, 2, 5, 5, 2, 5, 5, 2, 2, 5, 5, 2, 5, 5, 2, 2, 5, 5, 2, 5, 5, 2, 5, 5, 2, 5, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 2, 2, 2, 2, 2, 2, 5, 5, 2, 2, 5, 5, 2, 5, 5, 2, 5, 5, 2, 5, 5, 5, 5, 2, 5, 5, 2, 2, 2, 2, 5, 5, 2, 5, 5, 2, 2, 5, 5, 2, 5, 5, 2, 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 8, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 2, 2, 2, 2, 5, 5, 2, 5, 5, 2, 5, 5, 2, 5, 5, 2, 5, 5, 2, 5, 5, 2, 5, 5, 2, 5, 5, 2, 5, 5, 5, 5, 2, 1, 5, 4, 5, 4, 2, 1, 1, 1, 2, 1, 2, 1, },
            new ushort[] { 435, 496, 502, 508, 514, 520, 526, 532, 540, 548, 554, 560, 566, 566, 572, 572, 578, 578, 584, 584, 590, 590, 596, 596, 638, 639, 641, 664, 678, 717, 724, 726, 732, 740, 746, 752, 758, 764, 770, 776, 782, 788, 794, 800, 827, 833, 839, 845, 851, 857, 863, 869, 875, 881, 887, 888, 889, 890, 891, 892, 893, 894, 895, 896, 897, 898, 899, 900, 901, 902, 908, 914, 920, 926, 932, 938, 944, 950, 956, 962, 968, 974, 980, 986, 992, 998, 999, 1000, 1006, 1012, 1018, 1024, 1030, 1036, 1042, 1048, 1054, 1055, 1056, 1057, 1058, 1059, 1060, 1061, 1062, 1063, 1064, 1070, 1076, 1082, 1088, 1094, 1100, 1106, 1112, 1118, 1124, 1130, 1136, 1142, 1148, 1154, 1160, 1161, 1162, 1163, 1164, 1171, 1171, 1197, 1197, 1223, 1223, 1249, 1255, 1261, 1267, 1268, 1270, 1276, 1282, 1288, 1294, 1300, 1306, 1312, 1318, 1324, 1330, 1336, 1342, 1348, 1354, 1360, 1366, 1375, 1385, 1391, 1397, 1403, 1409, 1415, 1421, 1428, 1434, 1440, 1446, 1452, 1458, 1464, 1504, 1689, 1689, 1695, 1695, 1701, 1701, 1707, 1707, 1717, 1728, 1728, 1734, 1740, 1841, 1847, 1853, 1859, 1865, 1871, 1877, 1883, 1889, 1898, 1904, 1910, 1916, 1922, 1928, 1934, 1940, 1946, 1952, 1958, 1999, 2005, 2011, 2033, 2039, 2045, 2051, 2057, 2063, 2069, 2075, 2081, 2096, 2108, 2155, 2163, 2177, 2183, 2189, 2195, 2201, 2208, 2214, 2226, 2232, 2238, 2253, 2259, 2265, 2271, 2277, 2283, 2289, 2295, 2301, 2307, 2313, 2319, 2325, 2331, 2337, 2343, 2349, 2355, 2362, 2363, 2365, 2371, 2377, 2383, 2389, 2395, 2456, 2518, 2524, 2531, 2537, 2543, 2549, 2555, 2561, 2568, 2590, 2596, 2602, 2608, 2614, 2620, 2626, 2632, 2638, 2645, 2651, 2657, 2663, 2669, 2675, 2681, 2687, 2693, 2699, 2705, 2711, 2717, 2723, 2729, 2735, 2761, 2767, 2773, 2779, 2785, 2791, 2797, 2803, 2809, 2815, 2821, 2827, 2833, 2839, 2845, 2851, 2857, 2863, 2869, 2875, 2881, 2887, 2893, 2899, 2905, 2911, 2917, 2923, 2929, 2935, 2941, 2947, 2953, 2959, 2965, 2971, 2977, 2983, 2989, 2995, 2996, 2998, 2999, 3139, 3139, 3214, 3214, 3220, 3220, 3226, 3226, 3238, 3244, 3250, 3250, 3256, 3256, },
            new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 3, 1, 3, 1, 3, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, }
            );
    }
}
