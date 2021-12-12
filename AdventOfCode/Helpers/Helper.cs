using System;
using System.Collections.Generic;
using AdventOfCode.Tasks.Year2021.Day11;

namespace AdventOfCode.Tasks._2021
{
    public static class Helper
    {
        public static void Print<T>(this T[,] heightMap)
        {
            for (var y = 0; y < heightMap.GetLength(1); y++)
            {
                for (var x = 0; x < heightMap.GetLength(0); x++)
                {
                    Console.Write(heightMap[x, y]);
                }

                Console.WriteLine();
            }
        }

        public static void ForEachItem<T>(this T[,] twoDArray, Action<int, int> function)
        {
            for (var x = 0; x < twoDArray.GetLength(0); x++)
            {
                for (var y = 0; y < twoDArray.GetLength(1); y++)
                {
                    function(x,y);
                }
            }
        }

        public static void CreateTwoDArrayFromString<T>(string[] lines, Func<char, T> function, T[,] grid) 
        {
            var y = 0;
            foreach (var line in lines)
            {
                var x = 0;
                foreach (var c in line)
                {
                    grid[x, y] = function(c);
                    x++;
                }
                y++;
            }
        }

        public static void UpdateDict(this Dictionary<char, long> dict, char c, long newVal)
        {
            dict.TryGetValue(c, out var val);
            dict.Remove(c);
            dict.Add(c, newVal);
        }

    }
}