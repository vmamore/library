namespace Library.Api.Domain.BookRentals
{
    using System;
    using System.Threading.Tasks;

    public interface ILocatorRepository
    {
        Task<Locator> Load(Guid id);

        ValueTask Add(Locator entity);

        ValueTask<bool> Exists(Guid id);
    }
}
