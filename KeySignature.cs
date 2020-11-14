using System;
using static MusicGenerator.Theory;
using System.Linq;

namespace MusicGenerator
{
    public class KeySignature
    {
        public char Tonic {get; private set;}
        public string Accidental {get; private set;} // Accidental present in key signature name
        public string Mode {get; private set;}
        // Returns number of flats or sharps (flats are negative, sharps are positive):
        public int NumAccidentals
        {
            get
            {
                int num = 0;
                if (Accidental == "#") num = Tonic == 'F' ? 0 : 7;
                if (Accidental == "b") num = -7;
                if (Mode == "Minor") num -= 3;
                foreach (char n in CircleOfFifths)
                {
                    if (n == Tonic) break;
                    num++;
                }
                return num;
            }
        }
        public string TypeOfAccidental => NumAccidentals > 0 ? "#" : "b"; // Accidental applied to notes
        private static readonly Random _random = new Random();

        // Generates randomly:
        public KeySignature() 
        {   
            Accidental = Accidentals[_random.Next(0, Accidentals.Length)];
            // If sharp is in name, tonic cannot be further in circle of fifths than E or there will be double sharps:
            char tempTonic;
            do
            {
                tempTonic = CircleOfFifths[_random.Next(0, Scale.Length)];
            } while (Array.IndexOf(OrderOfSharps, tempTonic) > 4 && Accidental == "#");
            Tonic = tempTonic;
            
            // Prevent more than 7 sharps (no double sharps)
            if (Array.IndexOf(OrderOfSharps, Tonic) > 1 && Accidental == "#")
            {
                Mode = "Minor";
            }
            // Prevent more than 7 flats (no double flats)
            else if (Array.IndexOf(OrderOfFlats, Tonic) > 2 && Accidental == "b")
            {
                Mode = "Major";
            }
            else
            {
                Mode = Modes[_random.Next(0, Modes.Length)];
            }
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
                var tonic = char.ToUpper(tonicAndAccidentalArr[0]);
                var accidental = tonicAndAccidentalArr.Length == 2 ? tonicAndAccidentalArr[1].ToString() : "";
                var mode = inputArr[1].ToLower().FirstCharToUpper();

                if (Scale.Contains(tonic) && Accidentals.Contains(accidental) && 
                    Modes.Contains(mode))
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