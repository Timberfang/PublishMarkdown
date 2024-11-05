using System.Text.RegularExpressions;

namespace PublishMarkdown;

internal static partial class MarkdownRegex
{
	internal static string FilterString(string input)
	{
		// Is there a more performant way of handling this?
		input = FrontMatterRegex().Replace(input, string.Empty);
		input = HeaderRegex().Replace(input, string.Empty);
		input = HyperlinkRegex().Replace(input, @"$1");
		input = WikiLinkRegex().Replace(input, @"$1");
		return input;
	}

	// YAML Frontmatter: From start of file, match three '-' characters, one or more new lines,
	// zero or more of any character, another set of three '-' characters, then one or more new lines.
	[GeneratedRegex(@"^---\s+[\S\s]*?---\s+", RegexOptions.Compiled)]
	private static partial Regex FrontMatterRegex();

	// Headers: From start of line, match one or more '#' characters, a space, zero or more of any character, then a new line.
	[GeneratedRegex(@"^#+ \S*\s*", RegexOptions.Compiled | RegexOptions.Multiline)]
	private static partial Regex HeaderRegex();

	// Links: Match exactly one '[' character and exactly one ']' character, followed by a '(' character, zero or more other characters, then a ')' character.
	// NOTE: Match *only* group 1.
	[GeneratedRegex(@"\[(.*?)\]\(.*?\)", RegexOptions.Compiled)]
	private static partial Regex HyperlinkRegex();

	// Obsidian Wikilinks: Match exactly two '[' characters and exactly two ']' characters.
	// NOTE: Match *only* group 1.
	[GeneratedRegex(@"\[{2}(.*?)\]{2}", RegexOptions.Compiled)]
	private static partial Regex WikiLinkRegex();
}
