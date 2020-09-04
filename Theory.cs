using System.Collections.Generic;

namespace MusicGenerator
{
    class Theory
    {
        public char[] CircleOfFifths {get; private set;}
        public char[] OrderOfSharps {get; private set;}
        public char[] OrderOfFlats {get; private set;}
        public Dictionary<string, double> Durations {get; private set;}

        public Theory()
        {
            CircleOfFifths = new[] { 'C', 'G', 'D', 'A', 'E', 'B', 'F' };
            OrderOfSharps = new[] { 'F', 'C', 'G', 'D', 'A', 'E', 'B' };
            OrderOfFlats = new[] { 'B', 'E', 'A', 'D', 'G', 'C', 'F' };           
            Durations = new Dictionary<string, double>() 
            {
                {"1/16th note", 0.0625},
                {"1/8th note", 0.125},
                {"dotted 1/8th note", 0.1875},
                {"quarter note", 0.25},
                {"dotted quarter note", 0.375},
                {"half note", 0.5},
                {"dotted half note", 0.75},
                {"whole note", 1},
                {"dotted whole note", 1.5}
            };
        }
    }
}