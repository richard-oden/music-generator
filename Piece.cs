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

        public void ListNotes()
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

        public void PrintStaff()
        {
            Console.WriteLine($"The key signature is {_keySig.Tonic}{_keySig.Accidental} {_keySig.Mode}.");
            Console.WriteLine($"The time signature is {_timeSig.NotesPerMeasure}/{_timeSig.NoteDuration}.");

            string[] finalStaff = new string[14];

            string[] trebleClef = {
                "           ",
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

            int[] accidentalIndices = _keySig.TypeOfAccidental == "#" ? new[] {3, 6, 2, 5, 8, 4, 7} : new int[] {7, 4, 8, 5, 9, 6, 10};
            // int[] sharpIndices = {2, 5, 1, 4, 7, 3, 6};
            // int[] flatIndices = {6, 3, 7, 4, 8, 5, 9};
           
            for (int i = 0; i < 14; i++)
            {
                // Determines if current line is on a space or on a line in the staff:
                string spaceOrLine = (i % 2 != 0 && (i < 12 && i > 2)) ? "-" : " ";

                // Print treble clef line:
                finalStaff[i] += trebleClef[i];

                // Print key signature line:
                string keySigLineSegment = "";
                
                for (int j = 0; j < Math.Abs(_keySig.NumAccidentals); j++)
                {
                    keySigLineSegment += (accidentalIndices[j] == i) ? _keySig.TypeOfAccidental : spaceOrLine;
                    keySigLineSegment += spaceOrLine;
                }
                finalStaff[i] += keySigLineSegment;

                finalStaff[i] += spaceOrLine;

                if (i == 6)
                {
                    finalStaff[i] += _timeSig.NotesPerMeasure;
                }
                else if (i == 8)
                {
                    finalStaff[i] += _timeSig.NoteDuration;
                }
                else
                {
                    finalStaff[i] += spaceOrLine;
                }

                finalStaff[i] += spaceOrLine;

                // Print measure line:
                foreach (List<Note> measure in _measures)
                {
                    foreach (Note note in measure)
                    {
                        string measureLineSegment = "";
                        if (note.StaffLine == i) measureLineSegment += note.StaffNote;
                        while (measureLineSegment.Length < (int)(note.Duration / 0.0625))
                        {
                            measureLineSegment += spaceOrLine;
                        }
                        finalStaff[i] += measureLineSegment;
                    }
                    //Bar line:
                    finalStaff[i] += (i < 12 && i > 2) ? "|" : " ";
                }
                Console.WriteLine(finalStaff[i]);
            }
        }
    }
}