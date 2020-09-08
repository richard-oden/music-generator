using System;

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

        // Generates randomly:
        public KeySignature() 
        {   
            Theory theory = new Theory();
            Random random = new Random();
            string[] accidentals = {"#", "b", ""};
            string[] modes = {"Major", "Minor"};

            Tonic = theory.CircleOfFifths[random.Next(0, theory.Scale.Length)];
            Accidental = accidentals[random.Next(0, accidentals.Length)];
            // Prevent more than 7 sharps (no double sharps)
            for (int i = 2; i < theory.OrderOfSharps.Length; i++)
            {
                if (theory.OrderOfSharps[i] == Tonic)
                {
                    Accidental = accidentals[random.Next(1, 2)];
                    break;
                }
            }

            Mode = modes[random.Next(0, modes.Length)];
            // Prevent more than 7 flats (no double flats)
            if (Accidental == "b")
            {
                for (int i = 3; i < theory.OrderOfFlats.Length; i++)
                {
                    if (theory.OrderOfFlats[i] == Tonic)
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
            foreach (char n in theory.CircleOfFifths)
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