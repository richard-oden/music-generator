using System;

namespace MusicGenerator
{
    public static class TitleGenerator
    {
        private static Random _random = new Random();
        private static string[] _singlePrefixes = new [] {"The", "A", "This", "That", "One"};
        private static string[] _pluralPrefixes = new [] {"The", "Those", "These", "Many", "Some", "Two", "Three", "Five", "Seven", "Ten"};
        private static string[] _adjectives = new [] {"Warm", "Lovely", "Cool", "Heartless", "Gentle", "Vibrant", "Comic",
            "Loving", "Black", "Bright", "Heavenly", "Hellish", "Cold", "Fragrant", "Chirping", "Singing", "Chaotic"};
        private static string[] _singleNounsRegular = new [] {"Bird", "Flower", "Day", "Year", "Week", "Sound", "Song", "Sonata", "Etude",
            "Prelude", "Ode", "Conclusion", "Prologue", "Epilogue", "Anecdote", "Dog", "Cat", "Love"};
        private static string[] _singleNounsIrregular = new [] {"Man", "Woman", "Child", "Ox", "Person", "Mouse", "Leaf", "Goose", "Crisis"};
        private static string[] _pluralNounsIrregular = new [] {"Men", "Women", "Children", "Oxen", "People", "Mice", "Leaves", "Geese", "Crises"};

        public static string Generate()
        {
            string title = "";
            bool hasPrefix = _random.Next(0, 2) == 1;
            bool hasAdjective = _random.Next(0, 2) == 1;
            bool isPlural = _random.Next(0, 2) == 1;
            bool isIrregular = _random.Next(0, 2) == 1;

            if (hasPrefix)
            {
                title += (isPlural ? _pluralPrefixes : _singlePrefixes).RandomElement() + ' ';
            }

            if (hasAdjective)
            {
                title += _adjectives[_random.Next(0, _adjectives.Length-1)] + ' ';
            }

            if (isIrregular)
            {
                title += (isPlural ? _pluralNounsIrregular : _singleNounsIrregular).RandomElement();
            }
            else
            {
                title += isPlural ? (_singleNounsRegular.RandomElement() + 's') : _singleNounsRegular.RandomElement();
            }

            return title;
        }
    }
}