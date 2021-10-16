﻿using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Tasks.Year2020.Day2
{
    public class Part2 : ITask<int>
    {
        public int Solution(string input)
        {
            var count = 0;
            Regex countRegex = new Regex(@"^\d*",
              RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex maxCountRegex = new Regex(@"-(\d*)");
            Regex characterRegex = new Regex(@"[a-z]");
            Regex stringRegex = new Regex(@"(: )([a-z]*)");
            var numbers = input.Split("\n");
            Console.WriteLine(numbers[0]);
            foreach(string s in numbers){
                var minCount = countRegex.Matches(s)[0].Value;
                var maxCount = maxCountRegex.Matches(s)[0].Groups[1].Value;
                var character = characterRegex.Matches(s)[0].Value;
                var stringy = stringRegex.Matches(s)[0].Groups[2].Value;
                Console.WriteLine($"{minCount}");
                Console.WriteLine($"{maxCount}");
                Console.WriteLine($"{character}");
                Console.WriteLine($"{stringy}");
                var valid = isValid(int.Parse(minCount), int.Parse(maxCount), character[0], stringy);
                if (valid){
                    count++;
                }
            }
           return count;
        }
	private bool isValid(int minCount, int maxCount, char character, string input){
        minCount = minCount -1;
        maxCount = maxCount -1;
        var count = 0;
        if(minCount < input.Length  && input[minCount] == character){
            count++;
        }
        if(maxCount < input.Length  &&input[maxCount] == character){
            count++;
        }
        if(count ==1){
            return true;
        }
        return false;
        }
    }
}
