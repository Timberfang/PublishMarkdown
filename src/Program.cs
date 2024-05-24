namespace PublishMarkdown
{
    public class Program
    {
        static void Main()
        {
            MarkdownCleaner MarkdownFile = new(@".\test-input.txt");

            MarkdownFile.CleanText();
            if (MarkdownCleaner.TestOutput(@".\test-output.txt", @".\test-validate.txt"))
            {
                Console.WriteLine("Tests passed!");
            }
            else
            {
                Console.WriteLine("Tests failed!");
            }
        }
    }
}