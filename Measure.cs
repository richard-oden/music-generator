using System;
using System.Collections.Generic;

namespace MusicGenerator
{
    class Measure
    {
        public readonly double MeasureLength;
        public readonly List<Note> Notes;

        public Measure(KeySignature keySig, TimeSignature timeSig)
        {
            MeasureLength = (double)timeSig.NotesPerMeasure * (1.0 / (double)timeSig.NoteDuration);
            List<Note> notes = new List<Note>();
            double currentLength = 0;
            while (currentLength < MeasureLength) 
            {
                Note newNote = new Note(keySig);
                if (newNote.Duration + currentLength <= MeasureLength)
                {
                    notes.Add(newNote);
                    currentLength += newNote.Duration;
                }
            }
            Notes = notes;
        }
    }
}