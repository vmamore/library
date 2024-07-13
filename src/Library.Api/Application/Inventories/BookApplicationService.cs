namespace Library.Api.Application.Inventories
{
    using System.Threading.Tasks;
    using Domain.Inventory;
    using Shared;
    using static Commands;

    public class BookApplicationService(IBookRepository bookRepository, IDispatcher dispatcher) : IApplicationService
    {
        public Task Handle(ICommand command) => command switch
        {
            V1.RegisterBook cmd => this.HandleCreate(cmd),
            _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
        };

        private async Task HandleCreate(V1.RegisterBook command)
        {
            var newBook = Book.Create(command.Title, command.Author, command.ReleasedYear, command.ISBN, command.Pages, command.Version, command.PhotoUrl);

            await bookRepository.Add(newBook);

            var events = newBook.GetChanges();

            await bookRepository.Commit();

            await dispatcher.PublishAsync(events);
        }
    }
}
