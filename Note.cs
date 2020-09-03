using System;
using System.Linq;
using System.Collections.Generic;

namespace MusicGenerator
{
    class Note
    {
        public readonly char NoteName;
        public readonly string Accidental;
        public readonly int Octave;
        public readonly double Duration;
        public readonly string RhythmicValue;

        // Generates randomly:
        public Note(KeySignature keySig)
        {
            Theory theory = new Theory();
            Random random = new Random();
            int[] octaves = {5, 6};

            NoteName = theory.CircleOfFifths[random.Next(0, theory.CircleOfFifths.Length)];
            Octave = octaves[random.Next(0, octaves.Length)];

            int numAccidentals = keySig.GetNumAccidentals();
            if (numAccidentals > 0)
            {
                Accidental = Array.IndexOf(theory.OrderOfSharps, NoteName) < numAccidentals ? "#" : "";
            }
            else
            {
                Accidental = Array.IndexOf(theory.OrderOfFlats, NoteName) < Math.Abs(numAccidentals) ? "b" : "";
            }

            KeyValuePair<string, double> durationKVP = theory.Durations.ElementAt(random.Next(0, theory.Durations.Count)); 
            Duration = durationKVP.Value;
            RhythmicValue = durationKVP.Key;
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