using System;
using System.Linq;

namespace spllchck
{
    class Program
    {
        static void Main(string[] args)
        {
            var spllchcker = new SpllChcker("words.txt");

            Console.WriteLine(spllchcker.Check("anwyheer") ?? "404");
        }
    }
}
