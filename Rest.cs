using System;

namespace MusicGenerator
{
    class Rest : MeasureSegment
    {
        public Rest() : base()
        {
            StaffLine = 7;
            RhythmicValue += " rest";
            StaffSymbol = StaffSymbol.ToLower();
        }
    }
}