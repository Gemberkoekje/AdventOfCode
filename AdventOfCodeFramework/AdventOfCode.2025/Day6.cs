using AdventOfCode.Framework;
using AdventOfCodeFramework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode2025;

public sealed class Day6 : ICanGiveASolution
{
    [Theory]
    [InlineData("123 328  51 64 \r\n 45 64  387 23 \r\n  6 98  215 314\r\n*   +   *   +  ", "4277556")]
    [FileData(typeof(Day6), "6171290547579")]
    public void Day6_1(string input, string answer)
        => Solution1(input).ShouldBe(answer);

    [Theory]
    [InlineData("123 328  51 64 \r\n 45 64  387 23 \r\n  6 98  215 314\r\n*   +   *   +  ", "3263827")]
    [FileData(typeof(Day6), "8811937976367")]
    public void Day6_2(string input, string answer)
        => Solution2(input).ShouldBe(answer);

    public string Solution1(string input)
    {
        var set = input.ReadList();
        var numbers = new List<List<long>>();
        foreach (var list in set.Take(set.Length - 1))
        {
            var values = list.ReadListRegex<long>("(\\d+\\D*)");
            int index = 0;
            foreach (var value in values)
            {
                if (numbers.Count <= index)
                {
                    numbers.Add(new List<long>());
                }
                numbers[index].Add(value);
                index++;
            }
        }
        long total = 0;
        var types = set.Last().ReadListRegex<string>("([*+])");
        int index2 = 0;
        foreach (var value in types)
        {
            if (value == "+")
            {
                total += numbers[index2].Sum();
            }
            else if (value == "*")
            {
                long product = numbers[index2][0];
                foreach(var num in numbers[index2].Skip(1))
                {
                    product *= num;
                }
                total += product;
            }
            index2++;
        }
        return $"{total}";
    }

    public string Solution2(string input)
    {
        var set = input.ReadList();
        var transposed = new List<string>();
        var x = 0;
        foreach (var line in set)
        {
            var y = 0;
            foreach (var chara in line)
            {
                if (transposed.Count <= y)
                {
                    transposed.Add(string.Empty);
                }
                transposed[y] += chara;
                y++;
            }
        }
        long total = 0;
        var op = "";
        var numbers = new List<long>();
        foreach(var value in transposed)
        {
            var usevalue = value;
            if(value.Last() == '+')
            {
                op = "+";
                usevalue = value.Substring(0, value.Length - 1);
            }
            if (value.Last() == '*')
            {
                op = "*";
                usevalue = value.Substring(0, value.Length - 1);
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                if(op == "+")
                {
                    total += numbers.Sum();
                }
                else if (op == "*")
                {
                    long product = numbers[0];
                    foreach (var num in numbers.Skip(1))
                    {
                        product *= num;
                    }
                    total += product;
                }
                numbers = new List<long>();
            }
            else
            {
                numbers.Add(long.Parse(usevalue));
            }
        }
        if (op == "+")
        {
            total += numbers.Sum();
        }
        else if (op == "*")
        {
            long product = numbers[0];
            foreach (var num in numbers.Skip(1))
            {
                product *= num;
            }
            total += product;
        }

        return $"{total}";
    }
}
