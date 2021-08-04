namespace Library.Api.Domain.Users.ValueObjects
{
    using System;

    public class Address
    {
        public string Street { get; init; }
        public string City { get; init; }
        public string Number { get; init; }
        public string District { get; init; }

        public Address(string street, string city, string number, string district)
        {
            Street = street;
            City = city;
            Number = number;
            District = district;

            if (string.IsNullOrEmpty(Street) || string.IsNullOrEmpty(City) || string.IsNullOrEmpty(Number) || string.IsNullOrEmpty(District))
                throw new ArgumentNullException("Address cannot have street, city, number or district null");
        }
    }
}
