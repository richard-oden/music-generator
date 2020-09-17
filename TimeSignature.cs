using System;
using static MusicGenerator.Theory;

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
    }
}