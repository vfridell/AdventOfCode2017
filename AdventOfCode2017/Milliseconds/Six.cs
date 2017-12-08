using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Milliseconds
{
    public class Six
    {
        //public static List<int> input = new List<int> { 0, 2, 7, 0 };
        public static List<int> input = new List<int> { 10,3,15,10,5,15,5,15,9,2,5,8,5,2,3,6 };

        public static void Part1()
        {
            int cycles = 0;
            HashSet<string> states = new HashSet<string>();
            string initialStateString = input.Aggregate("", (s, i) => i.ToString() + "," + s);
            states.Add(initialStateString);

            while (true)
            {
                int blockRedistributionIndex = input.IndexOf(input.Max());
                int blocksToRedistribute = input[blockRedistributionIndex];
                input[blockRedistributionIndex] = 0;
                while (blocksToRedistribute > 0)
                {
                    blockRedistributionIndex++;
                    input[blockRedistributionIndex % input.Count]++;
                    blocksToRedistribute--;
                }
                int countBeforeAdd = states.Count;
                string stateString = input.Aggregate("", (s, i) => i.ToString() + "," + s);
                states.Add(stateString);
                cycles++;
                if (states.Count == countBeforeAdd)
                {
                    Console.WriteLine($"{cycles} cycles before a loop.");
                    return;
                }
            }
        }

        public static void Part2()
        {
            int cycles = 0;
            Dictionary<string, int> states = new Dictionary<string, int>();
            string initialStateString = input.Aggregate("", (s, i) => i.ToString() + "," + s);
            states.Add(initialStateString, 0);

            while (true)
            {
                int blockRedistributionIndex = input.IndexOf(input.Max());
                int blocksToRedistribute = input[blockRedistributionIndex];
                input[blockRedistributionIndex] = 0;
                while (blocksToRedistribute > 0)
                {
                    blockRedistributionIndex++;
                    input[blockRedistributionIndex % input.Count]++;
                    blocksToRedistribute--;
                }
                string stateString = input.Aggregate("", (s, i) => i.ToString() + "," + s);
                cycles++;
                if(states.ContainsKey(stateString))
                {
                    int priorCycle = states[stateString];
                    Console.WriteLine($"{cycles-priorCycle} cycles in the loop.");
                    return;
                }
                states.Add(stateString, cycles);
            }
        }
    }
}
