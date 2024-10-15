namespace ArticleConvert;

public class UnableToSniffHeadlineException : Exception
{
    public UnableToSniffHeadlineException(string? message) : base(message)
    {
    }
}