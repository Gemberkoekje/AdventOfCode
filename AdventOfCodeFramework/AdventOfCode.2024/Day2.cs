using AdventOfCodeFramework;
using DotNetProjectFile.Resx;
using System.Collections.Generic;

namespace AdventOfCode2024;

public sealed class Day2
{
    [Theory]
    [InlineData("7 6 4 2 1\r\n1 2 7 8 9\r\n9 7 6 2 1\r\n1 3 2 4 5\r\n8 6 4 4 1\r\n1 3 6 7 9", "2")]
    [FileData(typeof(Day2), "359")]
    public void Day2_1(string input, string answer)
        => Solution2_1(input).ShouldBe(answer);

    [Theory]
    [InlineData("7 6 4 2 1\r\n1 2 7 8 9\r\n9 7 6 2 1\r\n1 3 2 4 5\r\n8 6 4 4 1\r\n1 3 6 7 9", "4")]
    [FileData(typeof(Day2), "418")]
    public void Day2_2(string input, string answer)
        => Solution2_2(input).ShouldBe(answer);

    public static string Solution2_1(string input)
    {
        var values = input.ReadList<int>(" ");
        var total = 0;
        foreach (var value in values)
        {
            var count = 0;
            var consideredvalue = 0;
            bool? isincreasing = null;
            bool issafe = true;
            foreach (var intvalue in value)
            {
                if (count == 0)
                {
                    consideredvalue = intvalue;
                }
                else
                {
                    if (isincreasing == null)
                    {
                        if (intvalue > consideredvalue)
                            isincreasing = true;
                        else
                            isincreasing = false;
                    }
                    else
                    {
                        if (intvalue > consideredvalue && isincreasing == false)
                        {
                            issafe = false;
                            break;
                        }
                        if (intvalue < consideredvalue && isincreasing == true)
                        {
                            issafe = false;
                            break;
                        }
                    }
                    if (Math.Abs(intvalue - consideredvalue) < 1 || Math.Abs(intvalue - consideredvalue) > 3)
                    {
                        issafe = false;
                        break;
                    }
                    consideredvalue = intvalue;
                }
                count++;
            }
            if (issafe)
                total++;
        }
        return $"{total}";
    }

    public static string Solution2_2(string input)
    {
        var values = input.ReadList<int>(" ");
        var total = 0;
        foreach (var value in values)
        {
            var safety = DetermineSafetyLevel(value);
            if (safety == 0)
                total++;
            else
            {
                for (int i = 0; i < value.Count(); i++)
                {
                    var newlist = value.ToList();
                    newlist.RemoveAt(i);
                    var newsafety = DetermineSafetyLevel(newlist);
                    if (newsafety == 0)
                    {
                        total++;
                        break;
                    }
                }
            }
        }
        return $"{total}";
    }

    private static int DetermineSafetyLevel(IEnumerable<int> values2)
    {
        var count = 0;
        var consideredvalue = 0;
        bool? isincreasing = null;
        int safecount = 0;
        foreach (var value2 in values2)
        {
            var intvalue = value2;
            if (count == 0)
            {
                consideredvalue = intvalue;
                count++;
                safecount++;
            }
            else
            {
                count++;
                if (isincreasing == null)
                {
                    if (intvalue > consideredvalue)
                        isincreasing = true;
                    else
                        isincreasing = false;
                }
                else
                {
                    if (intvalue > consideredvalue && isincreasing == false)
                    {
                        continue;
                    }
                    if (intvalue < consideredvalue && isincreasing == true)
                    {
                        continue;
                    }
                }
                if (Math.Abs(intvalue - consideredvalue) < 1 || Math.Abs(intvalue - consideredvalue) > 3)
                {
                    continue;
                }
                safecount++;
                consideredvalue = intvalue;
            }
        }
        return count - safecount;
    }
}
