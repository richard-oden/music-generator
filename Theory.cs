namespace MusicGenerator
{
    static class Theory
    {
        public static char[] CircleOfFifths => new[] { 'C', 'G', 'D', 'A', 'E', 'B', 'F' };
        public static char[] OrderOfSharps => new[] { 'F', 'C', 'G', 'D', 'A', 'E', 'B' };
        public static char[] OrderOfFlats => new[] { 'B', 'E', 'A', 'D', 'G', 'C', 'F' };
        public static char[] Scale => new[] { 'C', 'D', 'E', 'F', 'G', 'A', 'B' };
        public static string[] Accidentals => new[] {"#", "b", ""};
        public static string[] Modes => new[] {"Major", "Minor"};
        public static int[] TimeSigNumerators => new[] {2, 3, 4, 6};
        public static int[] TimeSigDenominators => new[] {2, 4, 8};
        public static object[][] RhythmInfos => new object[][]
        {
            new object[] {"1/16th note", "S", 0.0625},
            new object[] {"1/8th note", "E", 0.125},
            new object[] {"dotted 1/8th note", "E.", 0.1875},
            new object[] {"quarter note", "Q", 0.25},
            new object[] {"dotted quarter note", "Q.", 0.375},
            new object[] {"half note", "H", 0.5,},
            new object[] {"dotted half note", "H.", 0.75},
            new object[] {"whole note", "W", 1.0},
            new object[] {"dotted whole note", "W.", 1.5}
        };
    }
}