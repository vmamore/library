namespace Library.Api.Application.Shared
{
    using System;
    using System.Threading.Tasks;

    public interface IHolidayClient
    {
        Task<DateTime> GetNextBusinessDate();
    }
}
