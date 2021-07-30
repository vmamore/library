namespace Library.Api.Books
{
    using System;

    public class ReadModels
    {
        public class BookListItem
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
        }
    }
}
