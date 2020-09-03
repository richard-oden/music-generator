using System;
using System.Collections.Generic;

namespace MusicGenerator
{
    class Piece
    {
        public readonly int NumMeasures;
        public readonly KeySignature KeySig;
        public readonly TimeSignature TimeSig;
        public readonly List<List<Note>> Measures;

        public Piece(int numMeasures, KeySignature keySig, TimeSignature timeSig)
        {
            NumMeasures = numMeasures;
            KeySig = keySig;
            TimeSig = timeSig;

            List<List<Note>> measures = new List<List<Note>>();
            int currentMeasure = 0;
            while (currentMeasure < NumMeasures)
            {
                Measure newMeasure = new Measure(keySig, timeSig);
                measures.Add(newMeasure.Notes);
                currentMeasure++;
            }
            Measures = measures;
        }

        public void listNotes()
        {
            Console.WriteLine($"The key signature is {KeySig.Tonic}{KeySig.Accidental} {KeySig.Mode}.");
            Console.WriteLine($"The time signature is {TimeSig.NotesPerMeasure}/{TimeSig.NoteDuration}.");
            foreach (List<Note> measure in Measures)
	        {
                Console.WriteLine($"\nMeasure {Measures.IndexOf(measure) + 1}:");
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
            foreach (string line in trebleClef) Console.WriteLine(line);
        }
    }
}