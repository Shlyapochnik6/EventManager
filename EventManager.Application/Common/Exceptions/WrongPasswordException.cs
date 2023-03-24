namespace EventManager.Application.Common.Exceptions;

public class WrongPasswordException : Exception
{
    public WrongPasswordException() :
        base($"The entered password isn't " +
             $"consistent with the user password") { }
}