namespace Library.Api.Domain.Users
{
    using System;

    public class Locator : User
    {
        public Guid Id { get; private set; }
    }
}
