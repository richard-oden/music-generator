using System;
using System.Collections.Generic;

namespace MusicGenerator
{
    class Piece
    {
        public string Title {get; private set;}
        private int _numMeasures = 0;
        private KeySignature _keySig;
        private TimeSignature _timeSig;
        private List<List<MeasureSegment>> _measures;
        private static Random _random = new Random();
        public bool hasBeenGenerated => _numMeasures != 0;

        public Piece(string title, int numMeasures, KeySignature keySig, TimeSignature timeSig)
        {
            Title = title;
            _numMeasures = numMeasures;
            _keySig = keySig;
            _timeSig = timeSig;

            List<List<MeasureSegment>> measures = new List<List<MeasureSegment>>();
            int currentMeasure = 0;
            while (currentMeasure < _numMeasures)
            {
                Measure newMeasure = new Measure(_keySig, _timeSig);
                measures.Add(newMeasure.Contents);
                currentMeasure++;
            }
            _measures = measures;
        }

        public static Piece GenerateProcedurally()
        {
            return new Piece(TitleGenerator.Generate(), _random.Next(1, 16), new KeySignature(), new TimeSignature());
        }

        public void GenerateSemiRandomly(int numMeasures, KeySignature keySig, TimeSignature timeSig)
        {
            _numMeasures = numMeasures;
            _keySig = keySig;
            _timeSig = timeSig;

            List<List<MeasureSegment>> measures = new List<List<MeasureSegment>>();
            int currentMeasure = 0;
            while (currentMeasure < _numMeasures)
            {
                Measure newMeasure = new Measure(keySig, timeSig);
                measures.Add(newMeasure.Contents);
                currentMeasure++;
            }
            _measures = measures;
        }

        public void WriteManually(int numMeasures, KeySignature keySig, TimeSignature timeSig)
        {
            _numMeasures = numMeasures;
            _keySig = keySig;
            _timeSig = timeSig;
            List<List<MeasureSegment>> measures = new List<List<MeasureSegment>>();
            int currentMeasure = 0;
            while (currentMeasure < _numMeasures)
            {
                Measure newMeasure = new Measure(keySig, timeSig);
                measures.Add(newMeasure.Contents);
                currentMeasure++;
            }
            _measures = measures;
        }

        public void ListNotes()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"The key signature is {_keySig.Tonic}{_keySig.Accidental} {_keySig.Mode}.");
            Console.WriteLine($"The time signature is {_timeSig.NotesPerMeasure}/{_timeSig.NoteDuration}.");
            foreach (List<MeasureSegment> measure in _measures)
	        {
                Console.WriteLine($"\nMeasure {_measures.IndexOf(measure) + 1}:");
                string text = "";
                foreach (var measureSegment in measure)
	            {
                    if (measureSegment is Note) 
                    {
                        Note note = (Note)measureSegment;
                        text += $"{note.NoteName}{note.Accidental}{note.Octave} ";
                    }
                    text += measureSegment.RhythmicValue;
                    text += measure.IndexOf(measureSegment) == measure.Count - 1 ? "." : ", ";
	            }
                Console.WriteLine(text);
	        }
        }

        public void PrintStaff()
        {
            Console.WriteLine($"Title: {Title}");
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
            // int[] flatIndices = {7, 4, 8, 5, 9, 6, 10};
           
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
                foreach (List<MeasureSegment> measure in _measures)
                {
                    foreach (var measureSegment in measure)
                    {
                        string measureLineSegment = "";
                        if (measureSegment.StaffLine == i) measureLineSegment += measureSegment.StaffSymbol;
                        while (measureLineSegment.Length < (int)(measureSegment.Duration / 0.0625))
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