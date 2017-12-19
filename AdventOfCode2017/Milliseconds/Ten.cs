using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Milliseconds
{
    public class Ten
    {
        public static int listsize = 256;
        public static List<int> circularList = new List<int>(listsize);
        public static int skipSize = 0;
        public static int pos = 0;

        public static void Part1()
        {
            for (int i = 0; i < listsize; i++) circularList.Add(i);

            KnotHash();

            Console.WriteLine($"Result: {circularList[0] * circularList[1]}");
        }

        public static void Part2()
        {
            List<byte> bytes = new List<byte>();

            foreach (char c in stringInput) bytes.Add((byte)c);
            bytes.AddRange(new byte[] { 17, 31, 73, 47, 23 });
            input = bytes.ConvertAll<int>(b => (char)b);
            for (int i = 0; i < listsize; i++) circularList.Add(i);

            for (int i = 0; i < 64; i++) KnotHash();

            bytes.Clear();
            for(int i = 0; i< 16; i++)
            {
                byte currentByte = (byte)circularList[(16 * i)];
                for(int j=1; j<16; j++)
                {
                    currentByte = (byte)(currentByte ^ circularList[(i * 16) + j]);
                }
                bytes.Add(currentByte);
            }

            Console.Write($"Knot hash: ");
            foreach (byte b in bytes) Console.Write($"{b:x2}");
            Console.WriteLine();
        }

        public static void KnotHash()
        {
            foreach (int length in input)
            {
                ReverseSubList(length);
                if (pos + length + skipSize > circularList.Count - 1)
                    pos = (pos + (length + skipSize)) - circularList.Count;
                else
                    pos += length + skipSize;

                skipSize++;
            }
        }

        public static void ReverseSubList(int length)
        {
            int subLength = length;
            int swaps = (int)Math.Floor((double)length / 2);
            for (int i = 0; i<swaps; i++)
            {
                int end;
                int start = pos + (length - subLength);
                if (start > circularList.Count - 1)
                {
                    start = start - circularList.Count;
                    while (start >= circularList.Count) start = start - circularList.Count;
                }

                if (pos + (subLength - 1) > circularList.Count - 1)
                {
                    end = (pos + (subLength - 1)) - circularList.Count;
                    while (end >= circularList.Count) end = end - circularList.Count;
                }
                else
                {
                    end = pos + (subLength - 1);
                }

                if (end != start)
                {
                    int temp = circularList[end];
                    circularList[end] = circularList[start];
                    circularList[start] = temp;
                }
                subLength--;
            }
        }

        public static List<int> testinput = new List<int>() { 3, 4, 1, 5 };
        public static List<int> input = new List<int>() { 18, 1, 0, 161, 255, 137, 254, 252, 14, 95, 165, 33, 181, 168, 2, 188 };
        public static string stringInput = "18,1,0,161,255,137,254,252,14,95,165,33,181,168,2,188";
        //public static string stringInput = "AoC 2017";
    }
}
