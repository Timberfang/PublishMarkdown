using System.Text.RegularExpressions;

namespace PublishMarkdown;

internal static class Program
{
	private static void Main(string[] args)
	{
		string testString = File.ReadAllText(@".\TestInput.md");
		Console.WriteLine(MarkdownRegex.FilterString(testString));
	}
}
