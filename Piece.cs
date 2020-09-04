using System;
using System.Collections.Generic;

namespace MusicGenerator
{
    class Piece
    {
        private readonly int _numMeasure;
        private readonly KeySignature _keySig;
        private readonly TimeSignature _timeSig;
        private readonly List<List<Note>> _measures;

        public Piece(int numMeasures, KeySignature keySig, TimeSignature timeSig)
        {
            _numMeasure = numMeasures;
            _keySig = keySig;
            _timeSig = timeSig;

            List<List<Note>> measures = new List<List<Note>>();
            int currentMeasure = 0;
            while (currentMeasure < _numMeasure)
            {
                Measure newMeasure = new Measure(keySig, timeSig);
                measures.Add(newMeasure.Notes);
                currentMeasure++;
            }
            _measures = measures;
        }

        public void listNotes()
        {
            Console.WriteLine($"The key signature is {_keySig.Tonic}{_keySig.Accidental} {_keySig.Mode}.");
            Console.WriteLine($"The time signature is {_timeSig.NotesPerMeasure}/{_timeSig.NoteDuration}.");
            foreach (List<Note> measure in _measures)
	        {
                Console.WriteLine($"\nMeasure {_measures.IndexOf(measure) + 1}:");
                string text = "";
                foreach (Note note in measure)
	            {
                    text += $"{note.NoteName}{note.Accidental}{note.Octave} {note.RhythmicValue}";
                    text += measure.IndexOf(note) == measure.Count - 1 ? "." : ", ";
	            }
                Console.WriteLine(text);
	        }
        }

        public void printStaff()
        {
            string[] trebleClef = {
                "           ",
                " /    /\\   ",
                "|+----|-|--",
                "||    |/   ",
                "||----|----",
                "||   /|    ",
                "||--/-|----",
                "|| |  | _  ",
                "||-|-(@)-)-",
                "||  \\ | /  ",
                "|+----|----",
                " \\    |    ",
                "    (_|    "
            };

            

            string[] Accidentals = new string[9];
            int[] sharpIndices = {2, 5, 1, 4, 7, 3, 6};
            int[] flatIndices = {6, 3, 7, 4, 8, 5, 9};
            foreach (string line in trebleClef) Console.WriteLine(line);
        }
    }
}