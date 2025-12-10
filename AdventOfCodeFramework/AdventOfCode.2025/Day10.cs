using AdventOfCode.Framework;
using AdventOfCodeFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode2025;

public sealed class Day10 : ICanGiveASolution
{
    [Theory]
    [InlineData("[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}\r\n[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}\r\n[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}", "7")]
    [FileData(typeof(Day10), "?")]
    public void Day10_1(string input, string answer)
        => Solution1(input).ShouldBe(answer);

    [Theory]
    [InlineData("[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}\r\n[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}\r\n[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}", "?")]
    [FileData(typeof(Day10), "?")]
    public void Day10_2(string input, string answer)
        => Solution2(input).ShouldBe(answer);

    public string Solution1(string input)
    {
        var set = input.ReadList();
        long totaldepth = 0;
        foreach (var value in set)
        {
            var capture = Regex.Matches(value, "\\[(.*)\\] \\((.*)\\) \\{(.*)\\}");
            foreach (Match match in capture)
            {
                var pattern = match.Groups[1].Value.Select(v => v == '#').ToArray();
                var buttons = match.Groups[2].Value.Split(") (").Select(t => t.Split(",").Select(v => int.Parse(v)).ToList()).ToList();

                bool found = false;
                int depth = 0;
                for (int recursivity = 0; recursivity < 6 && !found; recursivity++)
                {
                    (found, depth) = FindPattern(pattern, new bool[pattern.Length], buttons, found, recursivity, 1);
                    Console.WriteLine($"-- {recursivity} --");
                }
                totaldepth += depth;
            }
        }

        return $"{totaldepth}";
    }

    private (bool found, int depth) FindPattern(bool[] pattern, bool[] currentpattern, List<List<int>> buttons, bool found, int recursive, int depth)
    {
        foreach (var button in buttons)
        {
            Console.WriteLine($"{button.Select(b => b.ToString()).Aggregate((a, b) => a + ", " + b)}");
            var result = PressAButton(currentpattern, button);
            Console.WriteLine($"{result.Select(b => b.ToString()).Aggregate((a, b) => a + ", " + b)}");
            found = true;
            for(int i = 0; i < result.Length; i++)
            {
                if (result[i] != pattern[i])
                {
                    found = false;
                    break;
                }
            }
            if (!found && recursive > 0)
            {
                (found, depth) = FindPattern(pattern, result, buttons, found, recursive - 1, depth + 1);
            }
            if (found)
                return (found, depth);
        }

        return (found, depth);
    }

    public bool[] PressAButton(bool[] currentPattern, List<int> button)
    {
        foreach (var light in button)
        {
            currentPattern[light] = !currentPattern[light];
        }
        return currentPattern;
    }

    public string Solution2(string input)
    {
        var set = input.ReadListAndCastToTuple2D<long>().OrderBy(s => s.Y).ThenBy(s => s.X).ToList();
        long largest = 0;
        return $"{largest}";
    }
}
