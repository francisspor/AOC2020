using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _08
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new CloneableDictionary<int, Instruction>();

            using (var sr = new StreamReader("Day08Input.txt"))
            {
                var line = "";
                var i = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(" ");

                    var instruction = new Instruction();
                    switch (parts[0])
                    {
                        case "nop":
                            instruction.Op = Operation.Nop;
                            break;
                        case "acc":
                            instruction.Op = Operation.Acc;
                            break;
                        case "jmp":
                            instruction.Op = Operation.Jmp;
                            break;
                    }
                    instruction.Value = int.Parse(parts[1]);
                    program[i++] = instruction;
                }

                var acc = 0;
                var pointer = 0;
                var instructions = new List<int>();

                while (true)
                {
                    if (!instructions.Contains(pointer))
                    {
                        instructions.Add(pointer);

                        var inst = program[pointer];
                        if (inst.Op == Operation.Acc)
                        {
                            acc += inst.Value;
                            pointer++;
                        }
                        else if (inst.Op == Operation.Nop)
                        {
                            pointer++;
                        }
                        else if (inst.Op == Operation.Jmp)
                        {
                            pointer += inst.Value;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine($"P1: {acc}");

                MutateProgram(program);

            }
        }

        private static int ExecuteProgram(IDictionary<int, Instruction> program)
        {
            var acc = 0;
            var pointer = 0;
            var instructions = new List<int>();

            while (pointer < program.Keys.Count)
            {
                if (!instructions.Contains(pointer))
                {
                    instructions.Add(pointer);

                    var inst = program[pointer];
                    if (inst.Op == Operation.Acc)
                    {
                        acc += inst.Value;
                        pointer++;
                    }
                    else if (inst.Op == Operation.Nop)
                    {
                        pointer++;
                    }
                    else if (inst.Op == Operation.Jmp)
                    {
                        pointer += inst.Value;
                    }
                }
                else
                {
                    acc = -1;
                    break;
                }
            }
            return acc;
        }

        private static void MutateProgram(CloneableDictionary<int, Instruction> program)
        {
            var nops = new List<int>();
            var jmps = new List<int>();
            foreach (var kvp in program)
            {
                if (kvp.Value.Op == Operation.Nop)
                {
                    nops.Add(kvp.Key);
                }
                else if (kvp.Value.Op == Operation.Jmp)
                {
                    jmps.Add(kvp.Key);
                }
            }

            var acc = 0;

            foreach (var n in nops)
            {
                var mut = program.Clone();
                mut[n].Op = Operation.Jmp;

                acc = ExecuteProgram(mut);
                if (acc != -1)
                {
                    break;
                }
            }

            if (acc != -1)
            {
                Console.WriteLine($"P2: {acc}");
            }

            foreach (var n in jmps)
            {
                var mut = program.Clone();
                mut[n].Op = Operation.Nop;
                acc = ExecuteProgram(mut);
                if (acc != -1)
                {
                    break;
                }
            }

            if (acc != -1)
            {
                Console.WriteLine($"P2: {acc}");
            }
        }
    }

    public class CloneableDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TValue : ICloneable
    {
        public IDictionary<TKey, TValue> Clone()
        {
            CloneableDictionary<TKey, TValue> clone = new CloneableDictionary<TKey, TValue>();

            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                clone.Add(pair.Key, (TValue)pair.Value.Clone());
            }
            return clone;
        }
    }


    public class Instruction : ICloneable
    {
        public Operation Op { get; set; }
        public int Value { get; set; }

        public virtual object Clone()
        {
            var instruction = new Instruction();
            instruction.Op = this.Op;
            instruction.Value = this.Value;

            return instruction;
        }
    }

    public enum Operation
    {
        Nop,
        Jmp,
        Acc
    }
}
