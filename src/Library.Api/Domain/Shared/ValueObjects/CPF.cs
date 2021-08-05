namespace Library.Api.Domain.Shared.ValueObjects
{
    using System;

    public class CPF
    {
        public string Value { get; init; }

        public CPF(string value)
        {
            Value = value;

            if (!Validate())
                throw new ArgumentOutOfRangeException("Invalid value cannot be CPF");
        }

        private bool Validate() => true;
    }
}
