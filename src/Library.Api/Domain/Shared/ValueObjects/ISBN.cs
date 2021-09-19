namespace Library.Api.Domain.Shared.ValueObjects
{
    using System;

    public class ISBN
    {
        public string Value { get; init; }

        public ISBN(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Invalid value cannot be ISBN");

            Value = value;
        }
    }
}
