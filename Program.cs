using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

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
                
                string userInput = Console.ReadLine();
                Console.Clear();
                
                int parsedInput;
                bool validInput = int.TryParse(userInput, out parsedInput);
                if (validInput)
                {
                    Piece pieceToDisplay = null;
                    bool pieceSaved = false;
                    switch (parsedInput)
                    {
                        case 1:
                            pieceToDisplay = Piece.GenerateProcedurally();
                            break;
                        case 2:
                            pieceToDisplay = Piece.GenerateProcedurallyWithParameters();
                            break;
                        case 3:
                            pieceToDisplay = Piece.GenerateManually();
                            pieceSaved = true;
                            break;
                        case 4:
                            pieceToDisplay = FileLoader.Open();
                            pieceSaved = true;
                            break;
                        case 5:
                            Console.WriteLine("Goodbye!");
                            Thread.Sleep(1000);
                            programRunning = false;
                            break;
                        default:

                            break; 
                    }
                    if (pieceToDisplay != null)
                    {
                        pieceToDisplay.PrintInfo();
                        var printer = new StaffPrinter(pieceToDisplay);
                        printer.Print();
                        Console.ReadKey();
                        if (!pieceSaved) pieceToDisplay.SavePrompt();
                        pieceToDisplay = null;
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