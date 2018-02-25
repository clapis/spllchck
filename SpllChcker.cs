using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace spllchck
{
    class SpllChcker
    {
        const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

        private Dictionary<string, int> dictionary;

        public SpllChcker(string path)
        {
            var source = File.ReadAllText(path).ToLower();

            this.dictionary = Regex.Matches(source, "[a-z]+")
                                   .GroupBy(m => m.Value)
                                   .ToDictionary(g => g.Key, g => g.Count());
        }

        public string Check(string word)
        {
            if (this.dictionary.ContainsKey(word)) return word;

            var edits = GetEditsOf(word);
            var candidates = edits.Where(this.dictionary.ContainsKey);

            if (!candidates.Any())
                candidates = edits.SelectMany(GetEditsOf).Where(this.dictionary.ContainsKey);

            return candidates.OrderByDescending(c => this.dictionary[c]).FirstOrDefault();
        }

        private static IEnumerable<string> GetEditsOf(string word)
        {
            var deletes = Enumerable.Range(0, word.Length)
                .Select(i => word.Substring(0, i) + word.Substring(i + 1));

            var transposes = Enumerable.Range(0, word.Length - 1)
                .Select(i => word.Substring(0, i) + word[i + 1] + word[i] + word.Substring(i + 2));

            var inserts = Enumerable.Range(0, word.Length + 1)
                .SelectMany(i => Alphabet, (i, a) => new { i, a })
                .Select(x => word.Substring(0, x.i) + x.a + word.Substring(x.i));

            var alters = Enumerable.Range(0, word.Length)
                .SelectMany(i => Alphabet, (i, a) => new { i, a })
                .Select(x => word.Substring(0, x.i) + x.a + word.Substring(x.i + 1));

            return deletes.Union(transposes).Union(inserts).Union(alters);
        }

    }
}

