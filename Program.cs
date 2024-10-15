using CommandLine;
using Markdig;
using Markdig.Prism;

namespace ArticleConvert;

public class Program
{
    private readonly CommandLineOptions _options;
    private readonly HeadlineSniffer _sniffer;

    public Program(
        CommandLineOptions options,
        HeadlineSniffer sniffer)
    {
        _options = options;
        _sniffer = sniffer;
    }

    public static void Main(string[] args)
    {
        var parsedOptions = Parser.Default.ParseArguments<CommandLineOptions>(args);
        
        if (parsedOptions.Value is null)
        {
            Environment.Exit(-1);
        }

        var program = new Program(
            parsedOptions.Value,
            new HeadlineSniffer());

        program.Run();
    }

    public void Run()
    {
        var headline = _options.Headline ?? _sniffer.SniffHeadline(_options.Source);

        if (headline is null)
        {
            throw new UnableToSniffHeadlineException("Headline not found in source file.");
        }

        if (_options.Headline is null)
        {
            Console.WriteLine($"Found headline: {headline}");
        }

        var markdown = LoadFromFile(_options.Source);

        var mdPipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UsePrism()
            .Build();
        
        var article = Markdown.ToHtml(markdown, mdPipeline);
        var html = LoadFromFile(_options.TemplateName);
        html = html.Replace("{{HEADLINE}}", headline);
        html = html.Replace("{{DATELINE}}", _options.DateLine);
        html = html.Replace("{{ARTICLE}}", article);
        WriteToFile(_options.Destination, html);
    }

    private string LoadFromFile(string filename)
    {
        string markdown;

        using (var file = File.Open(filename, FileMode.Open, FileAccess.Read))
        using (var reader = new StreamReader(file))
        {
            markdown = reader.ReadToEnd();
        }

        return markdown;
    }

    private void WriteToFile(string filename, string text)
    {
        using (var file = File.Open(filename, FileMode.CreateNew, FileAccess.Write))
        using (var writer = new StreamWriter(file))
        {
            writer.WriteLine(text);
        }
    }
}