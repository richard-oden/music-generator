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
                    Piece newPiece = null;
                    Piece loadedPiece = null;
                    switch (parsedInput)
                    {
                        case 1:
                            newPiece = Piece.GenerateProcedurally();
                            break;
                        case 2:
                            newPiece = Piece.GenerateProcedurallyWithParameters();
                            break;
                        case 3:
                            newPiece = Piece.GenerateManually();
                            break;
                        case 4:
                            loadedPiece = Piece.LoadFromJson("That Sonata");
                            break;
                        case 5:
                            Console.WriteLine("Goodbye!");
                            programRunning = false;
                            break;
                        default:

                            break; 
                    }
                    if (newPiece != null)
                    {
                        newPiece.PrintInfo();
                        var printer = new StaffPrinter(newPiece);
                        printer.Print();
                        Console.WriteLine($"Would you like to save {newPiece.Title}? (Y/N)");
                        bool awaitingSaveInput = true;
                        while (awaitingSaveInput)
                        {
                            var saveInput = Console.ReadKey();
                            if (saveInput.Key == ConsoleKey.Y)
                            {
                                newPiece.SaveToJson();
                                awaitingSaveInput = false;
                            }
                            else if (saveInput.Key == ConsoleKey.N)
                            {
                                // Discard
                                awaitingSaveInput = false;
                            }
                        }
                        newPiece = null;
                    }
                    else if (loadedPiece != null)
                    {
                        Console.WriteLine($"{loadedPiece.Title} was successfully loaded!");
                        loadedPiece.PrintInfo();
                        var printer = new StaffPrinter(loadedPiece);
                        printer.Print();
                        Console.ReadKey();
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