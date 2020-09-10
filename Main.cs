using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicGenerator
{   
    class Program
    {
        public static void Main()
        {
            Piece piece = new Piece(4, new KeySignature(), new TimeSignature());
            piece.ListNotes();
            piece.PrintStaff();
        }
    }
}