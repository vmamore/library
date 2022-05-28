namespace Library.Api.Domain.BookRentals.Exceptions;

using System;
using Shared;

public class LocatorIsPenalized : LibraryException
{
    public LocatorIsPenalized(DateTime dateToFinishPenalty) : base($"Locator is penalized until {dateToFinishPenalty}, he/she cannot rent any books.")
    {
    }
}
