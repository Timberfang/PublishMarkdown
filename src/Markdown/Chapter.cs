namespace PublishMarkdown.Markdown;

public class Chapter
{
    public string Title { get; set; }
    public IList<Scene> Scenes { get; private set; }

    public Chapter()
    {
        // Read title from folder name
        this.Title = "";
        this.Scenes = Scenes ?? [];
    }
}
