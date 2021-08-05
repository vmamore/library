using System;
using Library.Api.Application.Core;

namespace Library.Api.Application.Locators
{
    public static class Commands
    {
        public static class V1
        {
            public class RegisterLocator : ICommand
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
