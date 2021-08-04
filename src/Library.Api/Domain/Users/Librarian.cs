namespace Library.Api.Domain.Users
{
    using System;

    public class Librarian : User
    {
        public Guid Id { get; private set; }
    }
}
