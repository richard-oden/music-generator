using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

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

        public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input.First().ToString().ToUpper() + input.Substring(1)
        };

        public static KeySignature GetKeySignature(this JsonElement jsonElement)
        {
            char tonic = Convert.ToChar(jsonElement.GetProperty("Tonic").GetString());
            string accidental = jsonElement.GetProperty("Accidental").GetString();
            string mode = jsonElement.GetProperty("Mode").GetString();
            return new KeySignature(tonic, accidental, mode);
        }

        public static TimeSignature GetTimeSignature(this JsonElement jsonElement)
        {
            int notesPerMeasure = jsonElement.GetProperty("NotesPerMeasure").GetInt32();
            int noteDuration = jsonElement.GetProperty("NoteDuration").GetInt32();
            return new TimeSignature(notesPerMeasure, noteDuration);
        }

        public static MeasureSegment GetMeasureSegment(this JsonElement jsonElement, KeySignature keySig)
        {
            int staffLine = jsonElement.GetProperty("StaffLine").GetInt32();
            string staffSymbol = jsonElement.GetProperty("StaffSymbol").GetString();

            if (char.IsUpper(staffSymbol.ToCharArray()[0]))
            {
                return new Note(keySig, staffLine, staffSymbol);
            }
            else
            {
                return new Rest(staffSymbol);
            }
        }

        public static Measure GetMeasure(this JsonElement jsonElement, KeySignature keySig, TimeSignature timeSig)
        {
            var jsonContents = jsonElement.GetProperty("Contents").EnumerateArray();
            var contents = jsonContents.Select(j => j.GetMeasureSegment(keySig)).ToList();
            return new Measure(keySig, timeSig, contents);
        }
    }
}