using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Tasks._2021;

namespace AdventOfCode.Tasks.Year2021.Day15
{
    public class Solution : Solvable
    {
        public long Part1(string input)
        {
            return Solve(input, false);
        }

        public long Part2(string input)
        {
            return Solve(input, true);
        }

        public long Solve(string input, bool duplicateBy5)
        {
            var rr = new int[] {0, 1, 0, -1};
            var cc = new int[] {1, 0, -1, 0};
            var pathCost = new Dictionary<(int, int), long>();
            var queue = new PriorityQueue<(long, int, int), long>();
            var grid = GetGrid(input, queue, pathCost, duplicateBy5);
            var visited = new HashSet<(int, int)>();
            var source = (0, 0);
            var destination = (grid.GetLength(0) - 1, grid.GetLength(1) - 1);
            pathCost[source] = 0;
            queue.Enqueue((0, 0, 0), 0);
            while (queue.Count > 0)
            {
                var (dist, x, y) = queue.Dequeue();
                
                if (x < 0 || x > grid.GetLength(0) - 1 || y < 0 || y > grid.GetLength(1) - 1)
                {
                    continue;
                }

                var localVertex = (x, y);
                var totalCost = dist + grid[x, y];

                if (!visited.Contains(localVertex) || totalCost < pathCost[localVertex])
                {
                    // this path is better
                    pathCost[localVertex] = totalCost;
                    visited.Add(localVertex);
                }
                else
                {
                    continue;
                }

                if (destination == localVertex)
                {
                    break;
                }


                for (var i = 0; i < rr.Length; i++)
                {
                    var (xx, yy) = (x, y);
                    yy += cc[i];
                    xx += rr[i];
                    
                    queue.Enqueue((pathCost[localVertex], xx, yy), pathCost[localVertex]);
                }
            }

            return pathCost[destination] - grid[source.Item1, source.Item2];
        }

        private long[,] GetGrid(string input, PriorityQueue<(long, int, int), long> queue, Dictionary<(int, int), long> pathCost,
            bool duplicateBy5)
        {
            var lines = input.Split("\n");
            var gridStart = new long[lines[0].Length, lines.Length];

            Helper.CreateTwoDArrayFromString(lines, (c, x, y) => { return long.Parse(c.ToString()); }, gridStart);
            // gridStart.Print();
            long[,] grid;
            if (duplicateBy5)
            {
                grid = new long[lines[0].Length * 5, lines.Length * 5];
                var width = lines[0].Length;
                gridStart.ForEachItem((x, y) =>
                {
                    for (var xx = 0; xx < 5; xx++)
                    {
                        for (var yy = 0; yy < 5; yy++)
                        {
                            grid[x + width * xx, y + width * yy] = GetNewGridValue(gridStart, x, y, xx, yy);
                        }
                    }
                });
            }
            else
            {
                grid = gridStart;
            }

            grid.ForEachItem((x, y) => { pathCost[(x, y)] = long.MaxValue; });

            return grid;
        }

        private static long GetNewGridValue(long[,] grid, int x, int y, int xx, int yy)
        {
            return (grid[x, y] + xx + yy - 1) % 9 + 1;
        }
    }
}