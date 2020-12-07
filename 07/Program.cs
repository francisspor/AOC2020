using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _06
{
    class Program
    {               
        static void Main(string[] args)
        {
            var records = new List<string>();

            string line = "";

            using (var sr = new StreamReader("Day06Input.txt"))
            {
                var record = new Dictionary<string, string>();

                while ((line = sr.ReadLine()) != null)
                {
                    records.Add(line);
                }
            }

            int total = 0;

            var set = new List<string>();
            int recordNumber = 0;
            foreach (var l in records) {
                if (!string.IsNullOrEmpty(l) ) {
                    set.Add(l);
                } else {

                    total += SetCount(set);
                    set = new List<string>();
                }
                recordNumber++;
            }
            total += SetCount(set);

            Console.WriteLine($"P1: {total}");
        }

        private static int SetCount(List<string> set) {
            int setS = set.Count();
            Console.WriteLine(setS);
            var whole = string.Join("",set);
            var dict = new Dictionary<char, int>();

            foreach (var c in whole.ToArray()) {
                if (!dict.ContainsKey(c)) {
                    dict[c] = 1;
                } else {
                    dict[c] = dict[c]+1;
                }
            }

            return dict.Values.Where(x=>x == setS).Count();
        }
    }
}
