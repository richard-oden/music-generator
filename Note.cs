using System;
using System.Linq;
using System.Collections.Generic;

namespace MusicGenerator
{
    class Note
    {
        public char NoteName {get; private set;}
        public string Accidental {get; private set;}
        public int Octave {get; private set;}
        public double Duration {get; private set;}
        public string RhythmicValue {get; private set;}

        // Generates randomly:
        public Note(KeySignature keySig)
        {
            Theory theory = new Theory();
            Random random = new Random();
            int[] octaves = {5, 6};

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