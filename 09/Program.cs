using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _09
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sr = new StreamReader("Day09Input.txt"))
            {
                var line = "";

                var nums = new List<long>();
                var allSums = new List<long>();

                while ((line = sr.ReadLine()) != null)
                {
                    nums.Add(long.Parse(line));
                }

                int numCount = nums.Count();
                // for( int i =0; i < 25; i++ ) {
                //     allSums.AddRange(SumTheRange(nums, i));
                // }
                // allSums = allSums.Distinct().ToList();

                // Console.WriteLine(allSums.Count());

                // for (int i = 25; i < numCount; i++) {
                //     if (allSums.Contains(nums[i])) {
                //         allSums.AddRange(SumTheRange(nums, i));
                //         allSums = allSums.Distinct().ToList();
                //     } else {
                //         Console.WriteLine($"P1: {nums[i]}");
                //         break;
                //     }

                // }

                long p1 = 1721308972;

                for(int i = 0; i < nums.Count; i++) {
                    var currentSum = nums[i];
                    for (int x = i + 1; x < nums.Count; x++) {
                        currentSum += nums[x];

                        if (currentSum == p1) {
                            var range = nums.Skip(i).Take(x-i+1).OrderBy(x=>x);
                            Console.WriteLine(string.Join(",", range));

                            Console.WriteLine($"P2: Holy Macaroni! {range.First() + range.Last()}");
                        } else if (currentSum > p1) {
                            break;
                        }
                    }
                }

            }
        }

        private static List<long> SumTheRange(List<long> nums, int index) {
            var result = new List<long>();
            var currentVal = nums[index];
            for (var i = 0; i < nums.Count; i++) {
                if (i != index) {
                    result.Add(currentVal + nums[i]);
                }
            }
            return result;
        } 
    }
}
