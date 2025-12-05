using AdventOfCode2025;
using BenchmarkDotNet.Attributes;

namespace Benchmarks.AdventOfCode2025;

public class Benchmark2025
{
    public Benchmark2025()
    {
        Day1Input = ReadFromFile(typeof(Day1));
        Day2Input = ReadFromFile(typeof(Day2));
        Day3Input = ReadFromFile(typeof(Day3));
        Day4Input = ReadFromFile(typeof(Day4));
        Day5Input = ReadFromFile(typeof(Day5));
    }

    public Day1 Day1 = new Day1();
    public string Day1Input;

    //[Benchmark(Description = "2025-01-1")]
    public void Day1_1()
    {
        Day1.Solution1(Day1Input);
    }

    //[Benchmark(Description = "2025-01-2")]
    public void Day1_2()
    {
        Day1.Solution2(Day1Input);
    }

    public Day2 Day2 = new Day2();
    public string Day2Input;

    //[Benchmark(Description = "2025-02-1")]
    public void Day2_1()
    {
        Day2.Solution1(Day2Input);
    }

    //[Benchmark(Description = "2025-02-2")]
    public void Day2_2()
    {
        Day2.Solution2(Day2Input);
    }

    public Day3 Day3 = new Day3();
    public string Day3Input;

    //[Benchmark(Description = "2025-03-1")]
    public void Day3_1()
    {
        Day3.Solution1(Day3Input);
    }

    //[Benchmark(Description = "2025-03-2")]
    public void Day3_2()
    {
        Day3.Solution2(Day3Input);
    }

    public Day4 Day4 = new Day4();
    public string Day4Input;

    [Benchmark(Description = "2025-04-1")]
    public void Day4_1()
    {
        Day4.Solution1(Day4Input);
    }

    [Benchmark(Description = "2025-04-2")]
    public void Day4_2()
    {
        Day4.Solution2(Day4Input);
    }

    public Day5 Day5 = new Day5();
    public string Day5Input;

    [Benchmark(Description = "2025-05-1")]
    public void Day5_1()
    {
        Day5.Solution1(Day5Input);
    }

    [Benchmark(Description = "2025-05-2")]
    public void Day5_2()
    {
        Day5.Solution2(Day5Input);
    }

    public string ReadFromFile(Type @class)
    {
        var classname = @class.FullName;
        var filename = $"{classname}.txt";
        var assembly = @class.Assembly;
        var resourceName = filename;

        using (var stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
                throw new InvalidOperationException($"{filename} wasn't found. Does it exist as an embedded resource?");
            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
    }
}
