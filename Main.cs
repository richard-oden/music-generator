using System;
using System.Collections.Generic;

namespace MusicGenerator
{   
    class Program
    {
        public static void Main()
        {
            var newPiece = new Piece();
            try
            {
                Console.WriteLine("Welcome to my Music Generator!");
                var moreInfoMenu = new Menu
                (
                    "more info menu",
                    "What would you like to know more about?",
                    new Dictionary<string, Action>
                    {
                        {"1 - This program", () => Console.WriteLine("Test 1")},
                        {"2 - Random generation", () => Console.WriteLine("Test 2")},
                        {"3 - Partial random generation", () => Console.WriteLine("Test 2")},
                        {"4 - Manual generation", () => Console.WriteLine("Test 3")},
                        {"5 - Printing the piece as a list", () => Console.WriteLine("Test 4")},
                        {"6 - Print the piece on a staff", () => Console.WriteLine("Test 5")}
                    }
                );
                string contactInfo = "email: richard.thomas.oden@gmail.com\nlinkedin: linkedin.com/in/rtoden/\ngithub: github.com/richard-oden";
                var mainMenu = new Menu
                (
                    "main menu",
                    "What would you like to do?",
                    new Dictionary<string, Action>
                    {
                        {"1 - Generate a new piece completely at random.", () => newPiece.GenerateRandomly(new KeySignature(), new TimeSignature())},
                        {"2 - Generate a new piece with some random elements.", () => Console.WriteLine("[Partial random generation goes here]")},
                        {"3 - Write a new piece manually.", () => Console.WriteLine("[Manual generation goes here]")},
                        {"4 - Contact the creator.", () => Console.WriteLine(contactInfo)},
                        {"5 - Learn more.", () => moreInfoMenu.OpenMenu()}
                    }
                );
                var printMethodMenu = new Menu
                (
                    "print method menu",
                    "Please select a method to print your new piece.",
                    new Dictionary<string, Action>
                    {
                        {"1 - List the contents of each measure.", () => newPiece.ListNotes()},
                        {"2 - Print the piece on a staff.", () =>  newPiece.PrintStaff()},
                    }
                );
                var tryAgainMenu = new Menu
                (
                    "try again menu",
                    "Would you like to try again? Exiting this menu will close the program.",
                    new Dictionary<string, Action>
                    {
                        {"Yes, let's try again!", () => mainMenu.OpenMenu()}
                    }

                );
                mainMenu.OpenMenu();
                if (newPiece.hasBeenGenerated) printMethodMenu.OpenMenu();
                tryAgainMenu.OpenMenu();
                Console.WriteLine("Goodbye!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}