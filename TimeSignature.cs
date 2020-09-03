using System;

namespace MusicGenerator
{
    class TimeSignature
    {
        public readonly int NotesPerMeasure;
        public readonly int NoteDuration;

        // Generates randomly:
        public TimeSignature()
        {
            Random random = new Random();
            int[] npms = {2, 3, 4, 6};
            NotesPerMeasure = npms[random.Next(0, npms.Length)];
            int[] durations = {2, 4, 8};
            NoteDuration = durations[random.Next(0, durations.Length)];
        }

        // Generates with given values:
        public TimeSignature(int notesPerMeasure, int noteDuration)
        {
            NotesPerMeasure = notesPerMeasure;
            NoteDuration = noteDuration;
        }
    }
}