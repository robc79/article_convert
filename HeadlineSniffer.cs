namespace ArticleConvert;

public class HeadlineSniffer
{
    public string? SniffHeadline(string sourceFile)
    {
        string? headline = null;

        using (var file = File.Open(sourceFile, FileMode.Open, FileAccess.Read))
        using (var reader = new StreamReader(file))
        {
            var line = reader.ReadLine();
            
            while (line != null)
            {
                if (!line.StartsWith("# "))
                {
                    line = reader.ReadLine();
                }
                else
                {
                    headline = line.Split("# ")[1];
                    break;
                }
            }
        }

        return headline;
    }
}