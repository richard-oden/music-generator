using System;

namespace MusicGenerator
{
    public class Menu
    {
        public string MenuName {get; private set;}
        public string Prompt {get; private set;}
        public MenuSelection[] Options {get; private set;}
        public bool AllowExit {get; private set;} = true;

        public Menu(string menuName, string prompt, MenuSelection[] options)
        {
            MenuName = menuName;
            Prompt = prompt;
            Options = options;
        }

        public Menu(string menuName, string prompt, MenuSelection[] options, bool allowExit)
        {
            MenuName = menuName;
            Prompt = prompt;
            Options = options;
            AllowExit = allowExit;
        }

        public static void PrintBlankLine()
        {
            Console.WriteLine("");
        }

        public static void PrintBlankLine(int times)
        {
            for (int i = 0; i < times; i++) Console.WriteLine("");
        }

        public void OpenMenu()
        {
            while (true)
            {
                Console.Write(Prompt);
                if (AllowExit) Console.Write(" Enter 'quit' to exit this menu.");
                PrintBlankLine();
                foreach (var option in Options) Console.WriteLine($"{option.Selector} - {option.Description}");
                PrintBlankLine();
                string input = Console.ReadLine();
                PrintBlankLine();
                bool validOptionSelected = false;
                foreach (var option in Options) 
                {
                    if (option.InputMatchesSelector(input))
                    {
                        validOptionSelected = true;
                        option.ExecuteCallback();
                        break;
                    }
                }
                if (validOptionSelected == false)
                {
                    if (input.ToLower() == "quit" && AllowExit)
                    {
                        Console.WriteLine($"Exiting {MenuName}...");
                        break;
                    }
                    else Console.WriteLine($"Menu option '{input}' not found. Please enter a valid choice.");
                }
            }
        }
    }
}