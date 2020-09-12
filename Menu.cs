using System;
using System.Collections.Generic;

namespace MusicGenerator
{
    public class Menu
    {
        public string MenuName {get; private set;}
        public string Prompt {get; private set;}
        public Dictionary<string, Action> Options {get; private set;}

        public Menu(string menuName, string prompt, Dictionary<string, Action> options)
        {
            MenuName = menuName;
            Prompt = prompt;
            Options = options;
        }

        public void OpenMenu()
        {
            bool breakOuterLoop = false;
            while (true)
            {
                Console.WriteLine("\n" + Prompt + " Enter 'quit' to exit this menu.\n");
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
                else if (response.ToLower() == "quit")
                {
                    Console.WriteLine($"Exiting {MenuName}...");
                    break;
                }
                else Console.WriteLine($"Menu option '{response}' not found. Please enter a valid choice.");
            }
        }
    }
}