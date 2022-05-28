namespace Library.Api.Domain.BookRentals.Exceptions;

using System;
using Shared;

public class LocatorHasActiveRental : LibraryException
{
    public LocatorHasActiveRental(DateTime rentalDate) : base($"Locator has an active rental already. Rental date: {rentalDate}")
    {
    }
}
