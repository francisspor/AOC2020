using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _07
{
    class Program
    {
        static Dictionary<string, Bag> bags = new Dictionary<string, Bag>();

        static void Main(string[] args)
        {
            string line = "";

            using (var sr = new StreamReader("Day07Input.txt"))
            {
                var record = new Dictionary<string, string>();

                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim('.');
                    var parts = line.Split(" contain ");
                    var bagParts = parts[0].Split(" ");
                    var name = string.Join(" ", new string[] { bagParts[0], bagParts[1] });

                    if (parts[1] == "no other bags")
                    {
                        AddToBags(name, new List<KeyValuePair<string, int>>());
                    }
                    else
                    {
                        var fits = parts[1].Split(",");
                        var bgs = new List<KeyValuePair<string, int>>();

                        foreach (var f in fits)
                        {
                            var t = f.Trim().Split(" ");
                            var num = int.Parse(t[0]);
                            var nm = string.Join(" ", new string[] { t[1], t[2] });

                            var kvp = new KeyValuePair<string, int>(nm, num);
                            bgs.Add(kvp);
                        }
                        AddToBags(name, bgs);
                    }
                }
            }

            var goldenOne = bags["shiny gold"];
            var ancestors = GetAncestors(goldenOne);
            Console.WriteLine($"P1: {ancestors.Distinct().Count()}");


            var numIn = WalkTheTree(goldenOne.Fits);

            //            var bottom = ancestors.Where(x=>x.Fits.Count == 0);
            Console.WriteLine($"P2: {numIn}");
        }

        private static int WalkTheTree(List<BagNode> fits)
        {
            var sum = 0;

            foreach (var f in fits)
            {
                sum += f.Count;
                sum += f.Count * WalkTheTree(f);
            }

            return sum;
        }

        private static int WalkTheTree(BagNode node)
        {
            var val = 0;
            foreach (var x in node.Bag.Fits)
            {
                val += x.Count;
                val += x.Count * WalkTheTree(x);
            }

            return val;
        }

        private static List<Bag> GetAncestors(Bag bag)
        {
            var result = new List<Bag>();
            foreach (var b in bag.FitsIn)
            {
                result.Add(b);
                result.AddRange(GetAncestors(b));
            }
            return result;
        }

        private static void AddToBags(string name, List<KeyValuePair<string, int>> fits)
        {

            if (!bags.ContainsKey(name))
            {
                var bg = new Bag { Name = name };
                bags[name] = bg;
            }

            var bag = bags[name];

            if (fits.Count == 0)
            {
                Console.WriteLine(bag);
            }

            foreach (var bg in fits)
            {
                if (!bags.ContainsKey(bg.Key))
                {
                    var bag1 = new Bag { Name = bg.Key };
                    bags[bg.Key] = bag1;
                }
                Bag f = bags[bg.Key];

                bag.Fits.Add(new BagNode { Bag = f, Count = bg.Value });
                f.FitsIn.Add(bag);
            }
        }
    }

    public class Bag
    {
        public string Name { get; set; }
        public List<BagNode> Fits { get; } = new List<BagNode>();
        public List<Bag> FitsIn { get; } = new List<Bag>();

        public override string ToString()
        {
            return $"{Name} Fits: {Fits.Count} Fits In: {FitsIn.Count}\n";
        }
    }

    public class BagNode
    {
        public Bag Bag { get; set; }

        public int Count { get; set; }
    }
}


    // internal class Program
    // {
    //     public static void Main(string[] args)
    //     {
    //         var colorBagMap = GetColorBagMap();
    //         var myBag = colorBagMap["shiny gold"];
    //         int count = 0;
    //         foreach (var kvp in colorBagMap)
    //         {
    //             var bag = kvp.Value;
    //             // don't count myself
    //             if (bag == myBag) continue;
    //             if (bag.CanContain(myBag))
    //             {
    //                 count++;
    //             }
    //         }
    //         Console.WriteLine($"Bags that can contain at least one shiny gold bag: {count}");
    //         Console.WriteLine($"Total bags required inside shiny gold bag: {myBag.GetTotalBagCount()}");
    //     }

    //     private static Dictionary<string, Bag> GetColorBagMap()
    //     {
    //         var colorBagMap = new Dictionary<string, Bag>();
    //         using (var reader = new StreamReader("Day07Input.txt"))
    //         {
    //             while (!reader.EndOfStream)
    //             {
    //                 string line = reader.ReadLine();
    //                 var outterBagColor = line.Substring(0, line.IndexOf("bags")).Trim();
    //                 var innerBags = line.Substring(line.IndexOf("contain"), line.Length - line.IndexOf("contain"))
    //                     .Replace("contain", "")
    //                     .Replace("bags", "")
    //                     .Replace("bag", "")
    //                     .Replace(".", "")
    //                     .Split(',')
    //                     .Select(b => b.Trim());
    //                 if (!colorBagMap.ContainsKey(outterBagColor))
    //                 {
    //                     colorBagMap[outterBagColor] = new Bag(outterBagColor);
    //                 }
    //                 var outterBag = colorBagMap[outterBagColor];
    //                 foreach (var bag in innerBags)
    //                 {
    //                     if (bag == "no other") continue;
    //                     string[] tmpArr = bag.Split(' ');
    //                     string innerBagColor = string.Join(" ", tmpArr, 1, tmpArr.Length - 1);
    //                     int quantity = int.Parse(tmpArr[0]);
    //                     if (!colorBagMap.ContainsKey(innerBagColor))
    //                     {
    //                         colorBagMap[innerBagColor] = new Bag(innerBagColor);
    //                     }
    //                     var innerBag = colorBagMap[innerBagColor];
    //                     outterBag.AddBag(innerBag, quantity);
    //                 }
    //             }
    //         }
    //         return colorBagMap;
    //     }
    // }

    // public class Bag
    // {
    //     private readonly Dictionary<Bag, int> _map = new Dictionary<Bag, int>();
    //     public string Color { get; }
    //     public Bag(string color)
    //     {
    //         Color = color;
    //     }
    //     public void AddBag(Bag bag, int quantity)
    //     {
    //         _map[bag] = quantity;
    //     }
    //     public bool CanContain(Bag bag)
    //     {
    //         // it's me!
    //         if (bag == this) return true;
    //         return _map.Any(kvp => kvp.Key.CanContain(bag));
    //     }
    //     public int GetTotalBagCount()
    //     {
    //         int count = 0;
    //         foreach (var kvp in _map)
    //         {
    //             var bag = kvp.Key;
    //             var quantity = kvp.Value;
    //             count += quantity;
    //             count += bag.GetTotalBagCount() * quantity;
    //         }
    //         return count;
    //     }
    // }
//}