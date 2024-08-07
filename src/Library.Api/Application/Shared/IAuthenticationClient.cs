namespace Library.Api.Application.Shared;

using System.Net.Http;
using System.Threading.Tasks;
using Domain.BookRentals;

public interface IAuthenticationClient
{
    Task<HttpResponseMessage> CreateLocator(Locator locator, string email, string password, string username);
}
