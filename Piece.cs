using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace MusicGenerator
{
    public class Piece
    {
        public string Title {get; private set;}
        public int NumMeasures {get; private set;} = 0;
        public KeySignature KeySig {get; private set;}
        public TimeSignature TimeSig {get; private set;}
        public List<Measure> Measures {get; private set;}
        private static Random _random = new Random();
        
        public Piece(string title, int numMeasures, KeySignature keySig, TimeSignature timeSig)
        {
            Title = title;
            NumMeasures = numMeasures;
            KeySig = keySig;
            TimeSig = timeSig;

            var measures = new List<Measure>();
            int currentMeasure = 0;
            while (currentMeasure < NumMeasures)
            {
                Measure newMeasure = new Measure(KeySig, TimeSig);
                measures.Add(newMeasure);
                currentMeasure++;
            }
            Measures = measures;
        }

        public Piece(string title, int numMeasures, KeySignature keySig, TimeSignature timeSig, List<Measure> measures)
        {
            Title = title;
            NumMeasures = numMeasures;
            KeySig = keySig;
            TimeSig = timeSig;
            Measures = measures;
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

        private static void printInstructions(bool show)
        {
            if (show)
            {
                Console.WriteLine("INSTRUCTIONS:");
                Console.WriteLine("- UP/DOWN arrows to move cursor");
                Console.WriteLine("- ENTER to add new rest/note");
                Console.WriteLine("- BACKSPACE to delete previous rest/note");
                Console.WriteLine("- I to toggle instructions");
                Console.WriteLine("- D to toggle piece description");
                Console.WriteLine("- S to save piece");
                Console.WriteLine("- ESC to exit to main menu");
            }
            else
            {
                Console.WriteLine("Press 'I' to show instructions.");
            }
        }
        public static Piece GenerateManually()
        {
            Dictionary<string, object> parameters = getParameters();
            Console.Clear();
            var numMeasures = (int)parameters["measures"];

            var measures = new List<Measure>();
            int currentMeasureNum = 0;
            int cursorPosition = 7;
            bool measureDeleted = false;
            
            bool showInstructions = true;
            bool showDescription = false;
            bool stillEditing = true;

            Piece tempPiece = new Piece((string)parameters["title"], currentMeasureNum,
                (KeySignature)parameters["key"], (TimeSignature)parameters["time"], measures);
            
            while (currentMeasureNum < numMeasures && stillEditing)
            {
                // Recreate piece each time a measure is added or deleted:
                tempPiece = new Piece((string)parameters["title"], currentMeasureNum,
                    (KeySignature)parameters["key"], (TimeSignature)parameters["time"], measures);
                // If measure was deleted, do not add a new measure (instead revisit previous measure):
                if (measureDeleted == false) tempPiece.Measures.Add(new Measure(tempPiece.KeySig, tempPiece.TimeSig, new List<MeasureSegment>()));
                // Create reference of current measure:
                var tempMeasure = tempPiece.Measures[currentMeasureNum];
                // Reset measure deleted bool:
                measureDeleted = false;
                while (tempMeasure.CurrentLength < tempMeasure.MeasureLength && !measureDeleted && stillEditing)
                {
                    if (showDescription)
                    {
                        tempPiece.PrintInfo();
                        tempPiece.ListNotes();
                    }
                    var printer = new StaffPrinter(tempPiece);
                    printer.Print(cursorPosition);
                    printInstructions(showInstructions);
                    var editInput = Console.ReadKey();
                    // Up and down arrows move cursor so that a different line is selected:
                    if (editInput.Key == ConsoleKey.UpArrow) cursorPosition -= 1;
                    else if (editInput.Key == ConsoleKey.DownArrow) cursorPosition += 1;
                    else if (editInput.Key == ConsoleKey.Enter) tempMeasure.Append(cursorPosition);
                    else if (editInput.Key == ConsoleKey.Backspace) 
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
                        // If this is empty and it is NOT the first measure, unappend from previous, 
                        // delete this measure, and change current measure number to previous:
                        else
                        {
                            tempPiece.Measures.RemoveAt(tempPiece.Measures.Count - 1);
                            tempPiece.Measures[currentMeasureNum - 1].Unappend();
                            // subtract two, because current measure number will be incremented at end of loop:
                            currentMeasureNum -= 2;
                            measureDeleted = true;
                        }
                    }
                    else if (editInput.Key == ConsoleKey.I) showInstructions = !showInstructions;
                    else if (editInput.Key == ConsoleKey.D) showDescription = !showDescription;
                    else if (editInput.Key == ConsoleKey.Escape) stillEditing = false;
                    Console.Clear();
                }
                currentMeasureNum++;
            }
            return tempPiece;
        }
        public void SaveToJson()
        {
            var directory = Directory.CreateDirectory("Pieces");
            var fileName = Path.Combine(directory.FullName, $"{Title}.json");
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(fileName, jsonString);
        }
        public static Piece LoadFromJson(string pieceTitle)
        {
            var fileName = Path.Combine("Pieces", $"{pieceTitle}.json");
            var jsonString = File.ReadAllText(fileName);
            var jsonDocRoot = JsonDocument.Parse(jsonString).RootElement;

            string title = jsonDocRoot.GetProperty("Title").GetString();
            int numMeasures = jsonDocRoot.GetProperty("NumMeasures").GetInt32();
            var keySig = jsonDocRoot.GetProperty("KeySig").GetKeySignature();
            var timeSig = jsonDocRoot.GetProperty("TimeSig").GetTimeSignature();
            var measures = jsonDocRoot.GetProperty("Measures").EnumerateArray()
                .Select(m => m.GetMeasure(keySig, timeSig)).ToList();

            return new Piece(title, numMeasures, keySig, timeSig, measures);
        }
        public void PrintInfo()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"The key signature is {KeySig.Tonic}{KeySig.Accidental} {KeySig.Mode}.");
            Console.WriteLine($"The time signature is {TimeSig.NotesPerMeasure}/{TimeSig.NoteDuration}.");
        }
        
        public void ListNotes()
        {
            foreach (Measure measure in Measures)
	        {
                Console.WriteLine($"\nMeasure {Measures.IndexOf(measure) + 1}:");
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

    }
}