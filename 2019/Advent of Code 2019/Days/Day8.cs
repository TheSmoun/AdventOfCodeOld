using System;
using System.Collections.Generic;
using System.Linq;
using AoC2019.Extensions;
using MoreLinq;

namespace AoC2019.Days
{
    public sealed class Day8 : DayBase<List<int[,]>, string>
    {
        private const int ImageWidth = 25;
        private const int ImageHeight = 6;

        public override string Name => "Day 8: Space Image Format";

        public override List<int[,]> ParseInput(IEnumerable<string> lines)
        {
            var pixels = lines.Single().ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();

            var image = new List<int[,]>();
            foreach (var layerPixels in pixels.Batch(ImageWidth * ImageHeight))
            {
                var layer = new int[ImageHeight, ImageWidth];
                var y = 0;
                foreach (var row in layerPixels.Batch(ImageWidth))
                {
                    var x = 0;
                    foreach (var i in row)
                    {
                        layer[y, x++] = i;
                    }

                    y++;
                }

                image.Add(layer);
            }

            return image;
        }

        public override string RunPart1(List<int[,]> input)
        {
            var i = 0;
            var min = int.MaxValue;
            var minLayer = 0;
            foreach (var zeros in input.Select(l => l.Cast<int>()).Select(px => px.Count(p => p == 0)))
            {
                if (zeros < min)
                {
                    min = zeros;
                    minLayer = i;
                }

                i++;
            }

            var layer = input[minLayer].Cast<int>().ToList();
            return (layer.Count(p => p == 1) * layer.Count(p => p == 2)).ToString();
        }

        public override string RunPart2(List<int[,]> input)
        {
            for (var y = 0; y < ImageHeight; y++)
            {
                for (var x = 0; x < ImageWidth; x++)
                {
                    var pixel = input.Select(l => l[y, x]).First(p => p != 2);
                    ConsoleEx.WriteColored("#", pixel == 0 ? ConsoleColor.Black : ConsoleColor.White);
                }

                Console.WriteLine();
            }

            return "LBRCE";
        }
    }
}
