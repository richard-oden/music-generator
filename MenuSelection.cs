using System;

namespace MusicGenerator
{
    public class MenuSelection
    {
        public string Selector {get; private set;} // User must type this to select
        public string Description {get; private set;}
        private Action _callback {get; set;}

        public MenuSelection(string selector, string description, Action callback)
        {
            Selector = selector;
            Description = description;
            _callback = callback;
        }

        public bool InputMatchesSelector(string input)
        {
            return input == Selector ? true : false;
        }
        public void ExecuteCallback()
        {
            _callback();
        }
    }
}