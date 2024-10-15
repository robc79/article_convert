using CommandLine;

namespace ArticleConvert;

public class CommandLineOptions
{
    [Option('h', "headline", HelpText = "Article headline.")]
    public string? Headline { get; set; }

    [Option('d', "dateline", Required = true, HelpText = "Article date line.")]
    public string DateLine { get; set; }

    [Option('t', "template", Required = true, HelpText = "HTML template to use in article generation.")]
    public string TemplateName { get; set; }

    [Option('i', "input", Required = true, HelpText = "Markdown file to read from.")]
    public string Source { get; set; }

    [Option('o', "output", Required = true, HelpText = "HTML file to write to.")]
    public string Destination { get; set; }
}