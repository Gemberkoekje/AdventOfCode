using AdventOfCode.Framework;
using AdventOfCodeFramework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode2025;

public sealed class Day8 : ICanGiveASolution
{
    [Theory]
    [InlineData("162,817,812\r\n57,618,57\r\n906,360,560\r\n592,479,940\r\n352,342,300\r\n466,668,158\r\n542,29,236\r\n431,825,988\r\n739,650,466\r\n52,470,668\r\n216,146,977\r\n819,987,18\r\n117,168,530\r\n805,96,715\r\n346,949,466\r\n970,615,88\r\n941,993,340\r\n862,61,35\r\n984,92,344\r\n425,690,689", "40")]
    [FileData(typeof(Day8), "81536")]
    public void Day8_1(string input, string answer)
        => Solution1(input).ShouldBe(answer);

    [Theory]
    [InlineData("162,817,812\r\n57,618,57\r\n906,360,560\r\n592,479,940\r\n352,342,300\r\n466,668,158\r\n542,29,236\r\n431,825,988\r\n739,650,466\r\n52,470,668\r\n216,146,977\r\n819,987,18\r\n117,168,530\r\n805,96,715\r\n346,949,466\r\n970,615,88\r\n941,993,340\r\n862,61,35\r\n984,92,344\r\n425,690,689", "25272")]
    [FileData(typeof(Day8), "7017750530")]
    public void Day8_2(string input, string answer)
        => Solution2(input).ShouldBe(answer);

    public string Solution1(string input)
    {
        var set = input.ReadListAndCastToTuple<long>().ToList();
        int amount = 1000;
        if (set.Count() == 20)
            amount = 10;
        long total = 0;
        var hashset = new HashSet<((long X, long Y, long Z) Item, (long X, long Y, long Z) Other, long DistSq)>();
        for (int a = 0; a < set.Count(); a++)
        {
            for (int b = a + 1; b < set.Count(); b++)
            {
                var item = set[a];
                var other = set[b];
                var distSq = GetLengthSquared(item, other);
                hashset.Add((item, other, distSq));
            }
        }
        var list = hashset.OrderBy(l => l.DistSq).ToList();
        var connected = new List<HashSet<(long X, long Y, long Z)>>();
        for (int x = 0; x < amount; x++)
        {
            var entry = list.First();
            var conentries = connected.Where(c => c.Any(d => d == entry.Item || d == entry.Other));
            if(conentries.Count() <= 1)
            {
                var conentry = conentries.FirstOrDefault();
                if (conentry != null)
                {
                    conentry.Add(entry.Item);
                    conentry.Add(entry.Other);
                }
                else
                {
                    connected.Add(new HashSet<(long X, long Y, long Z)>() { entry.Item, entry.Other });
                }
            }
            else
            {
                var conentry = conentries.First();
                foreach (var other in conentries.Skip(1).ToList())
                {
                    foreach (var item in other)
                    {
                        conentry.Add(item);
                    }
                    connected.Remove(other);
                }
                conentry.Add(entry.Item);
                conentry.Add(entry.Other);
            }
            list.RemoveAt(0);
        }
        var result = connected.OrderByDescending(c => c.Count).ToList();
        return $"{result[0].Count * result[1].Count * result[2].Count}";
    }

    public string Solution2(string input)
    {
        var set = input.ReadListAndCastToTuple<long>().ToList();
        long total = 0;
        var hashset = new HashSet<((long X, long Y, long Z) Item, (long X, long Y, long Z) Other, long DistSq)>();
        for (int a = 0; a < set.Count(); a++)
        {
            for (int b = a + 1; b < set.Count(); b++)
            {
                var item = set[a];
                var other = set[b];
                var distSq = GetLengthSquared(item, other);
                hashset.Add((item, other, distSq));
            }
        }
        var list = hashset.OrderBy(l => l.DistSq).ToList();
        var connected = new List<HashSet<(long X, long Y, long Z)>>();
        connected.AddRange(set.Select(s => new HashSet<(long X, long Y, long Z)>() { s }));
        var final2 = new List<(long X, long Y, long Z)>();
        while (connected.Count != 1)
        {
            var entry = list.First();
            var conentries = connected.Where(c => c.Any(d => d == entry.Item || d == entry.Other));
            if (conentries.Count() <= 1)
            {
                var conentry = conentries.FirstOrDefault();
                if (conentry != null)
                {
                    conentry.Add(entry.Item);
                    conentry.Add(entry.Other);
                }
                else
                {
                    connected.Add(new HashSet<(long X, long Y, long Z)>() { entry.Item, entry.Other });
                }
            }
            else
            {
                if(connected.Count == 2)
                {
                    final2.Add(entry.Item);
                    final2.Add(entry.Other);
                }
                var conentry = conentries.First();
                foreach (var other in conentries.Skip(1).ToList())
                {
                    foreach (var item in other)
                    {
                        conentry.Add(item);
                    }
                    connected.Remove(other);
                }
                conentry.Add(entry.Item);
                conentry.Add(entry.Other);
            }
            list.RemoveAt(0);
        }
        return $"{final2[0].X * final2[1].X}";
    }

    public long GetLengthSquared((long X, long Y, long Z) point1, (long X, long Y, long Z) point2)
    {
        return (Math.Abs(point1.X - point2.X) * Math.Abs(point1.X - point2.X))
             + (Math.Abs(point1.Y - point2.Y) * Math.Abs(point1.Y - point2.Y))
             + (Math.Abs(point1.Z - point2.Z) * Math.Abs(point1.Z - point2.Z));
    }
}
