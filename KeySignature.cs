using System;
using static MusicGenerator.Theory;

namespace MusicGenerator
{
    class KeySignature
    {
        public char Tonic {get; private set;}
        public string Accidental {get; private set;}
        public string Mode {get; private set;}
        // Returns number of flats or sharps (flats are negative, sharps are positive):
        public int NumAccidentals {get; private set;}
        public string TypeOfAccidental => NumAccidentals > 0 ? "#" : "b";
        private static readonly Random _random = new Random();

        // Generates randomly:
        public KeySignature() 
        {   
            string[] accidentals = {"#", "b", ""};
            string[] modes = {"Major", "Minor"};

            Tonic = CircleOfFifths[_random.Next(0, Scale.Length)];
            Accidental = accidentals[_random.Next(0, accidentals.Length)];
            // Prevent more than 7 sharps (no double sharps)
            for (int i = 2; i < OrderOfSharps.Length; i++)
            {
                if (OrderOfSharps[i] == Tonic)
                {
                    Accidental = accidentals[_random.Next(1, 2)];
                    break;
                }
            }

            Mode = modes[_random.Next(0, modes.Length)];
            // Prevent more than 7 flats (no double flats)
            if (Accidental == "b")
            {
                for (int i = 3; i < OrderOfFlats.Length; i++)
                {
                    if (OrderOfFlats[i] == Tonic)
                    {
                        Mode = "Major";
                        break;
                    }
                }
            }
            
            // Get nomber of accidentals:
            int num = 0;
            if (Accidental == "#") num = 7;
            if (Accidental == "b") num = -7;
            if (Mode == "Minor") num -= 3;
            foreach (char n in CircleOfFifths)
            {
                if (n == Tonic) break;
                num++;
            }
            NumAccidentals = num;
        }
        // Generates with given values:
        public KeySignature(char tonic, string accidental, string mode)
        {
            Tonic = tonic;
            Accidental = accidental;
            Mode = mode;
        }
    }
}