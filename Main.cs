using System;
using System.Collections.Generic;

namespace MusicGenerator
{   
    class Program
    {
        public static void Main()
        {
            
            Console.WriteLine("Welcome to the...");
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
                bool success = int.TryParse(userInput, out parsedInput);
                if (success)
                {
                    switch (parsedInput)
                    {
                        case 1:
                            var proceduralPiece = Piece.GenerateProcedurally();
                            proceduralPiece.PrintInfo();
                            proceduralPiece.PrintStaff();
                            Console.ReadLine();
                            break;
                        case 2:
                            var semiProceduralPiece = Piece.GenerateProcedurallyWithParameters();
                            semiProceduralPiece.PrintInfo();
                            semiProceduralPiece.PrintStaff();
                            Console.ReadLine();
                            break;
                        case 3:

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