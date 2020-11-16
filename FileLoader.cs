using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MusicGenerator
{
    public static class FileLoader
    {
        private static void ListPieces(FileInfo[] pieceFiles)
        {
            Console.WriteLine();
            if (pieceFiles.Length > 0)
            {
                foreach (var file in pieceFiles)
                {
                    string title = Path.GetFileNameWithoutExtension(file.FullName);
                    DateTime creationTime = file.CreationTime;
                    Console.WriteLine($"- \"{title}\" created on {creationTime.ToShortDateString()} at {creationTime.ToShortTimeString()}");
                }
            }
            else
            {
                Console.WriteLine("Nothing found!");
            }
            Console.WriteLine();
        }

        private static bool validateInput(string input, FileInfo[] pieceFiles)
        {
            if (!String.IsNullOrEmpty(input))
            {
                if (pieceFiles.Any(f => Path.GetFileNameWithoutExtension(f.FullName) == input))
                {
                    Console.Clear();
                    Console.WriteLine("File found!");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Piece with title '{input}' was not found. (Titles are case sensitive.)");
                }
            }
            else
            {
                Console.WriteLine("Title must contain at least one character.");
            }
            Console.ReadKey();
            return false;
        }

        public static Piece Open()
        {
            while (true)
            {
                var directory = Directory.CreateDirectory("Pieces");
                FileInfo[] pieceFiles = directory.GetFiles("*.json", SearchOption.AllDirectories);
                Console.WriteLine("Currently saved pieces:");
                ListPieces(pieceFiles);
                Console.WriteLine("Enter the title for the piece you'd like to load, or type 'quit' to cancel.");
                var input = Console.ReadLine();
                if (input.ToLower() == "quit")
                {
                    return null;
                }
                else if (validateInput(input, pieceFiles))
                {
                    return Piece.LoadFromJson(input);
                }
                Console.Clear();
            }
        }
    }
}