namespace Library.Api.Application.Librarians
{
    using System;
    using Library.Api.Application.Core;

    public static class Commands
    {
        public static class V1
        {
            public class RegisterLibrarian : ICommand
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
