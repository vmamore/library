namespace Library.Api.Books
{
    using System;
    using System.Collections.Generic;

    public class ReadModels
    {
        public class BookListItem
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
        }

        public class BookItem
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public IEnumerable<BookRentItem> Rents { get; set; }

            public class BookRentItem
            {
                public DateTime RentedDay { get; set; }
                public DateTime DayToReturn { get; set; }
                public string Condition { get; set; }
                public Guid PersonId { get; set; }

            }
        }
    }
}
