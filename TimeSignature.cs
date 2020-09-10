using System;

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
            int[] possibleNotesPerMeasure = {2, 3, 4, 6};
            NotesPerMeasure = possibleNotesPerMeasure[_random.Next(0, possibleNotesPerMeasure.Length)];
            int[] possibleDurations = {2, 4, 8};
            NoteDuration = possibleDurations[_random.Next(0, possibleDurations.Length)];
        }

        // Generates with given values:
        public TimeSignature(int notesPerMeasure, int noteDuration)
        {
            NotesPerMeasure = notesPerMeasure;
            NoteDuration = noteDuration;
        }
    }
}