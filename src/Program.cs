using System.Text.RegularExpressions;

namespace PublishMarkdown
{
    public partial class Program
    {
        static void Main()
        {
            Regex StripYAMLRegex = StripYAML();
            Regex StripHeadingsRegex = StripHeadings();
            Regex StripWikiLinksRegex = StripWikiLinks();
            Regex StripLinksRegex = StripLinks();
            Regex StripCopyrightRegex = StripCopyright();

            StreamReader TextStream = new StreamReader(@".\test.txt");
            string StreamContents = TextStream.ReadToEnd();

            // Remove YAML frontmatter
            StreamContents = StripYAMLRegex.Replace(StreamContents, "");

            // Remove headings
            StreamContents = StripHeadingsRegex.Replace(StreamContents, "");

            // Remove wikilinks
            StreamContents = StripWikiLinksRegex.Replace(StreamContents, "");

            // Remove external links
            StreamContents = StripLinksRegex.Replace(StreamContents, "");

            // Remove copyright notice
            StreamContents = StripCopyrightRegex.Replace(StreamContents, "");

            // Write output
            File.WriteAllText(@".\output.txt", StreamContents);
        }

        // Match YAML frontmatter
        [GeneratedRegex(@"^(---)([\s\S]*?)(---)(\s*)", RegexOptions.Multiline)]
        private static partial Regex StripYAML();

        // Match Markdown headings
        [GeneratedRegex(@"^(#.*\s*)", RegexOptions.Multiline)]
        private static partial Regex StripHeadings();

        // Match Obsidian [[wikilinks]]
        // TODO: Remove only double brackets, not text inside
        [GeneratedRegex(@"(\[\[)(.*?)(\]\])(\s*)")]
        private static partial Regex StripWikiLinks();

        // Match regular Markdown links
        // TODO: Match everything BUT anchor text
        [GeneratedRegex(@"(\[.*\])(\(.*?\))(\s*)")]
        private static partial Regex StripLinks();

        // Match my copyright notice
        [GeneratedRegex(@"(\s*)(---)(\s*)(.*)")]
        private static partial Regex StripCopyright();
    }
    
}