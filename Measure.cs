using System;
using System.Collections.Generic;

namespace MusicGenerator
{
    class Measure
    {
        public double MeasureLength {get; private set;}
        public List<MeasureSegment> Contents {get; private set;}
        private static readonly Random _random = new Random();

        public Measure(KeySignature keySig, TimeSignature timeSig)
        {
            MeasureLength = (double)timeSig.NotesPerMeasure * (1.0 / (double)timeSig.NoteDuration);
            List<MeasureSegment> contents = new List<MeasureSegment>();
            double currentLength = 0;
            while (currentLength < MeasureLength) 
            {
                var newAddition = new MeasureSegment();
                if (_random.NextDouble() < .8)
                {
                    newAddition = new Note(keySig);
                }
                else
                {
                    newAddition = new Rest();
                }

                if (newAddition.Duration + currentLength <= MeasureLength)
                {
                    contents.Add(newAddition);
                    currentLength += newAddition.Duration;
                }
            }
            Contents = contents;
        }
    }
}