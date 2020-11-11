using System;
using static MusicGenerator.Theory;
using System.Linq;

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
            Tonic = CircleOfFifths[_random.Next(0, Scale.Length)];
            Accidental = Accidentals[_random.Next(0, Accidentals.Length)];
            // Prevent more than 7 sharps (no double sharps)
            for (int i = 2; i < OrderOfSharps.Length; i++)
            {
                if (OrderOfSharps[i] == Tonic)
                {
                    Accidental = Accidentals[_random.Next(1, 2)];
                    break;
                }
            }

            Mode = Modes[_random.Next(0, Modes.Length)];
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

        public static KeySignature Parse(string input)
        {
            var inputArr = input.Split(' ');
            var tonicAndAccidentalArr = inputArr[0].ToCharArray();
            
            if (inputArr.Length == 2 && 
                (tonicAndAccidentalArr.Length == 2 ||
                tonicAndAccidentalArr.Length == 1))
            {
                var tonic = tonicAndAccidentalArr[0];
                var accidental = tonicAndAccidentalArr.Length == 2 ? tonicAndAccidentalArr[1].ToString() : "";
                var mode = inputArr[1];

                if (Scale.Contains(tonic) && Accidentals.Contains(accidental) && 
                    Modes.Contains(mode.ToLower().FirstCharToUpper()))
                {
                    return new KeySignature(tonic, accidental, mode);
                }
                else
                {
                    Console.WriteLine($"One or more parts of '{input}' are not valid.");
                }
            }
            else
            {
                Console.WriteLine($"'{input}' is not a valid key signature. Must contain a tonic and mode separated by a space. (e.g., Bb Major)");
            }
            return null;
        }
    }
}