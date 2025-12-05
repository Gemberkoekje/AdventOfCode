using AdventOfCode.Framework;
using AdventOfCodeFramework;
using System.Collections.Generic;

namespace AdventOfCode2025;

public sealed class Day4 : ICanGiveASolution
{
    [Theory]
    [InlineData("..@@.@@@@.\r\n@@@.@.@.@@\r\n@@@@@.@.@@\r\n@.@@@@..@.\r\n@@.@@@@.@@\r\n.@@@@@@@.@\r\n.@.@.@.@@@\r\n@.@@@.@@@@\r\n.@@@@@@@@.\r\n@.@.@@@.@.", "13")]
    [InlineData("...\r\n.@.\r\n...", "1")]
    [FileData(typeof(Day4), "1437")]
    public void Day4_1(string input, string answer)
        => Solution1(input).ShouldBe(answer);

    [Theory]
    [InlineData("..@@.@@@@.\r\n@@@.@.@.@@\r\n@@@@@.@.@@\r\n@.@@@@..@.\r\n@@.@@@@.@@\r\n.@@@@@@@.@\r\n.@.@.@.@@@\r\n@.@@@.@@@@\r\n.@@@@@@@@.\r\n@.@.@@@.@.", "43")]
    [FileData(typeof(Day4), "8765")]
    public void Day4_2(string input, string answer)
        => Solution2(input).ShouldBe(answer);

    public string Solution1(string input)
    {
        var values = input.ReadAndCoordList("@");
        var total = 0;
        var intermediate = new Dictionary<(int X, int Y), int?>();
        var totalmaxx = values.Max(v => v.Key.X);
        var totalmaxy = values.Max(v => v.Key.Y);
        foreach (var value in values)
        {
            if (value.Value == 0)
                continue;
            if (!intermediate.ContainsKey((value.Key.X, value.Key.Y)))
            {
                intermediate.Add((value.Key.X, value.Key.Y), 0);
            }
            var minx = value.Key.X - 1;
            var maxx = value.Key.X + 1;
            var miny = value.Key.Y - 1;
            var maxy = value.Key.Y + 1;
            for (int x = minx; x <= maxx; x++)
            {
                for (int y = miny; y <= maxy; y++)
                {
                    if (x == value.Key.X && y == value.Key.Y)
                        continue;
                    var cv = values.GetValueOrDefault((x, y));
                    if (cv != default)
                    {
                        intermediate[(value.Key.X, value.Key.Y)] += cv;
                    }
                }
            }
        }
#if DEBUG
        for (int y = 0; y <= totalmaxy; y++)
        {
            for (int x = 0; x <= totalmaxx; x++)
            {
                Console.Write($"{(intermediate.GetValueOrDefault((x,y)) != default ? intermediate.GetValueOrDefault((x, y)) : " ")}");
            }
            Console.WriteLine();
        }
#endif
        total = intermediate.Values.Count(v => v < 4);
        return $"{total}";
    }

    public string Solution2(string input)
    {
        var values = input.ReadAndCoordList("@");
        var total = 0;
        var intermediate = new Dictionary<(int X, int Y), int?>();
        var totalmaxx = values.Max(v => v.Key.X);
        var totalmaxy = values.Max(v => v.Key.Y);
        foreach (var value in values)
        {
            if (value.Value == 0)
                continue;
            if (!intermediate.ContainsKey((value.Key.X, value.Key.Y)))
            {
                intermediate.Add((value.Key.X, value.Key.Y), 0);
            }
            var minx = value.Key.X - 1;
            var maxx = value.Key.X + 1;
            var miny = value.Key.Y - 1;
            var maxy = value.Key.Y + 1;
            for (int x = minx; x <= maxx; x++)
            {
                for (int y = miny; y <= maxy; y++)
                {
                    if (x == value.Key.X && y == value.Key.Y)
                        continue;
                    var cv = values.GetValueOrDefault((x, y));
                    if (cv != default)
                    {
                        intermediate[(value.Key.X, value.Key.Y)] += cv;
                    }
                }
            }
        }

        while(intermediate.Any(i => i.Value >= 4))
        {
            var toRemove = intermediate.Where(i => i.Value < 4).Select(i => i.Key).ToList();
            if (!toRemove.Any())
                break;
            foreach (var key in toRemove)
            {
                total++;
                intermediate.Remove(key);
                var minx = key.X - 1;
                var maxx = key.X + 1;
                var miny = key.Y - 1;
                var maxy = key.Y + 1;
                for (int x = minx; x <= maxx; x++)
                {
                    for (int y = miny; y <= maxy; y++)
                    {
                        var cv = intermediate.GetValueOrDefault((x, y));
                        if (cv != default)
                        {
                            intermediate[(x, y)]--;
                        }
                    }
                }
            }
#if DEBUG
            for (int y = 0; y <= totalmaxy; y++)
            {
                for (int x = 0; x <= totalmaxx; x++)
                {
                    Console.Write($"{(intermediate.GetValueOrDefault((x, y)) != default ? intermediate.GetValueOrDefault((x, y)) : " ")}");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---");
#endif
        }
        return $"{total}";
    }
}
