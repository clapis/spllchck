using System;
using System.IO;
using System.Linq;

namespace spllchck
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1) {
                Console.WriteLine("Error: A single words file must be specified.");
                return;
            }

            var words = File.ReadAllText(args[0]);

            var spllchcker = new SpllChcker(words);

            while(true) {
                Console.Write("# ");
                var word = Console.ReadLine().ToLower();
                if (string.IsNullOrWhiteSpace(word)) break;
                Console.WriteLine(spllchcker.Check(word) ?? word);
            }
        }
    }
}
