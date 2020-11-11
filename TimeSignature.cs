using System;
using static MusicGenerator.Theory;
using System.Linq;

namespace MusicGenerator
{
    class TimeSignature
    {
        public int NotesPerMeasure {get; private set;}
        public int NoteDuration {get; private set;}
        private static readonly Random _random = new Random();

        // Generates randomly:
        public TimeSignature()
        {
            NotesPerMeasure = TimeSigNumerators[_random.Next(0, TimeSigNumerators.Length)];
            NoteDuration = TimeSigDenominators[_random.Next(0, TimeSigDenominators.Length)];
        }

        // Generates with given values:
        public TimeSignature(int notesPerMeasure, int noteDuration)
        {
            NotesPerMeasure = notesPerMeasure;
            NoteDuration = noteDuration;
        }

        public static TimeSignature Parse(string input)
        {
            var inputArr = input.Split('/');
            if (inputArr.Length == 2)
            {
                int numerator = int.Parse(inputArr[0]);
                int denominator = int.Parse(inputArr[1]);
                if (TimeSigNumerators.Contains(numerator) &&
                    TimeSigDenominators.Contains(denominator))
                {
                    return new TimeSignature(numerator, denominator);
                }
                else
                {
                    Console.WriteLine($"One or more integers in {input} are not valid.");
                }
            }
            else
            {
                Console.WriteLine($"'{input}' is not a valid time signature. Must contain only 2 integers separated by a '/'. (e.g., 4/4)");
            }
            return null;
        }
    }
}