using System;

namespace MusicGenerator
{
    class TimeSignature
    {
        public int NotesPerMeasure {get; private set;}
        public int NoteDuration {get; private set;}

        // Generates randomly:
        public TimeSignature()
        {
            Random random = new Random();
            int[] possibleNotesPerMeasure = {2, 3, 4, 6};
            NotesPerMeasure = possibleNotesPerMeasure[random.Next(0, possibleNotesPerMeasure.Length)];
            int[] possibleDurations = {2, 4, 8};
            NoteDuration = possibleDurations[random.Next(0, possibleDurations.Length)];
        }

        // Generates with given values:
        public TimeSignature(int notesPerMeasure, int noteDuration)
        {
            NotesPerMeasure = notesPerMeasure;
            NoteDuration = noteDuration;
        }
    }
}