namespace Library.Api.Infrastructure.BookRentals
{
    using System;
    using System.Collections.Generic;

    public class ReadModels
    {
        public class GetAllBooksPaginated
        {
            public IEnumerable<BookListItem> Books { get; set; }
            public int CurrentPage { get; set; }
            public long TotalCount { get; set; }
            public long TotalPages => (int)Math.Ceiling((TotalCount / (decimal)10));

            public class BookListItem
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
                public string Author { get; set; }
                public string PhotoUrl { get; set; }
                public string Status { get; set; }
            }
        }


        public class BookRental
        {
            public Guid Id { get; set; }
            public DateTime RentedDay { get; set; }
            public DateTime DayToReturn { get; set; }
            public DateTime? ReturnedDay { get; set; }
            public string Status { get; set; }
            public string LibrarianName { get; set; }
            public ICollection<BookRented> Books { get; set; }
            public class BookRented
            {
                public string Title { get; set; }
                public string Author { get; set; }
            }
        }
    }
}
