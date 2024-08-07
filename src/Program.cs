namespace PublishMarkdown
{
    public class Program
    {
        static void Main()
        {
            MarkdownCleaner MarkdownFile = new(@".\test-input.txt", @".\test-output.txt");
            MarkdownFile.CleanText();

            // If this fails, program should be considered non-functional
            if (MarkdownCleaner.TestOutput(@".\test-output.txt", @".\test-validate.txt")) { Console.WriteLine("Tests passed!"); }
            else { Console.WriteLine("Tests failed!"); }
        }
    }
}