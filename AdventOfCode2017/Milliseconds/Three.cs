using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2017.Milliseconds
{
    public static class Three
    {
        public static void Part2()
        {
            int input = 368078;
            var rankList = new Dictionary<int, Rank>();
            for (int i = 1; i < 100; i++) rankList.Add(i, new Rank(i));

        }

        public static void Part1()
        {
            int input = 368078;
            //int input = 1024;
            Rank inputRank = new Rank(RankNum(input));
            
            Console.WriteLine($"Value {input} is at rank {inputRank.rank}");
            Console.WriteLine($"Southeast corner of rank {inputRank.rank} is {inputRank.seCorner} which is {inputRank.seCorner - input} less than the input");
            Console.WriteLine($"Length of side on rank {inputRank.rank} is {inputRank.LengthOfSide}");
            Console.WriteLine($"Corners are {inputRank.seCorner}, {inputRank.swCorner}, {inputRank.nwCorner} and {inputRank.neCorner}");
            Console.WriteLine($"Midpoints are {inputRank.south}, {inputRank.west}, {inputRank.north} and {inputRank.east}");
            Console.WriteLine($"Distance from nearest midpoint is {inputRank.DistanceFromNearestSide(input)}");
            Console.WriteLine($"Distance from origin is {inputRank.DistanceFromOrigin(input)}");
        }

        public static int SoutheastCornerValue(int rank)
        {
            if (rank == 1) return 1;
            int w = (rank + (rank - 1));
            return w * w;
        }

        public static int RankNum(int value)
        {
            if (value == 1) return 1;
            double sqrtC = Math.Sqrt(value);
            double rankEst = Math.Ceiling(sqrtC / 2d);
            double rankMinusOneEst = Math.Floor(sqrtC / 2d);
            if (rankEst + rankMinusOneEst >= sqrtC &&
                rankEst != rankMinusOneEst)
            {
                return (int)rankEst;
            }
            else return (int)(rankEst + 1);
        }

        private static void Validate()
        {
            List<int> corners = new List<int>();
            corners.Add(0);
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine($"Rank {i} corner is {SoutheastCornerValue(i)}");
                corners.Add(SoutheastCornerValue(i));
            }

            foreach (int value in corners.Where(v => v > 0))
            {
                Console.WriteLine($"Corner {value} is rank {RankNum(value)}");
            }


            for (int i = 1; i < 170; i++)
            {
                Console.WriteLine($"Corner {i} is rank {RankNum(i)}");
            }
        }
    }

    public class Rank
    {
        public int rank;
        public int seCorner;
        public int LengthOfSide;
        public int numberOfSquaresAtRank;
        public int swCorner;
        public int nwCorner;
        public int neCorner;
        public int south;
        public int west;
        public int north;
        public int east;
        public int smallestCell;

        public Dictionary<int, int> part2List;

        public Rank(int n)
        {
            rank = n;
            seCorner = Three.SoutheastCornerValue(n);
            LengthOfSide = (int)Math.Sqrt(seCorner);
            numberOfSquaresAtRank = (LengthOfSide * 4) - 4;
            swCorner = seCorner - (LengthOfSide - 1);
            nwCorner = seCorner - ((LengthOfSide * 2) - 2);
            neCorner = seCorner - ((LengthOfSide * 3) - 3);
            south = seCorner - (LengthOfSide / 2);
            west = swCorner - (LengthOfSide / 2);
            north = nwCorner - (LengthOfSide / 2);
            east = neCorner - (LengthOfSide / 2);
            smallestCell = east - (LengthOfSide / 2) + 1;
        }

        public bool CellNumInRank(int cellValue)
        {
            return (cellValue <= seCorner && cellValue >= smallestCell);
        }

        public int DistanceFromOrigin(int cellValue)
        {
            return DistanceFromNearestSide(cellValue) + (rank - 1);
        }

        public int DistanceFromNearestSide(int cellValue)
        {
            if (!CellNumInRank(cellValue))
                throw new Exception($"Value {cellValue} is not on rank {rank}");

            int[] distances = {
                Math.Abs(east - cellValue),
                Math.Abs(north - cellValue),
                Math.Abs(west - cellValue),
                Math.Abs(south - cellValue),
            };

            return distances.Min();
        }

        public List<int> GetPart2Values()
        {
            if (part2List == null) throw new Exception("Must init values first");
            return part2List.Values.ToList();
        }

        public void CalcPart2Values(Rank lastRank)
        {
            if (rank == 1) part2List = new Dictionary<int, int>() { { 1, 1 } };

            for(int i = smallestCell; i<= seCorner; i++)
            {

            }
        }

        //private int Distance(int currentCellNum, int otherCellNum, Rank lastRank)
        //{
        //    if (!lastRank.CellNumInRank(otherCellNum) || CellNumInRank(otherCellNum)) throw new Exception("cell number must be in the current or prior rank to be adjacent");

        //    if (currentCellNum - 1 == otherCellNum) return 1;

        //}
    }
}
