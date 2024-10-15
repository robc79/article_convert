using CommandLine;

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

    public void Run()
    {
        var headline = FindHeadline(_options.Headline, _options.Source);

        if (headline is null)
        {
            throw new UnableToSniffHeadlineException("Headline not found in source file.");
        }

        Console.WriteLine(headline);
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

    private string? FindHeadline(string? headline, string sourceFile)
    {
        if (headline is not null)
        {
            return headline;
        }

        return _sniffer.SniffHeadline(sourceFile);
    }
}