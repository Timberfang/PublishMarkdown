using System.Text.RegularExpressions;

namespace PublishMarkdown
{
    public partial class Program
    {
        static void Main()
        {
            Regex StripYAMLRegex = StripYAML();
            Regex StripHeadingRegex = StripHeading();
            Regex StripWikiLinksRegex = StripWikiLinks();
            Regex StripLinksRegex = StripLinks();
            Regex StripFooterRegex = StripFooter();

            StreamReader TextStream = new StreamReader(@".\test.txt");
            string StreamContents = TextStream.ReadToEnd();

            // Remove YAML frontmatter
            StreamContents = StripYAMLRegex.Replace(StreamContents, "");

            // Remove heading(s)
            StreamContents = StripHeadingRegex.Replace(StreamContents, "");

            // Remove wikilinks
            StreamContents = StripWikiLinksRegex.Replace(StreamContents, "");

            // Remove external links
            StreamContents = StripLinksRegex.Replace(StreamContents, "");

            // Remove footer
            StreamContents = StripFooterRegex.Replace(StreamContents, "");

            // Write output
            File.WriteAllText(@".\output.txt", StreamContents);
        }

        // Match YAML frontmatter
        [GeneratedRegex(@"^(---)([\s\S]*?)(---)(\s*)", RegexOptions.Multiline)]
        private static partial Regex StripYAML();

        // Match Markdown heading(s)
        [GeneratedRegex(@"^(#.*\s*)", RegexOptions.Multiline)]
        private static partial Regex StripHeading();

        // Match Obsidian [[wikilinks]]
        // TODO: Remove only double brackets, not text inside
        [GeneratedRegex(@"(\[\[)(.*?)(\]\])(\s*)")]
        private static partial Regex StripWikiLinks();

        // Match regular Markdown links
        // TODO: Match everything BUT anchor text
        [GeneratedRegex(@"(\[.*\])(\(.*?\))(\s*)")]
        private static partial Regex StripLinks();

        // Match my style of footer
        [GeneratedRegex(@"(\s*)(---)(\s*)(.*)")]
        private static partial Regex StripFooter();
    }
    
}