namespace EventManager.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name) :
        base($"{name} wasn't found") { }
}