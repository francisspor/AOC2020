using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _05
{
    class Program
    {               
        static void Main(string[] args)
        {
            var records = new List<string>();

            string line = "";

            using (var sr = new StreamReader("Day05Input.txt"))
            {
                var record = new Dictionary<string, string>();

                while ((line = sr.ReadLine()) != null)
                {
                    records.Add(line);
                }
            }

            int maxSeat = 0;

            var seats = new Dictionary<int, Dictionary<int, int>>();
            for(int z = 0; z < 128; z++) {
                seats[z] = new Dictionary<int, int>();
                for(int i = 0; i < 8; i++) {
                    seats[z][i] = 0;
                }
            }


            // p1
            foreach (var l in records) {
                var row = l.Substring(0, 7);
                var col = l.Substring(7, 3);

                int rowNum = FindRow(row, 0, 127);
                int colNum = FindRow(col, 0, 7);

                int seatNumber = (rowNum * 8) + colNum;

                seats[rowNum][colNum] = seatNumber;

                if (seatNumber > maxSeat) {
                    maxSeat = seatNumber;
                }
            }

            Console.WriteLine($"P1 Max Seat Number: {maxSeat}");

            for(int y = 1; y < 127; y++)
            {
                for( int x = 0; x < 8; x++) {
                    if (seats[y][x] == 0) {
                        Console.WriteLine($"Row: {y} Col: {x} is empty");
                    }
                }
            }

        }

        private static int FindRow(string row, int min, int max) {
            if (row.Length == 1) {
                if (row == "F" || row == "L") {
                    return min;
                } else {
                    return max;
                }
            }
            var first = row[0];

            var range = (max - min) / 2 ;

            if (first == 'F' || first == 'L') {
                return FindRow(row.Substring(1, row.Length - 1), min, min + range);
            } else  {
                return FindRow(row.Substring(1, row.Length - 1), max - range, max);
            }
        }
    }
}
