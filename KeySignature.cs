using System;

namespace MusicGenerator
{
    class KeySignature
    {
        public readonly char Tonic;
        public readonly string Accidental;
        public readonly string Mode;

        // Generates randomly:
        public KeySignature() 
        {   
            Theory theory = new Theory();
            Random random = new Random();
            string[] accidentals = {"b", "#", ""};
            string[] modes = {"Major", "Minor"};

            Tonic = theory.CircleOfFifths[random.Next(0, theory.CircleOfFifths.Length)];
            Accidental = accidentals[random.Next(0, accidentals.Length)];
            Mode = modes[random.Next(0, modes.Length)];
        }
        // Generates with given values:
        public KeySignature(char tonic, string accidental, string mode)
        {
            Tonic = tonic;
            Accidental = accidental;
            Mode = mode;
        }
        
        // Returns number of flats or sharps (flats are negative, sharps are positive):
        public int GetNumAccidentals()
        {
            Theory theory = new Theory();
            int num = 0;
            if (Accidental == "#") num = 7;
            if (Accidental == "b") num = -7;
            if (Mode == "Minor") num -= 3;
            foreach (char n in theory.CircleOfFifths)
            {
                if (n == Tonic) break;
                num++;
            }
            return num;
        }
    }
}