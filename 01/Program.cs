using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = "";
            var list = new SortedList<int, int>();

            using (var sr = new StreamReader("Day01Input.txt"))
            {
                while((line = sr.ReadLine()) != null)  
                {  
                    var num = int.Parse(line);
                    list.Add(num, num);
                } 
            }

            var numbers = list.Values.ToList();

            // p1           
            foreach (var l in numbers) {
                var diff = 2020 - l;

                int value;
                if (list.TryGetValue(diff, out value)) {
                    Console.WriteLine($"Match: {l} - {diff}");
                    Console.WriteLine($"Answer P1: {l * diff}");
                    break;
                }
            }

            //p2
            foreach (var place1 in numbers) {
                foreach (var place2 in numbers.Where(x=>x > place1)) {
                    foreach (var place3 in numbers.Where(x=>x > place2)) {
                        var total = place1 + place2 + place3;
                        if (total == 2020) {
                            Console.WriteLine($"Match: {place1} - {place2} - {place3}");
                            Console.WriteLine($"Answer P2: {place1 * place2 * place3}");
                            return;
                        }

                        if (total > 2020) {
                            break;
                        }
                    }
                }
            }
        }
    }
}
