using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicGenerator
{
    public class Measure
    {
        private KeySignature _keySig;
        private TimeSignature _timeSig;
        public double MeasureLength => (double)_timeSig.NotesPerMeasure * (1.0 / (double)_timeSig.NoteDuration);
        public double CurrentLength => Contents.Sum(mS => mS.Duration);
        public List<MeasureSegment> Contents {get; private set;}
        private static readonly Random _random = new Random();
        public Measure(KeySignature keySig, TimeSignature timeSig)
        {
            _keySig = keySig;
            _timeSig = timeSig;
            Contents = new List<MeasureSegment>();
            while (CurrentLength < MeasureLength) 
            {
                var newAddition = new MeasureSegment();
                if (_random.NextDouble() < .8)
                {
                    newAddition = new Note(_keySig);
                }
                else
                {
                    newAddition = new Rest();
                }

                if (newAddition.Duration + CurrentLength <= MeasureLength)
                {
                    Contents.Add(newAddition);
                }
            }
        }

        public Measure(KeySignature keySig, TimeSignature timeSig, List<MeasureSegment> contents)
        {
            _keySig = keySig;
            _timeSig = timeSig;
            Contents = contents;
        }
        public void Append(int line)
        {
            Console.WriteLine("\nEnter the symbol for the note/rest you'd like to add:");
            string symbolInput = Console.ReadLine();
            var rhythmInfo = Theory.RhythmInfos.SingleOrDefault(rI => (string)rI[1] == symbolInput.ToUpper());
            if (rhythmInfo != null && (double)rhythmInfo[2] + CurrentLength <= MeasureLength)
            {
                if (char.IsUpper(symbolInput.ToCharArray()[0]))
                {
                    Contents.Add(new Note(_keySig, line, symbolInput));
                }
                else
                {
                    Contents.Add(new Rest(symbolInput));
                }
            }
            else
            {
                Console.WriteLine($"New measure segment {symbolInput} is invalid or too long to fit in measure.");
                Console.ReadKey();
            }
        }
    
        public void Unappend()
        {
            if (Contents.Count > 0)
            {
                Contents.RemoveAt(Contents.Count - 1);
            }
        }
    }
}