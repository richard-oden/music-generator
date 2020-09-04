using System;

namespace MusicGenerator
{
    class KeySignature
    {
        public char Tonic {get; private set;}
        public string Accidental {get; private set;}
        public string Mode {get; private set;}
        // Returns number of flats or sharps (flats are negative, sharps are positive):
        public int numAccidentals
        {
            get
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
        public string typeOfAccidental => numAccidentals > 0 ? "#" : "b";

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
    }
}