namespace PYP_Book.Application.Common.Exceptions;

public class LoginException : Exception
{
    public LoginException(string message) : base(message) { }
}