using System;
using System.Collections.Generic;
using System.Linq;
using static MusicGenerator.Theory;

namespace MusicGenerator
{
    class Piece
    {
        public string Title {get; private set;}
        private int _numMeasures = 0;
        private KeySignature _keySig;
        private TimeSignature _timeSig;
        private List<Measure> _measures;
        private static Random _random = new Random();
        public bool hasBeenGenerated => _numMeasures != 0;

        public Piece(string title, int numMeasures, KeySignature keySig, TimeSignature timeSig)
        {
            Title = title;
            _numMeasures = numMeasures;
            _keySig = keySig;
            _timeSig = timeSig;

            var measures = new List<Measure>();
            int currentMeasure = 0;
            while (currentMeasure < _numMeasures)
            {
                Measure newMeasure = new Measure(_keySig, _timeSig);
                measures.Add(newMeasure);
                currentMeasure++;
            }
            _measures = measures;
        }

        public Piece(string title, int numMeasures, KeySignature keySig, TimeSignature timeSig, List<Measure> measures)
        {
            Title = title;
            _numMeasures = numMeasures;
            _keySig = keySig;
            _timeSig = timeSig;
            _measures = measures;
        }

        public static Piece GenerateProcedurally()
        {
            return new Piece(TitleGenerator.Generate(), _random.Next(1, 16), new KeySignature(), new TimeSignature());
        }

        private static Dictionary<string, object> getParameters()
        {
            var parameters = new Dictionary<string, object>();

            bool titleFound = false;
            while (!titleFound)
            {
                Console.WriteLine("Enter a title for your piece:");
                string titleInput = Console.ReadLine();
                if (!String.IsNullOrEmpty(titleInput))
                {
                    parameters.Add("title", titleInput);
                    titleFound = true;
                }
                else
                {
                    Console.WriteLine("Title must contain at least one character.");
                }
            }

            bool numMeasuresFound = false;
            while (!numMeasuresFound)
            {
                Console.WriteLine("Enter the number of measures in your piece:");
                string numMeasuresInput = Console.ReadLine();
                int numMeasures;
                if (int.TryParse(numMeasuresInput, out numMeasures))
                {
                    parameters.Add("measures", numMeasures);
                    numMeasuresFound = true;
                }
                else
                {
                    Console.WriteLine("Input must be an integer.");
                }
            }
            
            bool keySigFound = false;
            while (!keySigFound)
            {
                Console.WriteLine("Enter a key signature (e.g., C minor, F# Major, Ab minor, etc):");
                string keySigInput = Console.ReadLine();
                var newKeySig = KeySignature.Parse(keySigInput);
                if (newKeySig != null) 
                {
                    parameters.Add("key", newKeySig);
                    keySigFound = true;
                }
            }

            bool timeSigFound = false;
            while (!timeSigFound)
            {
                Console.WriteLine("Enter a time signature for your piece (e.g., 2/4, 6/8, etc):");
                string timeSigInput = Console.ReadLine();
                var newTimeSig = TimeSignature.Parse(timeSigInput);
                if (newTimeSig != null) 
                {
                    parameters.Add("time", newTimeSig);
                    timeSigFound = true;
                }
            }

            return parameters;
        }

        public static Piece GenerateProcedurallyWithParameters()
        {
            Dictionary<string, object> parameters = getParameters();
            return new Piece((string)parameters["title"], (int)parameters["measures"],
                (KeySignature)parameters["key"], (TimeSignature)parameters["time"]);
        }

        public static void GenerateManually()
        {
            Dictionary<string, object> parameters = getParameters();
            Console.Clear();
            var numMeasures = (int)parameters["measures"];

            var measures = new List<Measure>();
            int currentMeasureNum = 0;
            int cursorPosition = 7;
            bool measureDeleted = false;
            
            while (currentMeasureNum < numMeasures)
            {
                // Recreate piece each time a measure is added or deleted:
                var tempPiece = new Piece((string)parameters["title"], currentMeasureNum,
                    (KeySignature)parameters["key"], (TimeSignature)parameters["time"], measures);
                // If measure was deleted, do not add a new measure (instead revisit previous measure):
                if (measureDeleted == false) tempPiece._measures.Add(new Measure(tempPiece._keySig, tempPiece._timeSig, new List<MeasureSegment>()));
                // Create reference of current measure:
                var tempMeasure = tempPiece._measures[currentMeasureNum];
                // Reset measure deleted bool:
                measureDeleted = false;
                while (tempMeasure.CurrentLength < tempMeasure.MeasureLength && !measureDeleted)
                {
                    tempPiece.PrintStaff(cursorPosition);
                    Console.WriteLine("\nUse up/down arrow keys to select staff line, press enter to add a note/rest and backspace to delete previous note/rest.");
                    var cursorInput = Console.ReadKey();
                    // Up and down arrows move cursor so that a different line is selected:
                    if (cursorInput.Key == ConsoleKey.UpArrow) cursorPosition -= 1;
                    else if (cursorInput.Key == ConsoleKey.DownArrow) cursorPosition += 1;
                    else if (cursorInput.Key == ConsoleKey.Enter) tempMeasure.Append(cursorPosition);
                    else if (cursorInput.Key == ConsoleKey.Backspace) 
                    {
                        if (tempMeasure.Contents.Count > 0)
                        {
                            tempMeasure.Unappend();
                        }
                        // If this is empty and it is the first measure, warn user:
                        else if (currentMeasureNum == 0)
                        {
                            Console.WriteLine("There is nothing left to delete!");
                            Console.ReadKey();
                        }
                        // If this is empty and it is NOT the first measure, unappend from previous 
                        // and change current measure number to previous:
                        else
                        {
                            tempPiece._measures.RemoveAt(tempPiece._measures.Count - 1);
                            tempPiece._measures[currentMeasureNum - 1].Unappend();
                            // subtract two, because current measure number will be incremented at end of loop:
                            currentMeasureNum -= 2;
                            measureDeleted = true;
                        }
                    }
                    Console.Clear();
                }
                currentMeasureNum++;
            }
        }


        public void PrintInfo()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"The key signature is {_keySig.Tonic}{_keySig.Accidental} {_keySig.Mode}.");
            Console.WriteLine($"The time signature is {_timeSig.NotesPerMeasure}/{_timeSig.NoteDuration}.");
        }
        
        public void ListNotes()
        {
            foreach (Measure measure in _measures)
	        {
                Console.WriteLine($"\nMeasure {_measures.IndexOf(measure) + 1}:");
                string text = "";
                foreach (var measureSegment in measure.Contents)
	            {
                    if (measureSegment is Note) 
                    {
                        Note note = (Note)measureSegment;
                        text += $"{note.NoteName}{note.Accidental}{note.Octave} ";
                    }
                    text += measureSegment.RhythmicValue;
                    text += measure.Contents.IndexOf(measureSegment) == measure.Contents.Count - 1 ? "." : ", ";
	            }
                Console.WriteLine(text);
	        }
        }

        public void PrintStaff(int cursorPosition = -1)
        {
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

            int[] accidentalIndices = _keySig.TypeOfAccidental == "#" ? 
                new[] {3, 6, 2, 5, 8, 4, 7} : 
                new int[] {7, 4, 8, 5, 9, 6, 10};
           
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

                // Print time signature line:
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
                foreach (Measure measure in _measures)
                {
                    foreach (var measureSegment in measure.Contents)
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
                Console.Write(finalStaff[i]);
                if (i == cursorPosition)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('<');
                    Console.ResetColor();
                }
                Console.Write('\n');
            }
        }
    }
}