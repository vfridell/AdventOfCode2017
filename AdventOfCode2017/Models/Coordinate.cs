using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Models
{
    public enum Direction
    {
        Left = 60,  // <
        Right = 62, // >
        Up = 43,    // +
        Down = 45   // -
    }

        public struct Coordinate
        {
            public Coordinate(int row, int column)
            {
                Row = row;
                Column = column;
            }
            public int Row;
            public int Column;

            public override string ToString() => $"({Row},{Column})";

            public static Coordinate operator +(Coordinate c1, Coordinate c2) => new Coordinate(c1.Row + c2.Row, c1.Column + c2.Column);
            public static int Distance(Coordinate c1, Coordinate c2) => Math.Abs(c1.Row - c2.Row) + Math.Abs(c1.Column - c2.Column);
            public static bool AreNeighbors(Coordinate c1, Coordinate c2) => Math.Abs(c1.Row - c2.Row) <= 1  && Math.Abs(c1.Column - c2.Column) <= 1;
            public static bool operator ==(Coordinate c1, Coordinate c2) => c1.Row == c2.Row && c1.Column == c2.Column;
            public static bool operator !=(Coordinate c1, Coordinate c2) => c1.Row != c2.Row || c1.Column != c2.Column;

        }
}
