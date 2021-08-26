namespace Library.Api.Domain.Users
{
    using Library.Api.Domain.Core;
    using static Library.Api.Domain.Users.Events.V1;

    public class User : AggregateRoot
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case UserCreated e:
                    Login = e.Login;
                    Password = e.Password;
                    break;
                default:
                    break;
            }
        }

        public override void EnsureValidState() { }
    }
}
