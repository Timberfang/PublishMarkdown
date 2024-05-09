using System.Text.RegularExpressions;

namespace PublishMarkdown
{
    public partial class Program
    {
        static void Main()
        {
            CleanText(@".\test-input.txt");
            if (TestClean(@".\test-output.txt", @".\test-validate.txt"))
            {
                Console.WriteLine("Tests passed!");
            }
            else
            {
                Console.WriteLine("Tests failed!");
            }
        }

        static void CleanText(string FilePath)
        {
            Regex StripYAMLRegex = StripYAML();
            Regex StripHeadingRegex = StripHeading();
            Regex AnchorTextRegex = AnchorText();
            Regex StripLinksRegex = StripLinks();
            Regex StripWikiLinksRegex = StripWikiLinks();
            Regex StripFooterRegex = StripFooter();

            StreamReader TextStream = new StreamReader(FilePath);
            string StreamContents = TextStream.ReadToEnd();

            // Remove YAML frontmatter
            StreamContents = StripYAMLRegex.Replace(StreamContents, "");

            // Remove heading(s)
            StreamContents = StripHeadingRegex.Replace(StreamContents, "");

            // Remove Links
            StreamContents = SubStringReplace(StreamContents, StripLinksRegex, AnchorTextRegex);

            // Remove Wikilinks
            StreamContents = SubStringReplace(StreamContents, StripWikiLinksRegex, AnchorTextRegex);

            // Remove footer
            StreamContents = StripFooterRegex.Replace(StreamContents, "");

            // Write output
            File.WriteAllText(@".\test-output.txt", StreamContents);
        }

        static bool TestClean(string InputFile, string ValidateFile)
        {
            StreamReader InputStream = new StreamReader(InputFile);
            StreamReader ValidateStream = new StreamReader(ValidateFile);

            if (InputStream.ReadToEnd().SequenceEqual(ValidateStream.ReadToEnd()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        static string SubStringReplace(string Input, Regex StringPattern, Regex SubPattern)
        {
            foreach (Match MatchedValue in StringPattern.Matches(Input))
            {
                string CleanText = SubPattern.Match(MatchedValue.Value).Value;
                Input = Input.Replace(MatchedValue.Value, CleanText);
            }
            return Input;
        }

        // Match YAML frontmatter
        [GeneratedRegex(@"^(---)([\s\S]*?)(---)(\s*)", RegexOptions.Multiline)]
        private static partial Regex StripYAML();

        // Match Markdown heading(s)
        [GeneratedRegex(@"^(#.*\s*)", RegexOptions.Multiline)]
        private static partial Regex StripHeading();

        // Match link anchor text
        [GeneratedRegex(@"(?<=\[)([^\[\]]*?)(?=\])")]
        private static partial Regex AnchorText();

        // Match regular links
        [GeneratedRegex(@"(\[|\])|(\(.*?\))")]
        private static partial Regex StripLinks();

        // Match [[Wikilinks]]
        [GeneratedRegex(@"\[\[.*?\]\]")]
        private static partial Regex StripWikiLinks();

        // Match my style of footer
        [GeneratedRegex(@"(\s*)(---)(\s*)(.*)$")]
        private static partial Regex StripFooter();
    }
}