using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = "";
            var list = new List<string>();

            int p1valid = 0;
            int p2valid = 0;

            using (var sr = new StreamReader("Day02Input.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }

            foreach (var l in list)
            {
                var parts = l.Split(":");
                var pattern = parts[0].Trim();
                var password = parts[1].Trim();

                var letter = pattern.Last();

                var range = pattern.Substring(0, pattern.IndexOf(" "));
                var parts2 = range.Split("-");
                int min = 0;
                int max = 0;
                int.TryParse(parts2[0], out min);
                int.TryParse(parts2[1], out max);

                var count = password.Where(x => x == letter).Count();
                //                    Console.WriteLine($"{password}: {letter}: {count}");
                if (count >= min && count <= max)
                {
                    p1valid++;
                }

                if (password[min-1] == letter ^ password[max-1] == letter) {
                    p2valid++;

                }
            }
            Console.WriteLine($"P1 Valid Count: {p1valid}");
            Console.WriteLine($"P2 Valid Count: {p2valid}");

        }
    }
}
