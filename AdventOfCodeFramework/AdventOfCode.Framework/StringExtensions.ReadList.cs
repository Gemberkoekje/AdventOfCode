using System.Text.RegularExpressions;

namespace AdventOfCodeFramework;

public static partial class StringExtensions
{
    public static string[] ReadList(this string input)
    {
        var list = input.Split("\r\n");
        return list;
    }

    public static char[][] ReadAndSplitList(this string input)
    {
        var list = input.ReadList();
        return list.Select(v => v.ToArray()).ToArray();
    }

    public static string[][] ReadList(this string input, string splitcharacter1, string splitcharacter2)
    {
        var list = input.Split(splitcharacter1);
        return list.Select(v => v.Split(splitcharacter2).ToArray()).ToArray();
    }

    public static string[][] ReadList(this string input, string splitcharacter)
    {
        var list = input.Split("\r\n");
        return list.Select(v => v.Split(splitcharacter).ToArray()).ToArray();
    }

    public static T[] ReadList<T>(this string input)
    {
        var list = input.ReadList();
        return list.Select(v2 => (T)Convert.ChangeType(v2, typeof(T))).ToArray();
    }

    public static T[][] ReadListWithMultipleWhiteSpaces<T>(this string input)
    {
        var list = input.ReadList(" ");
        return list.Select(v => v.Where(v2 => !string.IsNullOrWhiteSpace(v2)).Select(v2 => (T)Convert.ChangeType(v2, typeof(T))).ToArray()).ToArray();
    }

    public static T[][] ReadList<T>(this string input, string splitcharacter)
    {
        var list = input.ReadList(splitcharacter);
        return list.Select(v => v.Select(v2 => (T)Convert.ChangeType(v2, typeof(T))).ToArray()).ToArray();
    }

    public static IndexedListItem<T>[][] ReadAndSplitList<T>(this string input) where T : IComparable<T>
    {
        var list = input.ReadList().Select(v => v.Select(x => $"{x}")).ToArray();
        var index = 0;
        return list.Select(v => v.Select(v2 => new IndexedListItem<T>((T)Convert.ChangeType(v2, typeof(T)),index++)).ToArray()).ToArray();
    }

    public static Dictionary<(int X, int Y), T> ReadAndCoordList<T>(this string input) where T : IComparable<T>
    {
        var list = input.ReadList().Select(v => v.Select(x => $"{x}")).ToArray();
        var result = new Dictionary<(int X, int Y), T>();
        var x = 0;
        var y = 0;
        foreach(var line in list)
        {
            foreach(var item in line)
            {
                result.Add((x, y), (T)Convert.ChangeType(item, typeof(T)));
                x++;
            }
            y++;
        }
        return result;
    }

    public static Dictionary<(int X, int Y), int> ReadAndCoordList(this string input, string yesValue)
    {
        var list = input.ReadList().Select(v => v.Select(x => $"{x}")).ToArray();
        var result = new Dictionary<(int X, int Y), int>();
        var y = 0;
        foreach (var line in list)
        {
            var x = 0;
            foreach (var item in line)
            {
                result.Add((x, y), item == yesValue ? 1 : 0);
                x++;
            }
            y++;
        }
        return result;
    }


    public static T[] ReadListRegex<T>(this string input, string regex)
    {
        var matches = Regex.Matches(input, regex);
        return matches.Select(m => (T)Convert.ChangeType(m.Value, typeof(T))).ToArray();
    }

}

public record struct IndexedListItem<T>(T value, int index) : IComparable<IndexedListItem<T>> where T : IComparable<T>
{
    public int CompareTo(IndexedListItem<T> other)
    {
        return value.CompareTo(other.value);
    }
}