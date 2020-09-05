using System;
using System.Linq;

namespace MusicGenerator
{
    class Note
    {
        public char NoteName {get; private set;}
        public string Accidental {get; private set;}
        public int Octave {get; private set;}
        public int StaffLine {get; private set;}
        public double Duration {get; private set;}
        public string RhythmicValue {get; private set;}
        public string StaffNote {get; private set;}

        // Generates randomly:
        public Note(KeySignature keySig)
        {
            Theory theory = new Theory();
            Random random = new Random();
            int[] octaves = {4, 5};

            NoteName = theory.CircleOfFifths[random.Next(0, theory.CircleOfFifths.Length)];
            Octave = octaves[random.Next(0, octaves.Length)];

            if (keySig.typeOfAccidental == "#")
            {
                Accidental = Array.IndexOf(theory.OrderOfSharps, NoteName) < keySig.numAccidentals ? "#" : "";
            }
            else
            {
                Accidental = Array.IndexOf(theory.OrderOfFlats, NoteName) < Math.Abs(keySig.numAccidentals) ? "b" : "";
            }

            // Represents position on staff (0 to 13 from top to bottom):
            StaffLine = 13 - Array.IndexOf(theory.Scale, NoteName);
            if (Octave == 5) StaffLine -= 7;


            object[] durationArray = theory.Durations[random.Next(0, theory.Durations.Length)]; 
            Duration = (double)durationArray[2];
            RhythmicValue = (string)durationArray[0];
            StaffNote = (string)durationArray[1];
        }

        // Generates with given values:
        public Note(char noteName, string accidental, int octave, double duration)
        {
            NoteName = noteName;
            Accidental = accidental;
            Octave = octave;
            Duration = duration;
        }
    }
}