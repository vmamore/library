namespace Library.Api.Application.Locators
{
    using Shared;

    public static class Commands
    {
        public static class V1
        {
            public class RegisterLocator : ICommand
            {
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string CPF { get; set; }
                public string Username { get; set; }
                public string Email { get; set; }
                public string Password { get; set; }
                public DateTime BirthDate { get; set; }
                public string Street { get; set; }
                public string City { get; set; }
                public string Number { get; set; }
                public string District { get; set; }
            }
        }
    }
}
