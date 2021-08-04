namespace Library.Api.Domain.Users
{
    using Library.Api.Domain.Core;
    using Library.Api.Domain.Users.ValueObjects;
    using static Library.Api.Domain.Users.Events.V1;

    public class User : AggregateRoot
    {
        public Name Name { get; private set; }
        public Age Age { get; private set; }
        public Address Address { get; private set; }
        public CPF Cpf { get; private set; }

        public override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case UserCreated e:
                    Name = Name.Create(e.FirstName, e.LastName);
                    Age = new(e.BirthDate);
                    Cpf = new(e.CPF);
                    Address = new(e.Street, e.City, e.Number, e.District);
                    break;
                default:
                    break;
            }
        }

        public override void EnsureValidState() {}
    }
}
