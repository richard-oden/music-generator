using System;
using static MusicGenerator.Theory;

namespace MusicGenerator
{
    public class Note : MeasureSegment
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

            applyKeySignature(keySig);

            // Represents position on staff (0 to 13 from top to bottom):
            StaffLine = 13 - Array.IndexOf(Scale, NoteName);
            if (Octave == 5) StaffLine -= 7;
        }

        //Generates with given values:
        public Note(KeySignature keySig, int staffLine, string staffSymbol) : base(staffSymbol)
        {
            StaffLine = staffLine;
            int scaleIndex = 13 - staffLine;
            if (scaleIndex >= 7)
            {
                scaleIndex -= 7;
                Octave = 5;
            }
            else
            {
                Octave = 4;
            }
            NoteName = Scale[scaleIndex];
            applyKeySignature(keySig);
        }

        private void applyKeySignature(KeySignature keySig)
        {
            if (keySig.TypeOfAccidental == "#")
            {
                Accidental = Array.IndexOf(OrderOfSharps, NoteName) < keySig.NumAccidentals ? "#" : "";
            }
            else
            {
                Accidental = Array.IndexOf(OrderOfFlats, NoteName) < Math.Abs(keySig.NumAccidentals) ? "b" : "";
            }
        }
    }
}