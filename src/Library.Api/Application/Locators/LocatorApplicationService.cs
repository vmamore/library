namespace Library.Api.Application.Locators
{
    using System;
    using System.Threading.Tasks;
    using Library.Api.Application.Core;
    using Library.Api.Domain.Core;
    using Library.Api.Domain.Users;
    using static Library.Api.Application.Locators.Commands;

    public class LocatorApplicationService : IApplicationService
    {
        private readonly ILocatorRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public LocatorApplicationService(ILocatorRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(ICommand command) => command switch
        {
            V1.RegisterLocator cmd =>
                HandleCreate(cmd),
            _ => Task.CompletedTask
        };

        private async Task HandleCreate(V1.RegisterLocator cmd)
        {
            var newLocator = Locator.Create(
                cmd.FirstName,
                cmd.LastName,
                cmd.BirthDate,
                cmd.CPF,
                cmd.City, cmd.District, cmd.Street, cmd.Number);

            await _repository.Add(newLocator);

            await _unitOfWork.Commit();
        }

        private async Task HandleUpdate(
            Guid locatorId,
            Action<Locator> operation
        )
        {
            var book = await _repository
                .Load(locatorId);

            if (book == null)
                throw new InvalidOperationException(
                    $"Entity with id {locatorId} cannot be found"
                );

            operation(book);

            await _unitOfWork.Commit();
        }
    }
}
