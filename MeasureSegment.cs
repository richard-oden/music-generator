using System;

namespace MusicGenerator
{
    class MeasureSegment
    {
        public int StaffLine {get; set;}
        public double Duration {get; private set;}
        public string RhythmicValue {get; set;}
        public string StaffSymbol {get; set;}
        public static readonly Theory _theory = new Theory();
        public static readonly Random _random = new Random();

        public MeasureSegment()
        {
            object[] durationArray = _theory.Durations[_random.Next(0, _theory.Durations.Length)]; 
            Duration = (double)durationArray[2];
            RhythmicValue = (string)durationArray[0];
            StaffSymbol = (string)durationArray[1];
        }
    }
}