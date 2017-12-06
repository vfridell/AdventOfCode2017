using AdventOfCode2017.Models;
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
            Validate2();

            int input = 368078;
            int r = 3;
            Rank prevRank = new Rank(2);
            Rank currentRank = new Rank(r);
            List<int> part2Values = new List<int> { 0, 1, 1,2,4,5,10,11,23,25 };
            while(true)
            {
                int currentPart2Value;
                for (int i = currentRank.smallestCell; i <= currentRank.seCorner; i++)
                {
                    currentPart2Value = 0;
                    if (i > 2)
                    {
                        currentPart2Value = part2Values[i - 1];
                        if (i > 3 && Adjacent(i, i - 2)) currentPart2Value += part2Values[i - 2];
                    }

                    for (int j = prevRank.smallestCell; j <= prevRank.seCorner; j++)
                    {
                        if (j == i - 1) continue;
                        if (j == i - 2) continue;
                        if (Adjacent(i, j)) currentPart2Value += part2Values[j];
                    }

                    part2Values.Add(currentPart2Value);
                    Console.WriteLine($"Position {i} => {currentPart2Value}");
                    if(currentPart2Value >= input)
                    {
                        Console.WriteLine($"Answer is {currentPart2Value}");
                        return;
                    }
                }
                prevRank = currentRank;
                currentRank = new Rank(++r);
            } 
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

        public static double Angle(int value)
        {
            Rank rank = new Rank(RankNum(value));
            //double angle = 360.0 * ((rank.seCorner - (double)value) / (rank.seCorner - (double)rank.smallestCell - 1));
            double angle = (rank.seCorner - (double)value) * rank.unitAngle;
            return angle;
        }

        public static double AngleBetween(int value1, int value2)
        {
            double angle1 = Angle(value1);
            double angle2 = Angle(value2);
            double a = angle1 - angle2;

            Func<double, double, double> mod = (v,n) => { return v - Math.Floor(v / n) * n; };
            a = mod((a + 180), 360) - 180;
            return Math.Abs(a);
        }

        public static double Distance(int value1, int value2)
        {
            Rank rank1 = new Rank(RankNum(value1));
            Rank rank2 = new Rank(RankNum(value2));
            double angleBetween = AngleBetween(value1, value2);
            double radians = angleBetween * Math.PI / 180.0;
            int distance1 = rank1.DistanceFromOrigin(value1);
            int distance2 = rank2.DistanceFromOrigin(value2);

            double length = (distance1 * distance1) + (distance2 * distance2) - (2 * distance1 * distance2 * Math.Cos(radians));
            return length;
        }

        private static bool Adjacent(int currentCellNum, int otherCellNum)
        {
            if (currentCellNum - 1 == otherCellNum) return true;
            if (otherCellNum == 1 && currentCellNum <= 9) return true;

            if (currentCellNum <= otherCellNum) throw new Exception("current cell should be greater than the other cell");

            Rank currentRank = new Rank(RankNum(currentCellNum));
            Rank otherRank = new Rank(RankNum(otherCellNum));
            if (currentRank.rank != otherRank.rank 
                && currentRank.rank - 1 != otherRank.rank) throw new Exception("cell number must be in the current or prior rank to be adjacent");


            double length = Distance(currentCellNum, otherCellNum);

            return Math.Floor(length) <= 2;
        }

        private static void Validate2()
        {
            Console.WriteLine($"9 Adjacent to 2: {Adjacent(9, 2)}");
            Console.WriteLine($"25 Adjacent to 24: {Adjacent(25, 24)}");
            Console.WriteLine($"40 Adjacent to 11: {Adjacent(40, 11)}");

            Rank rank1 = new Rank(1);
            Rank rank2 = new Rank(2);
            Rank rank3 = new Rank(3);
            Rank rank4 = new Rank(4);
            Rank rank5 = new Rank(5);

            Rank rank = rank3;
            for (int i = rank.smallestCell; i <= rank.seCorner; i++)
            {
                for (int j = rank.smallestCell; j <= rank.seCorner; j++)
                {
                    //Console.WriteLine($"Angle between {i} and {j} is {AngleBetween(i, j)}");
                    Console.WriteLine($"Distance between {i} and {j} is {Distance(i, j)}");
                }
            }

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
        public double unitAngle;

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
            smallestCell = n==1 ? 1 : east - (LengthOfSide / 2) + 1;
            unitAngle = 360.0 * (  1.0 / (seCorner - (smallestCell - 1)));
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

        public int SignedDistanceFromNearestSide(int cellValue)
        {
            if (!CellNumInRank(cellValue))
                throw new Exception($"Value {cellValue} is not on rank {rank}");

            int[] distances = {
                east - cellValue,
                north - cellValue,
                west - cellValue,
                south - cellValue,
            };

            return distances.Min(d => Math.Abs(d));
        }

        public Coordinate GetCoordinate(int cellValue)
        {

        }



    }

}
