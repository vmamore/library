namespace Library.Api.Books
{
    using System;
    using Library.Api.Core;

    public static class Commands
    {
        public static class V1
        {
            public class RegisterBook : ICommand
            {
                public string Title { get; set; }
                public string Author { get; set; }
                public string ReleasedYear { get; set; }
                public int Pages { get; set; }
                public int Version { get; set; }
            }

        }
    }
}
