namespace Library.Api.Domain.Users.ValueObjects
{
    using System;

    public class Age
    {
        public DateTime BirthDate { get; init; }

        public Age(DateTime birthDate)
        {
            BirthDate = birthDate;

            if (this.BirthDate > DateTime.UtcNow)
                throw new ArgumentOutOfRangeException("Birthdate cannot be bigger than today.");
        }

        // Fix: improve calculation
        public int Get() => DateTime.Now.Year - BirthDate.Year;
    }
}
