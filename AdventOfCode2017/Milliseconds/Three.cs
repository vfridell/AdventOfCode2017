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
            //Validate2();

            int input = 368078;
            int r = 2;
            Rank prevRank = new Rank(1);
            Rank currentRank = new Rank(r);
            List<int> part2Values = new List<int> { 0, 1, };
            while(true)
            {
                int currentPart2Value;
                for (int i = currentRank.smallestCell; i <= currentRank.seCorner; i++)
                {
                    currentPart2Value = 0;

                    for (int j = prevRank.smallestCell; j < i; j++)
                    {
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

        private static bool Adjacent(int currentCellNum, int otherCellNum)
        {
            if (currentCellNum - 1 == otherCellNum) return true;
            if (otherCellNum == 1 && currentCellNum <= 9) return true;

            Rank currentRank = new Rank(RankNum(currentCellNum));
            Rank otherRank = new Rank(RankNum(otherCellNum));
            if (currentRank.rank != otherRank.rank 
                && currentRank.rank - 1 != otherRank.rank) throw new Exception("cell number must be in the current or prior rank to be adjacent");

            Coordinate currCoord = currentRank.GetCoordinate(currentCellNum);
            Coordinate otherCoord = otherRank.GetCoordinate(otherCellNum);
            return Coordinate.AreNeighbors(currCoord, otherCoord);
        }

        private static void Validate2()
        {
            Console.WriteLine($"9 Adjacent to 2: {Adjacent(9, 2)}");
            Console.WriteLine($"25 Adjacent to 24: {Adjacent(25, 24)}");
            Console.WriteLine($"40 Adjacent to 11: {Adjacent(40, 11)}");
            Console.WriteLine($"22 Adjacent to 20: {Adjacent(22, 20)}");
            Console.WriteLine($"48 Adjacent to 46: {Adjacent(48, 46)}");

            Rank rank1 = new Rank(1);
            Rank rank2 = new Rank(2);
            Rank rank3 = new Rank(3);
            Rank rank4 = new Rank(4);
            Rank rank5 = new Rank(5);

            Rank rank = rank3;

            for (int i = rank.smallestCell; i <= rank.seCorner; i++)
            {
                Console.WriteLine($"Value {i} is at {rank.GetCoordinate(i)}");
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

        public Coordinate GetCoordinate(int cellValue)
        {
            if(cellValue <= east)
                return new Coordinate(-DistanceFromNearestSide(cellValue), rank-1);
            if(cellValue <= neCorner)
                return new Coordinate(DistanceFromNearestSide(cellValue), rank-1);
            if(cellValue <= north)
                return new Coordinate(rank - 1, DistanceFromNearestSide(cellValue));
            if(cellValue <= nwCorner)
                return new Coordinate(rank - 1, -DistanceFromNearestSide(cellValue));
            if(cellValue <= west)
                return new Coordinate(DistanceFromNearestSide(cellValue), -(rank-1));
            if(cellValue <= swCorner)
                return new Coordinate(-DistanceFromNearestSide(cellValue), -(rank-1));
            if(cellValue <= south)
                return new Coordinate(-(rank - 1), -DistanceFromNearestSide(cellValue));
            if(cellValue <= seCorner)
                return new Coordinate(-(rank - 1), DistanceFromNearestSide(cellValue));

            throw new ArgumentException($"Error with rank {rank} for value {cellValue}");
        }



    }

}
