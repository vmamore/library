namespace Library.Api.Domain.Users.ValueObjects
{
    using System;

    public class Name
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        private Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static Name Create(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException("Firstname or Lastname cannot be empty");

            return new(firstName, lastName);
        }
    }
}
