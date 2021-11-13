namespace Library.Api.Domain.Shared.ValueObjects
{
    using System;

    public class Email
    {
        public string Value { get; init; }

        public Email(string value)
        {
            Value = value;

            if (IsValid() == false)
                throw new ArgumentOutOfRangeException("Invalid value cannot be Email");
        }

        private bool IsValid() => !string.IsNullOrEmpty(Value);
    }
}
