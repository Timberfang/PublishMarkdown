using System.Text.RegularExpressions;

namespace PublishMarkdown
{
    public partial class MarkdownCleaner
    {
        public string InputFile { get; set; }
        public string OutputFile { get; set; }

        public MarkdownCleaner(string InputFile, string OutputFile)
        {
            this.InputFile = InputFile;
            this.OutputFile = OutputFile;
        }

        public void CleanText()
        {
            // Read input file
            string StreamContents = File.ReadAllText(InputFile);

            // Remove YAML frontmatter, headers, internal & extenral links, and footers
            // Can this be optimized?
            StreamContents = MarkdownRegex.FrontMatter().Replace(StreamContents, "");
            StreamContents = MarkdownRegex.Headers().Replace(StreamContents, "");
            StreamContents = SubStringReplace(StreamContents, MarkdownRegex.ExternalLinks(), MarkdownRegex.AnchorText());
            StreamContents = SubStringReplace(StreamContents, MarkdownRegex.InternalLinks(), MarkdownRegex.AnchorText());
            StreamContents = MarkdownRegex.Footer().Replace(StreamContents, "");
            StreamContents = MarkdownRegex.Linebreaks().Replace(StreamContents, "");

            // Write output
            File.WriteAllText(OutputFile, StreamContents);
        }

        // Should this be in this class? More of a functionality test than anything
        public static bool TestOutput(string InputFile, string ValidateFile)
        {
            if (File.ReadAllText(InputFile).SequenceEqual(File.ReadAllText(ValidateFile))) { return true; }
            else { return false; }
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
    }
}