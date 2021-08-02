namespace Library.Api.Infrastructure.Clients
{
    using System;
    using System.Threading.Tasks;
    using Library.Api.Books;

    public class HolidayClient : IHolidayClient
    {
        public Task<DateTime> GetNextBusinessDate() => Task.FromResult(DateTime.UtcNow.AddDays(3));
    }
}
