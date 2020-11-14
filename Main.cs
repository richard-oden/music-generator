using System;
using System.Collections.Generic;

namespace MusicGenerator
{   
    class Program
    {
        public static void Main()
        {
            
            Console.WriteLine("------- Welcome to The -------");
            Console.WriteLine("==============================");
            Console.WriteLine("==== MUSIC GENERATOR 5000 ====");
            Console.WriteLine("==============================");
            Console.WriteLine();

            bool programRunning = true;
            while (programRunning)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Procedurally generate a new piece of music.");
                Console.WriteLine("2. Select some basic parameters, then generate a new piece of music.");
                Console.WriteLine("3. Manually write a new piece of music.");
                Console.WriteLine("4. Load a saved piece of music.");
                Console.WriteLine("5. Exit program.");
                
                int parsedInput;
                string userInput = Console.ReadLine();
                Console.Clear();
                
                bool validInput = int.TryParse(userInput, out parsedInput);
                if (validInput)
                {
                    switch (parsedInput)
                    {
                        case 1:
                            var proceduralPiece = Piece.GenerateProcedurally();
                            proceduralPiece.PrintInfo();
                            var pPrinter = new StaffPrinter(proceduralPiece);
                            pPrinter.Print();
                            Console.ReadLine();
                            break;
                        case 2:
                            var semiProceduralPiece = Piece.GenerateProcedurallyWithParameters();
                            semiProceduralPiece.PrintInfo();
                            var spPrinter = new StaffPrinter(semiProceduralPiece);
                            spPrinter.Print();
                            Console.ReadLine();
                            break;
                        case 3:
                            Piece.GenerateManually();
                            break;
                        case 4:

                            break;
                        case 5:
                            Console.WriteLine("Goodbye!");
                            programRunning = false;
                            break;
                        default:

                            break; 
                    }
                }
                else
                {
                    Console.WriteLine($"'{userInput}' is not a valid selection. Please enter an integer.");
                }
                Console.Clear();
            }
        }
    }
}