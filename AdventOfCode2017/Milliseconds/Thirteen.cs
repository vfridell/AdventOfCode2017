using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Milliseconds
{

    public class Layer
    {
        public Layer(int depth)
        {
            Active = false;
            Caught = false;
            Depth = depth;
        }

        public Layer(int depth, int range)
        {
            Active = true;
            Caught = false;
            ScannerLocation = 1;
            MovementVector = 1;
            Range = range;
            Depth = depth;
        }

        public bool Active { get; set; }
        public int ScannerLocation { get; set; }
        public int MovementVector { get; set; }
        public int Range { get; set; }
        public int Depth { get; set; }
        public bool Caught { get; set; }
        public void MoveScanner(bool packetHere)
        {
            if (!Active) return;

            if (packetHere && ScannerLocation == 1) Caught = true;

            if (ScannerLocation == 1) MovementVector = 1;
            if (ScannerLocation == Range) MovementVector = -1;
            ScannerLocation += MovementVector;
        }

        public int ComputedSeverity => Depth * Range;

        public Layer NextLayer { get; set; }
    }

    public class Thirteen
    {
        public static void Part1()
        {
            Layer headLayer = null;
            Layer layer;
            Layer prevLayer = null;
            for (int i = 0; i <= input.Keys.Max(); i++)
            {
                if (input.ContainsKey(i))
                {
                    layer = new Layer(i, input[i]);
                }
                else
                {
                    layer = new Layer(i);
                }
                if (i == 0) headLayer = layer;
                if (prevLayer != null) prevLayer.NextLayer = layer;
                prevLayer = layer;
            }

            for (int i = 0; i <= input.Keys.Max(); i++)
            {
                AdvanceTime(i, headLayer);
            }

            Console.WriteLine($"Severity was: {ComputeSeverity(headLayer)}");
        }

        public static void Part2()
        {
            Layer headLayer = null;
            Layer layer;
            Layer prevLayer = null;
            for (int i = 0; i <= input.Keys.Max(); i++)
            {
                if (input.ContainsKey(i))
                {
                    layer = new Layer(i, input[i]);
                }
                else
                {
                    layer = new Layer(i);
                }
                if (i == 0) headLayer = layer;
                if (prevLayer != null) prevLayer.NextLayer = layer;
                prevLayer = layer;
            }

            int delay = 0;
            int severity = 0;
            do
            {
                Console.Write($"Trying delay of {delay}...");
                delay++;
                for (int i = 0; i <= input.Keys.Max() + delay; i++)
                {
                    AdvanceTime(i - delay, headLayer);
                }
                severity = ComputeSeverity(headLayer);
                Console.WriteLine($"Severity was {severity}");
            }
            while (severity > 0);

            Console.WriteLine($"To achieve severity==0, delay was: {delay}");
        }

        public static int ComputeSeverity(Layer layer)
        {
            int severity = 0;
            while (layer != null)
            {
                if(layer.Caught) severity += layer.ComputedSeverity;
                layer = layer.NextLayer;
            }
            return severity;
        }

        public static void AdvanceTime(int packetPos, Layer layer)
        {
            while(layer != null)
            {
                layer.MoveScanner(packetPos == layer.Depth);
                layer = layer.NextLayer;
            }
        }

        public static Dictionary<int,int> input = new Dictionary<int, int>() {
{0,3},
{1,2},
{2,4},
{4,6},
{6,4},
{8,6},
{10,5},
{12,8},
{14,8},
{16,6},
{18,8},
{20,6},
{22,10},
{24,8},
{26,12},
{28,12},
{30,8},
{32,12},
{34,8},
{36,14},
{38,12},
{40,18},
{42,12},
{44,12},
{46,9},
{48,14},
{50,18},
{52,10},
{54,14},
{56,12},
{58,12},
{60,14},
{64,14},
{68,12},
{70,17},
{72,14},
{74,12},
{76,14},
{78,14},
{82,14},
{84,14},
{94,14},
{96,14},

        };
    }



}
