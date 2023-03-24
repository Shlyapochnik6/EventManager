namespace EventManager.Application.Common.Exceptions;

public class ExistingUserException : Exception
{
    public ExistingUserException() : 
        base("User with this username or email already exists") { }
}