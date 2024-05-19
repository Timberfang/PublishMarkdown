# PublishMarkdown

This is a (very WIP) attempt at creating a standalone tool for formatting my writing. I (currently) use Obsidian with the [Longform](https://github.com/kevboh/longform) plugin for my writing. As a set of Markdown files, written scene-by-scene, each has their own formatting (e.g., YAML frontmatter, header, footer, links, etc.). This program will extract the body of the text in each file, combine the text into a single manuscript file, and add final formatting to create a full version of the work. This can then be converted using tools such as Pandoc into publish-ready formats.

## Usage

For now, this will process files in alphabetical order. I use the naming scheme `{ChapterNumber}-{SceneNumber}-{SceneName}` for each file. If it marks a new chapter, it will be marked as "scene 0", like so: `{ChapterNumber}-{00}-{ChapterName}`. In the future, I'd like to extract this information from the YAML frontmatter, but that's not implemented yet.