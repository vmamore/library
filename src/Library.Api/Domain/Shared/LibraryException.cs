namespace Library.Api.Domain.Shared;

using System;

public abstract class LibraryException : Exception
{
    public LibraryException(string message) : base(message)
    {

    }
}
