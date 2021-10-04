namespace Library.Api.Application.Inventories
{
    using System.Threading.Tasks;
    using Library.Api.Application.Core;
    using Library.Api.Domain.Inventory;
    using Library.Api.Infrastructure.Integrations;
    using static Library.Api.Application.Inventories.Commands;

    public class BookApplicationService : IApplicationService
    {
        private readonly IBookRepository _repository;
        private readonly IIntegrationEventsMapper _integrationEventsMapper;
        private readonly BackgroundWorkerQueue _backgroundWorkerQueue;

        public BookApplicationService(IBookRepository repository, IIntegrationEventsMapper integrationEventsMapper, BackgroundWorkerQueue backgroundWorkerQueue)
        {
            _repository = repository;
            _integrationEventsMapper = integrationEventsMapper;
            _backgroundWorkerQueue = backgroundWorkerQueue;
        }

        public Task Handle(ICommand command) => command switch
        {
            V1.RegisterBook cmd => HandleCreate(cmd)
        };

        private async Task HandleCreate(V1.RegisterBook command)
        {
            var newBook = Book.Create(command.Title, command.Author, command.ReleasedYear, command.ISBN, command.Pages, command.Version, command.PhotoUrl);

            await _repository.Add(newBook);

            _backgroundWorkerQueue.QueueBackgroundWorkItem(async token =>
            {
                await _integrationEventsMapper.Handle(newBook.GetChanges());
            });

            await _repository.Commit();
        }
    }
}
