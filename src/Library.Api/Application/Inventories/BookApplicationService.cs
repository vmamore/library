namespace Library.Api.Application.Inventories
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Domain.Inventory;
    using Infrastructure.Integrations;
    using static Commands;

    public class BookApplicationService : IApplicationService
    {
        private readonly IBookRepository _repository;
        private readonly IIntegrationEventsMapper _integrationEventsMapper;
        private readonly BackgroundWorkerQueue _backgroundWorkerQueue;

        public BookApplicationService(IBookRepository repository, IIntegrationEventsMapper integrationEventsMapper, BackgroundWorkerQueue backgroundWorkerQueue)
        {
            this._repository = repository;
            this._integrationEventsMapper = integrationEventsMapper;
            this._backgroundWorkerQueue = backgroundWorkerQueue;
        }

        public Task Handle(ICommand command) => command switch
        {
            V1.RegisterBook cmd => this.HandleCreate(cmd),
            _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
        };

        private async Task HandleCreate(V1.RegisterBook command)
        {
            var newBook = Book.Create(command.Title, command.Author, command.ReleasedYear, command.ISBN, command.Pages, command.Version, command.PhotoUrl);

            await this._repository.Add(newBook);

            this._backgroundWorkerQueue.QueueBackgroundWorkItem(async token =>
            {
                await this._integrationEventsMapper.Handle(newBook.GetChanges());
            });

            await this._repository.Commit();
        }
    }
}
