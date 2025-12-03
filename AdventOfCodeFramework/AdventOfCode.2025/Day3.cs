using AdventOfCode.Framework;
using AdventOfCodeFramework;
using Grammr;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2025;

public sealed class Day3 : ICanGiveASolution
{
    [Theory]
    [InlineData("987654321111111\r\n811111111111119\r\n234234234234278\r\n818181911112111", "357")]
    [FileData(typeof(Day3), "17193")]
    public void Day3_1(string input, string answer)
        => Solution1(input).ShouldBe(answer);

    [Theory]
    [InlineData("987654321111111\r\n811111111111119\r\n234234234234278\r\n818181911112111", "3121910778619")]
    [InlineData("5273162326562522665463274364443266744556153632225542544252553244422713343255264252473211466325514322", "777776655432")]
    [InlineData("2917789753933567375786362489139167748494396753874222846657525985975984295875277439443638676981968588", "999999999999")]
    [FileData(typeof(Day3), "171297349921310")]
    public void Day3_2(string input, string answer)
        => Solution2(input).ShouldBe(answer);

    public string Solution1(string input)
    {
        var values = input.ReadAndSplitList<int>();
        var total = 0;
        foreach(var bank in values)
        {
            var highest = bank.Max();
            var restofbank = bank.Where((v) => v.index > highest.index);
            IndexedListItem<int> secondhighest;
            if (restofbank.Any())
            {
                secondhighest = bank.Where((v) => v.index > highest.index).Max();
            }
            else
            {
                secondhighest = bank.Where((v) => v.index != highest.index).Max();
            }
            var result = 0;
            if(highest.index < secondhighest.index)
            {
                result = int.Parse($"{highest.value}{secondhighest.value}");
            }
            else if(highest.index > secondhighest.index)
            {
                result = int.Parse($"{secondhighest.value}{highest.value}");
            }
            total += result;
        }
        return $"{total}";
    }

    public string Solution2(string input)
    {
        var values = input.ReadAndSplitList<int>();
        long total = 0;
        foreach (var bank in values)
        {
            var biggestset = FindBiggestSet(bank, bank.OrderByDescending(o => o.index).Take(12).ToList()).OrderBy(o => o.index);
            var result = "";
            foreach(var value in biggestset)
            {
                result = $"{result}{value.value}";
            }
            total += long.Parse(result);
        }
        return $"{total}";
    }

    public static IndexedListItem<int>[] FindBiggestSet(IndexedListItem<int>[] bank, List<IndexedListItem<int>> currentsolution)
    {
        var lastValue = currentsolution.OrderBy(o => o.index).First();
        var bankselection = bank.Where(o => o.index <= lastValue.index).ToArray();
        var highestnumber = bankselection.Max();
        var remainingbank = bank.Where(o => o.index > highestnumber.index).ToArray();
        currentsolution.Remove(lastValue);
        currentsolution.Add(highestnumber);
        if (remainingbank.Length == 0 || currentsolution.Count == 1)
        {
            return currentsolution.ToArray();
        }
        else
        {
            var recursivesolution = currentsolution.OrderByDescending(o => o.index).Take(currentsolution.Count - 1);
            return [highestnumber, .. FindBiggestSet(remainingbank, recursivesolution.ToList())];
        }
    }
}
