using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _04
{
    class Program
    {               
        static void Main(string[] args)
        {
            var records = new List<Dictionary<string, string>>();

            string line = "";

            using (var sr = new StreamReader("Day04Input.txt"))
            {
                var record = new Dictionary<string, string>();

                while ((line = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line)) {
                        var kvp = line.Split(":");
                        record[kvp[0]] = kvp[1];

                    } else {
                        records.Add(record);
                        record = new Dictionary<string, string>();
                    }
                }
                records.Add(record);
            }

            // p1
            var valid = 0;
            foreach (var kvp in records) {
                if (kvp.Values.Count() == 8) {
                    valid++;
                } else if (kvp.Values.Count() == 7 && !kvp.ContainsKey("cid")) {
                    valid++;
                }
            }
            Console.WriteLine($"In your {records.Count()} records {valid} are valid.");


            // p2
            valid = 0;
            foreach (var kvp in records) {
                bool validRecord = true;

                if (kvp.TryGetValue("byr", out string birthYear)) {
                    if (int.TryParse(birthYear, out int year)) {
                        if (year >= 1920 && year <= 2002) {
                            validRecord &= true;
                        } else {
                            validRecord = false;
                        }
                    }
                } else {
                    validRecord = false;
                }

                if (kvp.TryGetValue("iyr", out string issueYear)) {
                    if (int.TryParse(issueYear, out int year)) {
                        if (year >= 2010 && year <= 2020) {
                            validRecord &= true;
                        } else {
                            validRecord = false;
                        }
                    }
                } else {
                    validRecord = false;
                }

                if (kvp.TryGetValue("eyr", out string expYear)) {
                    if (int.TryParse(expYear, out int year)) {
                        if (year >= 2020 && year <= 2030) {
                            validRecord &= true;
                        } else {
                            validRecord = false;
                        }
                    }
                } else {
                    validRecord = false;
                }

                if (kvp.TryGetValue("pid", out string passportId)) {
                    if (passportId.Length == 9) {
                        validRecord &= true;
                    } else {
                        validRecord = false;
                    }
                } else {
                    validRecord = false;
                }

                if (kvp.TryGetValue("ecl", out string eyeColor)) {
                    if (eyeColor == "amb" || eyeColor == "blu" || eyeColor=="brn" || eyeColor == "gry" || eyeColor=="grn" || eyeColor == "hzl" || eyeColor == "oth") {
                        validRecord &= true;
                    } else {
                        validRecord = false;
                    }
                } else {
                    validRecord = false;
                }

                if (kvp.TryGetValue("hcl", out string hairColor)) {
                    if (hairColor.StartsWith("#") && hairColor.Length == 7) {
                        validRecord &= true;
                    } else {
                        validRecord = false;
                    }
                } else {
                    validRecord = false;
                }


                if (kvp.TryGetValue("hgt", out string height)) {
                    if (height.Contains("cm")) {
                        if (int.TryParse(height.Substring(0, height.Length - 2), out int h)) {
                            if (h >= 150 && h <= 193) {
                                validRecord &= true;
                            } else {
                                validRecord = false;
                            }
                        }                                               
                    } else if (height.Contains("in")) {
                        if (int.TryParse(height.Substring(0, height.Length - 2), out int h)) {                        
                            if (h >= 59 && h <= 76) {
                                validRecord &= true;
                            } else {
                                validRecord = false;
                            }
                        }
                    } else {
                        validRecord = false;
                    }

                } else {
                    validRecord = false;
                }




                if (validRecord) {
                    valid++;
                }
            }
            Console.WriteLine($"In your {records.Count()} records {valid} are valid.");

        }
    }
}
