using System.Text.RegularExpressions;
namespace PublishMarkdown.Markdown;

public class Scene
{
    public string Title { get; set; }
    public string Content { get; private set; }

    public Scene(string Content)
    {
        // TODO: Read title from YAML metadata using Markdig
        this.Title = "";
        this.Content = Content;
    }

    public void Format()
    {
        // TODO: Make this customizable
        Content = MarkdownRegex.FrontMatter().Replace(Content, "");
        Content = MarkdownRegex.Headers().Replace(Content, "");
        Content = SubStringReplace(Content, MarkdownRegex.ExternalLinks(), MarkdownRegex.AnchorText());
        Content = SubStringReplace(Content, MarkdownRegex.InternalLinks(), MarkdownRegex.AnchorText());
        Content = MarkdownRegex.Linebreaks().Replace(Content, "");
    }

    private static string SubStringReplace(string Input, Regex StringPattern, Regex SubPattern)
    {
        foreach (Match MatchedValue in StringPattern.Matches(Input))
        {
            string CleanText = SubPattern.Match(MatchedValue.Value).Value;
            Input = Input.Replace(MatchedValue.Value, CleanText);
        }
        return Input;
    }

    // Should this be in this class? More of a functionality test than anything
    public static bool TestOutput(string InputFile, string ValidateFile)
    {
        return File.ReadAllText(InputFile).SequenceEqual(File.ReadAllText(ValidateFile));
    }
}
