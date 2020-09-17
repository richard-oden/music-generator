using System;
using static MusicGenerator.Theory;

namespace MusicGenerator
{
    class Note : MeasureSegment
    {
        public char NoteName {get; private set;}
        public string Accidental {get; private set;}
        public int Octave {get; private set;}

        // Generates randomly:
        public Note(KeySignature keySig) : base()
        {
            int[] octaves = {4, 5};

            NoteName = CircleOfFifths[_random.Next(0, CircleOfFifths.Length)];
            Octave = octaves[_random.Next(0, octaves.Length)];

            if (keySig.TypeOfAccidental == "#")
            {
                Accidental = Array.IndexOf(OrderOfSharps, NoteName) < keySig.NumAccidentals ? "#" : "";
            }
            else
            {
                Accidental = Array.IndexOf(OrderOfFlats, NoteName) < Math.Abs(keySig.NumAccidentals) ? "b" : "";
            }

            // Represents position on staff (0 to 13 from top to bottom):
            StaffLine = 13 - Array.IndexOf(Scale, NoteName);
            if (Octave == 5) StaffLine -= 7;
        }

        // Generates with given values:
        // public Note(char noteName, string accidental, int octave, double duration)
        // {
        //     NoteName = noteName;
        //     Accidental = accidental;
        //     Octave = octave;
        //     Duration = duration;
        // }
    }
}