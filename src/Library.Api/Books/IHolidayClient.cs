namespace Library.Api.Books
{
    using System;
    using System.Threading.Tasks;

    public interface IHolidayClient
    {
        Task<DateTime> GetNextBusinessDate();
    }
}
