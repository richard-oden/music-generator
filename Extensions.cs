using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicGenerator
{
    public static class Extensions
    {
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            var rand = new Random();
            int index = rand.Next(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }
    }
}