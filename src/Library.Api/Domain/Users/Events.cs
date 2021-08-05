namespace Library.Api.Domain.Users
{
    using System;
    using Library.Api.Domain.Core;

    public static class Events
    {
        public static class V1
        {
            public class UserCreated : DomainEvent
            {
                public string Login { get; set; }
                public string Password { get; set; }
            }

            public class LibrarianCreated : DomainEvent
            {
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string CPF { get; set; }
                public DateTime BirthDate { get; set; }
                public string Street { get; set; }
                public string City { get; set; }
                public string Number { get; set; }
                public string District { get; set; }
            }

            public class LocatorCreated : DomainEvent
            {
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string CPF { get; set; }
                public DateTime BirthDate { get; set; }
                public string Street { get; set; }
                public string City { get; set; }
                public string Number { get; set; }
                public string District { get; set; }
            }
        }
    }
}
