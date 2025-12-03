using AdventOfCode.Framework;
using AdventOfCodeFramework;
using System.Collections.Generic;

namespace AdventOfCode2025;

public sealed class Day2 : ICanGiveASolution
{
    [Theory]
    [InlineData("11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124", "1227775554")]
    [FileData(typeof(Day2), "21898734247")]
    public void Day2_1(string input, string answer)
        => Solution1(input).ShouldBe(answer);

    [Theory]
    [InlineData("11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124", "4174379265")]
    [FileData(typeof(Day2), "28915664389")]
    public void Day2_2(string input, string answer)
        => Solution2(input).ShouldBe(answer);

    public string Solution1(string input)
    {
        var values = input.ReadList(",", "-");
        List<long> validids = [];
        foreach (var value in values)
        {
            var smallest = value[0];
            var largest = value[1];
            if (smallest.Length % 2 != 0 && largest.Length % 2 != 0)
                continue;
            if (smallest.Length % 2 != 0 && largest.Length % 2 == 0)
            {
                smallest = "1000000000000000".Substring(0, largest.Length);
            }
            if (largest.Length % 2 != 0 && smallest.Length % 2 == 0)
            {
                largest = "999999999999999".Substring(0, smallest.Length);
            }
            var length = Math.Max(smallest.Length, largest.Length);
            if (length % 2 != 0)
            {
                length++;
            }
            if (smallest.Length != length)
            {
                smallest = smallest.PadLeft(length, '0');
            }
            if (largest.Length != length)
            {
                largest = largest.PadLeft(length, '0');
            }
            var firsthalfsmallest = long.Parse(smallest.Substring(0, length / 2));
            var firsthalflargest = long.Parse(largest.Substring(0, length / 2));

            var secondhalfsmallest = long.Parse(smallest.Substring(length / 2));
            var secondhalflargest = long.Parse(largest.Substring(length / 2));

            for (var i = firsthalfsmallest; i <= firsthalflargest; i++)
            {
                validids.Add(long.Parse($"{i}{i}"));
            }

            if (firsthalfsmallest < secondhalfsmallest)
            {
                validids.Remove(long.Parse($"{firsthalfsmallest}{firsthalfsmallest}"));

            }
            if (firsthalflargest > secondhalflargest)
            {
                validids.Remove(long.Parse($"{firsthalflargest}{firsthalflargest}"));

            }
        }
        return $"{validids.Sum(v => v)}";
    }

    public string Solution2(string input)
    {
        var values = input.ReadList(",", "-");
        HashSet<long> validids = [];
        foreach (var value in values)
        {
            var smallest = value[0];
            var largest = value[1];
            var digits = Math.Max(smallest.Length, largest.Length);
            for (var x = 2; x <= digits; x++)
                {
                if (!ResolveFor(ref validids, smallest, largest, x))
                {
                    continue;
                }
            }
        }
        return $"{validids.Sum(v => v)}";
    }

    private static bool ResolveFor(ref HashSet<long> validids, string smallest, string largest, int amountofnumbers)
    {
        if (smallest.Length % amountofnumbers != 0 && largest.Length % amountofnumbers != 0)
            return false;
        if (smallest.Length % amountofnumbers != 0 && largest.Length % amountofnumbers == 0)
        {
            smallest = "1000000000000000".Substring(0, largest.Length);
        }
        if (largest.Length % amountofnumbers != 0 && smallest.Length % amountofnumbers == 0)
        {
            largest = "999999999999999".Substring(0, smallest.Length);
        }
        var length = Math.Max(smallest.Length, largest.Length);
        if (length % amountofnumbers != 0)
        {
            length++;
        }
        if (smallest.Length != length)
        {
            smallest = smallest.PadLeft(length, '0');
        }
        if (largest.Length != length)
        {
            largest = largest.PadLeft(length, '0');
        }
        var smallestlist = new List<long>();
        for (var i = 0; i < amountofnumbers; i++)
        {
            smallestlist.Add(long.Parse(smallest.Substring(i * (length / amountofnumbers), length / amountofnumbers)));
        }
        var largestlist = new List<long>();
        for (var i = 0; i < amountofnumbers; i++)
        {
            largestlist.Add(long.Parse(largest.Substring(i * (length / amountofnumbers), length / amountofnumbers)));
        }

        for (var i = smallestlist[0]; i <= largestlist[0]; i++)
        {
            var id = long.Parse(CreateValidId(amountofnumbers, i));
            if (id >= long.Parse(smallest) && id <= long.Parse(largest))
                validids.Add(id);
        }

        return true;
    }

    private static string CreateValidId(int amountofnumbers, long i)
    {
        var x = "";
        for (int y = 0; y < amountofnumbers; y++)
        {
            x = $"{x}{i}";
        }

        return x;
    }
}
