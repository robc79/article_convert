using CommandLine;

namespace ArticleConvert;

public class Program
{
    public static void Main(string[] args)
    {
        var parsedOptions = Parser.Default.ParseArguments<CommandLineOptions>(args);
        
        if (parsedOptions.Value is null)
        {
            Environment.Exit(-1);
        }

        var options = parsedOptions.Value!;
    }
}