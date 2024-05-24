using System.Text;

namespace PublishMarkdown
{
    public class MarkdownPublisher(string InputPath)
    {
        public string InputPath {get; set;} = InputPath;

        public string Concat()
        {
            DirectoryInfo SearchDir = new(InputPath);
            List<FileInfo> FileList = SearchDir.GetFiles("*.md").ToList();
            FileList.Sort();

            StringBuilder OutFile = new();

            foreach (FileInfo file in FileList)
            {
                OutFile.Append(file);
            }

            return OutFile.ToString();
        }
    }
}