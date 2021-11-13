namespace Library.Api.Application.Locators
{
    using System;
    using System.Threading.Tasks;
    using Library.Api.Application.Core;
    using Library.Api.Application.Shared;
    using Library.Api.Domain.BookRentals;
    using static Library.Api.Application.Locators.Commands;

    public class LocatorApplicationService : IApplicationService
    {
        private readonly ILocatorRepository repository;
        private readonly IAuthenticationClient authenticationClient;

        public LocatorApplicationService(ILocatorRepository repository, IAuthenticationClient authenticationClient)
        {
            this.repository = repository;
            this.authenticationClient = authenticationClient;
        }

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
                cmd.Email,
                cmd.City, cmd.District, cmd.Street, cmd.Number);

            var response = await this.authenticationClient.CreateLocator(locator);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Error when creating user in authentication server.");
            }

            await this.repository.Add(locator);

            await this.repository.Commit();
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
