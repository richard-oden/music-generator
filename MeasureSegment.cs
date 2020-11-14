using System;
using System.Linq;
using static MusicGenerator.Theory;

namespace MusicGenerator
{
    public class MeasureSegment
    {
        public int StaffLine {get; set;}
        public double Duration {get; protected set;}
        public string RhythmicValue {get; set;}
        public string StaffSymbol {get; set;}
        public static readonly Random _random = new Random();

        public MeasureSegment()
        {
            object[] rhythmInfo = RhythmInfos[_random.Next(0, RhythmInfos.Length)]; 
            Duration = (double)rhythmInfo[2];
            RhythmicValue = (string)rhythmInfo[0];
            StaffSymbol = (string)rhythmInfo[1];
        }

        public MeasureSegment(string staffSymbol)
        {
            var rhythmInfo = RhythmInfos.SingleOrDefault(rI => (string)rI[1] == staffSymbol.ToUpper());
            if (rhythmInfo != null)
            {
                StaffSymbol = staffSymbol;
                Duration = (double)rhythmInfo[2];
                RhythmicValue = (string)rhythmInfo[0];
            }
            else
            {
                throw new Exception($"{staffSymbol} is not a valid staff symbol.");
            }
        }
    }
}