namespace Library.Api.Domain.Users
{
    using System;
    using Library.Api.Domain.Core;
    using Library.Api.Domain.Shared.ValueObjects;
    using static Library.Api.Domain.Users.Events.V1;

    public class Librarian : AggregateRoot
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
                case LibrarianCreated e:
                    Name = Name.Create(e.FirstName, e.LastName);
                    Age = new(e.BirthDate);
                    Cpf = new(e.CPF);
                    Address = new(e.Street, e.City, e.Number, e.District);
                    break;
                default:
                    break;
            }
        }

        public override void EnsureValidState() => throw new NotImplementedException();
    }
}
