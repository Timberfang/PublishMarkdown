using PublishMarkdown.Markdown;

namespace PublishMarkdown
{
    public class Program
    {
        static void Main()
        {
            string Input = File.ReadAllText(@".\test-input.txt");
            Scene MarkdownFile = new(Input);
            MarkdownFile.Format();
            File.WriteAllText(@".\test-output.txt", MarkdownFile.Content);

            // If this fails, program should be considered non-functional
            if (Scene.TestOutput(@".\test-output.txt", @".\test-validate.txt")) { Console.WriteLine("Tests passed!"); }
            else { Console.WriteLine("Tests failed!"); }
        }
    }
}