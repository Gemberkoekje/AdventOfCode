namespace AdventOfCodeFramework;

public static partial class StringExtensions
{
    public static string[][] ReadVerticalList(this string input, string splitcharacter)
    {
        var list = input.ReadList(splitcharacter);
        var result = new List<List<string>>();
        foreach (var value in list)
        {
            int currentcount = 0;
            foreach (var value2 in value)
            {
                // if it's the xth value, add it to the xth list
                if (result.Count < (currentcount + 1))
                {
                    result.Add([]);
                }
                result[currentcount].Add(value2);
                currentcount++;
            }
        }
        return result.Select(v => v.ToArray()).ToArray();
    }

    public static T[][] ReadVerticalList<T>(this string input, string splitcharacter)
    {
        var list = input.ReadVerticalList(splitcharacter);
        return list.Select(v => v.Select(v2 => (T)Convert.ChangeType(v2, typeof(T))).ToArray()).ToArray();
    }
}
