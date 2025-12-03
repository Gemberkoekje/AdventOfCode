using System.Reflection;
using Xunit.Sdk;

namespace AdventOfCodeFramework;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class FileDataAttribute : DataAttribute
{
    private string Input { get; init; }

    private string Answer { get; init; }

    public FileDataAttribute(Type @class, string answer)
    {
        var classname = @class.FullName;
        var filename = $"{classname}.txt";
        var assembly = @class.Assembly;
        var resourceName = filename;

        using (var stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
                throw new InvalidOperationException($"{filename} wasn't found. Does it exist as an embedded resource?");
            using StreamReader reader = new (stream);
            Input = reader.ReadToEnd();
        }
        Answer = answer;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        return [[Input, Answer]];
    }
}
