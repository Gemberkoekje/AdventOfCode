namespace AdventOfCodeFramework;

public static partial class StringExtensions
{
    public static string[] ReadList(this string input)
    {
        var list = input.Split("\r\n");
        return list;
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
}

public record struct IndexedListItem<T>(T value, int index) : IComparable<IndexedListItem<T>> where T : IComparable<T>
{
    public int CompareTo(IndexedListItem<T> other)
    {
        return value.CompareTo(other.value);
    }
}