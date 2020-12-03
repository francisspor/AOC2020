using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _03
{
    class Program
    {
        private static Dictionary<int, Dictionary<int, bool>> forest = new Dictionary<int, Dictionary<int, bool>>();
        private static int maxX = 0;
        private static int maxY = 0;

        static void Main(string[] args)
        {
            string line = "";

            using (var sr = new StreamReader("Day03Input.txt"))
            {
                int y = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    maxX = line.Length;
                    forest[y] = new Dictionary<int, bool>();
                    for(int x = 0; x < line.Length; x++) {
                        forest[y][x] = line[x] == '#';
                    }
                    y++;
                }
            }

            maxY = forest.Keys.Last();

            Console.WriteLine(maxY);
            // p1;
            var p1 = TreesOnSlope(3,1);
            Console.WriteLine($"Trees!: {p1}");

            var p21 = TreesOnSlope(1,1);
            var p23 = TreesOnSlope(5,1);
            var p24 = TreesOnSlope(7,1);
            var p25 = TreesOnSlope(1,2);


            Console.WriteLine(p21);
            Console.WriteLine(p1);
            Console.WriteLine(p23);
            Console.WriteLine(p24);
            Console.WriteLine(p25);

            long total = p21 * p1;
            total = total * p23;
            total = total * p24;
            total = total * p25;

            Console.WriteLine($"So Many Trees!: {total}");
        }

        private static int TreesOnSlope(int right, int down) {
            var currentX = 0;
            var currentY = 0;

            var tree = 0;

            while(currentY < maxY) {
                if (currentX + right < maxX) {
                    currentX += right;
                    currentY += down;
                    Console.WriteLine($"At {currentX},{currentY}");
                } else {
                    currentY += down;
                    var xDiff = right - (maxX - currentX);
                    currentX = xDiff;

                    Console.WriteLine($"Wrapped to {currentX},{currentY}");
                }

                if (forest[currentY][currentX]) {
                    Console.WriteLine($"Tree at {currentX},{currentY}");
                    tree++;
                }
            }

            return tree;
        }
    }
}
