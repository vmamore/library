namespace Library.Api.Domain.Users
{
    using System;
    using Shared.Core;

    public static class Events
    {
        public static class V1
        {
            public class LibrarianCreated : IDomainEvent
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
