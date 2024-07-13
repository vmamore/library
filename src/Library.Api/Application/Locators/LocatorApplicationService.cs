namespace Library.Api.Application.Locators
{
    using System;
    using System.Threading.Tasks;
    using Shared;
    using Domain.BookRentals;
    using static Commands;

    public class LocatorApplicationService(ILocatorRepository repository, IAuthenticationClient authenticationClient)
        : IApplicationService
    {
        public Task Handle(ICommand command) => command switch
        {
            V1.RegisterLocator cmd =>
                this.HandleCreate(cmd),
            _ => Task.CompletedTask
        };

        private async Task HandleCreate(V1.RegisterLocator cmd)
        {
            var locator = Locator.Create(
                cmd.FirstName,
                cmd.LastName,
                cmd.BirthDate,
                cmd.CPF,
                cmd.City, cmd.District, cmd.Street, cmd.Number);

            var response = await authenticationClient.CreateLocator(locator, cmd.Email, cmd.Password, cmd.Username);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Error when creating user in authentication server.");
            }

            await repository.Add(locator);

            await repository.Commit();
        }

        private async Task HandleUpdate(
            Guid locatorId,
            Action<Locator> operation
        )
        {
            var book = await repository
                .Load(locatorId);

            if (book == null)
                throw new InvalidOperationException(
                    $"Entity with id {locatorId} cannot be found"
                );

            operation(book);
        }
    }
}
