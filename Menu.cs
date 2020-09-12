using System;
using System.Collections.Generic;

namespace MusicGenerator
{
    public class Menu
    {
        public string MenuName {get; private set;}
        public string Prompt {get; private set;}
        public Dictionary<string, Action> Options {get; private set;}
        public bool AllowExit {get; private set;} = true;

        public Menu(string menuName, string prompt, Dictionary<string, Action> options)
        {
            MenuName = menuName;
            Prompt = prompt;
            Options = options;
        }

        public Menu(string menuName, string prompt, Dictionary<string, Action> options, bool allowExit)
        {
            MenuName = menuName;
            Prompt = prompt;
            Options = options;
            AllowExit = allowExit;
        }

        public void OpenMenu()
        {
            bool breakOuterLoop = false;
            while (true)
            {
                Console.Write("\n" + Prompt);
                if (AllowExit) Console.Write(" Enter 'quit' to exit this menu.");
                Console.WriteLine("\n");
                foreach (var option in Options) Console.WriteLine(option.Key);
                Console.WriteLine("");
                string response = Console.ReadLine();
                Console.WriteLine("");
                foreach (var option in Options)
                {
                    if (response.ToLower()[0] == option.Key.ToLower()[0])
                    {
                        option.Value();
                        breakOuterLoop = true;
                        break;
                    }
                }
                if (breakOuterLoop) break;
                else if (response.ToLower() == "quit" && AllowExit)
                {
                    Console.WriteLine($"Exiting {MenuName}...");
                    break;
                }
                else Console.WriteLine($"Menu option '{response}' not found. Please enter a valid choice.");
            }
        }
    }
}