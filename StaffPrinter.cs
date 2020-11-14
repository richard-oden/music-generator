using System;
using System.Collections.Generic;
using static MusicGenerator.Theory;

namespace MusicGenerator
{
    public class StaffPrinter
    {
        private Piece _piece;
        // Used to index _trebleClef, _finalStaff, and _finalBarLine:
        private int i = 0;
        // Determines if current line is on a space or on a line in the staff:
        private string _spaceOrLine => (i % 2 != 0 && (i < 12 && i > 2)) ? "-" : " ";
        private string[] _trebleClef = new[] 
        {
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
        private string[] _finalStaff = new string[14];
        public StaffPrinter(Piece pieceToPrint)
        {
            _piece = pieceToPrint;
        }
        private void printKeySig()
        {
            int[] accidentalIndices = _piece.KeySig.TypeOfAccidental == "#" ? 
                new[] {3, 6, 2, 5, 8, 4, 7} : 
                new[] {7, 4, 8, 5, 9, 6, 10};
            
            string keySigLineSegment = "";
                
            for (int j = 0; j < Math.Abs(_piece.KeySig.NumAccidentals); j++)
            {
                keySigLineSegment += (accidentalIndices[j] == i) ? _piece.KeySig.TypeOfAccidental : _spaceOrLine;
                keySigLineSegment += _spaceOrLine;
            }
            _finalStaff[i] += keySigLineSegment;
        }
        private void printTimeSig()
        {
            if (i == 6)
            {
                _finalStaff[i] += _piece.TimeSig.NotesPerMeasure;
            }
            else if (i == 8)
            {
                _finalStaff[i] += _piece.TimeSig.NoteDuration;
            }
            else
            {
                _finalStaff[i] += _spaceOrLine;
            }
        }
        private void printBarLine(Measure currentMeasure)
        {
            if (_piece.Measures.IndexOf(currentMeasure) == _piece.Measures.Count - 1)
            {
                    string[] finalBarLine = {
                    "  ",
                    "  ",
                    "\\ ",
                    "+|",
                    "||",
                    "||",
                    "||",
                    "||",
                    "||",
                    "||",
                    "|| ",
                    "+|",
                    "/ ",
                    "  "
                };
                _finalStaff[i] += finalBarLine[i];
            }
            else if (i < 12 && i > 2)
            {
                _finalStaff[i] += "|";
            }
            else
            {
                _finalStaff[i] += " ";
            }
        }
        private void printMeasure()
        {
            foreach (Measure measure in _piece.Measures)
            {
                foreach (var measureSegment in measure.Contents)
                {
                    string measureLineSegment = "";
                    if (measureSegment.StaffLine == i) measureLineSegment += measureSegment.StaffSymbol;
                    while (measureLineSegment.Length < (int)(measureSegment.Duration / 0.0625))
                    {
                        measureLineSegment += _spaceOrLine;
                    }
                    _finalStaff[i] += measureLineSegment;
                }
                printBarLine(measure);
            }
        }
        private void printCursor(int cursorPosition)
        {
        if (i == cursorPosition)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('<');
                Console.ResetColor();
            }
        }

        public void Print(int cursorPosition = -1)
        {
            while (i < 14)
            {
                // Print treble clef line:
                _finalStaff[i] += _trebleClef[i];

                printKeySig();
                _finalStaff[i] += _spaceOrLine;

                printTimeSig();
                _finalStaff[i] += _spaceOrLine;

                printMeasure();

                Console.Write(_finalStaff[i]);
                printCursor(cursorPosition);
                Console.Write('\n');
                i++;
            }
        }
    }
}