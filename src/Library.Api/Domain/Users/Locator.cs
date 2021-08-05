namespace Library.Api.Domain.Users
{
    using System;
    using Library.Api.Domain.Core;
    using Library.Api.Domain.Shared.ValueObjects;
    using static Library.Api.Domain.Users.Events.V1;

    public class Locator : AggregateRoot
    {
        public Guid Id { get; private set; }

        public Name Name { get; private set; }
        public Age Age { get; private set; }
        public Address Address { get; private set; }
        public CPF Cpf { get; private set; }

        public override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case LocatorCreated e:
                    Name = Name.Create(e.FirstName, e.LastName);
                    Age = new(e.BirthDate);
                    Cpf = new(e.CPF);
                    Address = new(e.Street, e.City, e.Number, e.District);
                    break;
                default:
                    break;
            }
        }

        public override void EnsureValidState() { }
        public static Locator Create(
            string firstName, string lastName, DateTime birthDate,
            string cpf, string city, string district,
            string street, string number)
        {
            var locator = new Locator();

            locator.Apply(new LocatorCreated
            {
                FirstName = firstName,
                LastName = lastName,
                CPF = cpf,
                City = city,
                District = district,
                Street = street,
                Number = number
            });

            return locator;
        }
    }
}
