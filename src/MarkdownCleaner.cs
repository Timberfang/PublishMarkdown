using System.Text.RegularExpressions;

namespace PublishMarkdown
{
    public partial class MarkdownCleaner(string InputFile)
    {
        public string InputFile { get; set; } = InputFile;

        public void CleanText()
        {
            // Read input file
            string StreamContents = File.ReadAllText(InputFile);

            // Remove YAML frontmatter, headers, internal & extenral links, and footers
            // TODO: Can this be optimized using a StringBuilder?
            StreamContents = MatchYAML().Replace(StreamContents, "");
            StreamContents = MatchHeader().Replace(StreamContents, "");
            StreamContents = SubStringReplace(StreamContents, MatchExternalLinks(), AnchorText());
            StreamContents = SubStringReplace(StreamContents, MatchInternalLinks(), AnchorText());
            StreamContents = MatchFooter().Replace(StreamContents, "");
            StreamContents = Linebreaks().Replace(StreamContents, "");

            // Write output
            File.WriteAllText(@".\test-output.txt", StreamContents);
        }

        public static bool TestOutput(string InputFile, string ValidateFile)
        {
            StreamReader InputStream = new StreamReader(InputFile);
            StreamReader ValidateStream = new StreamReader(ValidateFile);

            if (InputStream.ReadToEnd().SequenceEqual(ValidateStream.ReadToEnd())) { return true; }
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


        // Markdown Regex - Regex is complicated & hard to read, so it's explained in more detail
        // Match YAML frontmatter
        /* Pattern:
            Group 1: Text begins with "---"
            Group 2: Match 0 or more characters, matching as few as possible
            Group 3: Text ends with "---" */
        [GeneratedRegex(@"^(---)(.*?)(---)", RegexOptions.Singleline)]
        private static partial Regex MatchYAML();

        // Match Markdown header(s)
        // Pattern: Line begins with '#' character followed by 0 or more characters, matching as few as possible, until line-end
        [GeneratedRegex(@"^(#.*?)$", RegexOptions.Multiline)]
        private static partial Regex MatchHeader();

        // Match  linebreaks
        /* Pattern: Match one or more carriage return and/or line-feed characters
            Group 1: Match one or more carriage return and/or line-feed characters at the *start* of the string
            Group 1: Match one or more carriage return and/or line-feed characters at the *end* of the string */
        [GeneratedRegex(@"^([\r\n]+)|([\r\n]+)$")]
        private static partial Regex Linebreaks();

        // Match link anchor text
        /* Pattern:
            Group 1: Text *preceded* by '[' character, but not matching '['
            Group 2: Match 0 or more characters, matching as few as possible, *excluding* '[' and ']'
            Group 3: Text *followed* by ']' character, but not matching ']' */
        [GeneratedRegex(@"(?<=\[)([^\[\]]*?)(?=\])")]
        private static partial Regex AnchorText();

        // Match external links
        /* Pattern:
            Group 1: Text begins with '[' character, followed by 0 or more characters *excluding* '[' and ']', matching as few as possible, followed by ']' character
            Group 2: Match '(' character, followed by 0 or more characters, matching as few as possible, followed by ')' character */
        [GeneratedRegex(@"(\[[^\[\]]*?\])(\(.*?\))")]
        private static partial Regex MatchExternalLinks();

        // Match internal links
        // Pattern: Text begins with two '[' characters, followed by 0 or more character, matching as few as possible, until two ']' characters
        [GeneratedRegex(@"\[\[.*?\]\]")]
        private static partial Regex MatchInternalLinks();

        // Match my style of footer
        /* Pattern:
            Group 1: Text begins with "---"
            Group 2: Match 0 or more whitespace characters
            Group 3: Match 0 or more characters, until line-end */
        // Not sure why group 2 is necessary, but the footer's not detected otherwise.
        [GeneratedRegex(@"(---)(\s*)(.*)$")]
        private static partial Regex MatchFooter();
    }
}