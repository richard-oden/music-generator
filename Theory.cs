using System.Collections.Generic;

namespace MusicGenerator
{
    class Theory
    {
        public char[] CircleOfFifths {get; private set;}
        public char[] OrderOfSharps {get; private set;}
        public char[] OrderOfFlats {get; private set;}
        public char[] Scale {get; private set;}
        public object[][] Durations {get; private set;}

        public Theory()
        {
            CircleOfFifths = new[] { 'C', 'G', 'D', 'A', 'E', 'B', 'F' };
            OrderOfSharps = new[] { 'F', 'C', 'G', 'D', 'A', 'E', 'B' };
            OrderOfFlats = new[] { 'B', 'E', 'A', 'D', 'G', 'C', 'F' };
            Scale = new[] { 'C', 'D', 'E', 'F', 'G', 'A', 'B' };     
            Durations = new object[][]
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
}