using System;
using System.Collections.Generic;

namespace MusicGenerator
{   
    class Program
    {
        public static void Main()
        {
            var newPiece = new Piece();
            bool programRunning = true;
            try
            {
                Console.WriteLine("Welcome to my Music Generator!");
                var moreInfoMenu = new Menu
                (
                    "more info menu",
                    "What would you like to know more about?",
                    new Dictionary<string, Action>
                    {
                        {"1 - This program", () => Console.WriteLine(@"This program was written for Code Louisville's Sept. 2020 C# Course. It's based on a similar 
project I made in Ruby in 2019, and it's my first serious program in C#. It can generate a piece
of music randomly based on some parameters, and allows the user to create a piece of music using
my attempt at a basic music notation software.")},
                        {"2 - Random generation", () => Console.WriteLine(@"To generate a piece of music completely at random, first the number of measures, key signature, 
and time signature are generated. After this, the contents of each measure are generated. Each 
new measure segment has an 80% chance of being a note, and 20% chance of being a rest. Both rests 
and notes are randomly assigned a rhythmic duration, and notes are assigned a pitch. Notes and 
rests are added to the measure until their total duration is equal to the measure's length. After 
that, measures are added to the piece until the specified number of measures have been created.")},
                        {"3 - Partial random generation", () => Console.WriteLine(@"When generating a piece of music with some random elements, the user selects the number of measures,
key signature, and time signature. The piece is then generated randomly based on these parameters.")},
                        {"4 - Manual generation", () => Console.WriteLine(@"When generating a piece of manually, the user specifies the piece's basic parameters before
writing the contents of each measure in the piece.")},
                        {"5 - Printing the piece as a list", () => Console.WriteLine(@"After the piece is generated, it can be displayed as a list of its contents. When this is done, 
basic information about the piece is first printed, then each measure is listed with descriptions 
of the notes and rests they contain.")},
                        {"6 - Printing the piece on a staff", () => Console.WriteLine(@"After the piece is generated, it can be displayed on an ASCII art representation of a treble clef
staff. In this form, notes and rests are placed where they would appear on a staff, and rhythmic 
durations are represented by the first letter of their name. Notes are shown as uppercase letters,
whereas rests are shown as lowercase letters. If a note or rest has a dotted rhythm, a period is 
appended to the letter. For example, a quarter note is shown as a 'Q', a whole rest is shown as a 
'w', and a dotted eighth note is shown as 'E.'.")}
                    }
                );
                string contactInfo = @"email: richard.thomas.oden@gmail.com
linkedin: linkedin.com/in/rtoden/
github: github.com/richard-oden";
                var mainMenu = new Menu
                (
                    "main menu",
                    "What would you like to do?",
                    new Dictionary<string, Action>
                    {
                        {"1 - Generate a new piece completely at random", () => newPiece.GenerateRandomly(new KeySignature(), new TimeSignature())},
                        {"2 - Generate a new piece with some random elements", () => Console.WriteLine("[Partial random generation goes here]")},
                        {"3 - Write a new piece manually", () => Console.WriteLine("[Manual generation goes here]")},
                        {"4 - Contact the creator", () => Console.WriteLine(contactInfo)},
                        {"5 - Learn more", () => moreInfoMenu.OpenMenu()},
                        {"6 - Quit program", () => programRunning = false}
                    },
                    false
                );
                var printMethodMenu = new Menu
                (
                    "print method menu",
                    "Please select a method to print your new piece.",
                    new Dictionary<string, Action>
                    {
                        {"1 - List the contents of the piece", () => newPiece.ListNotes()},
                        {"2 - Print the piece on a staff", () =>  newPiece.PrintStaff()},
                    }
                );
                var tryAgainMenu = new Menu
                (
                    "try again menu",
                    "Would you like to try again?",
                    new Dictionary<string, Action>
                    {
                        {"1 - Try again", () => mainMenu.OpenMenu()},
                        {"2 - Quit program", () => programRunning = false}
                    },
                    false
                );
                // Main loop
                while (programRunning)
                {
                    // Clear previous piece
                    newPiece = new Piece();
                    mainMenu.OpenMenu();
                    if (newPiece.hasBeenGenerated) 
                    {
                        printMethodMenu.OpenMenu();
                        tryAgainMenu.OpenMenu();
                    }
                }
                Console.WriteLine("Goodbye!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}