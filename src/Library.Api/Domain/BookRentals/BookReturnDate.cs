namespace Library.Api.Domain.BookRentals
{
    using System;

    public class BookReturnDate
    {
        public DateTime Value { get; private set; }

        public BookReturnDate(DateTime value) => this.Value = value;

        public static BookReturnDate Create(DateTime value)
        {
            if (value.Date < DateTime.UtcNow.Date)
                throw new ArgumentException("Value cannot be on the past");

            return new BookReturnDate(value);
        }

        public static implicit operator DateTime(BookReturnDate returnDate) => returnDate.Value;
    }
}
