using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicGenerator
{   
    class Program
    {
        public static void Main()
        {
            Piece piece = new Piece(6, new KeySignature(), new TimeSignature());
            piece.printStaff();
        }
    }
}