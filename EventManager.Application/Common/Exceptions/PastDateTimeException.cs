namespace EventManager.Application.Common.Exceptions;

public class PastDateTimeException : Exception
{
    public PastDateTimeException() :
        base("The specified date or time is in the past") { }
}