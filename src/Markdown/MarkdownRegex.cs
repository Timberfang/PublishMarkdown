using System.Text.RegularExpressions;
namespace PublishMarkdown.Markdown;

public static partial class MarkdownRegex
{
    [GeneratedRegex(@"^(---)(.*?)(---)", RegexOptions.Singleline)]
    public static partial Regex FrontMatter();
    [GeneratedRegex(@"^#+\s.+$", RegexOptions.Multiline)]
    public static partial Regex Headers();
    [GeneratedRegex(@"^\s+|\s+$")]
    public static partial Regex Linebreaks();
    [GeneratedRegex(@"(?<=\[)([^\[\]]*?)(?=\])")]
    public static partial Regex AnchorText();
    [GeneratedRegex(@"(\[[^\[\]]*?\])(\(.*?\))")]
    public static partial Regex ExternalLinks();
    [GeneratedRegex(@"\[\[.*?\]\]")]
    public static partial Regex InternalLinks();
}
