namespace Library.Api.Application.Inventories
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Domain.Inventory;
    using Shared;
    using static Commands;

    public class BookApplicationService : IApplicationService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IDispatcher _dispatcher;

        public BookApplicationService(IBookRepository bookRepository, IDispatcher dispatcher)
        {
            this._bookRepository = bookRepository;
            this._dispatcher = dispatcher;
        }

        public Task Handle(ICommand command) => command switch
        {
            V1.RegisterBook cmd => this.HandleCreate(cmd),
            _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
        };

        private async Task HandleCreate(V1.RegisterBook command)
        {
            var newBook = Book.Create(command.Title, command.Author, command.ReleasedYear, command.ISBN, command.Pages, command.Version, command.PhotoUrl);

            await this._bookRepository.Add(newBook);

            var events = newBook.GetChanges();

            await this._bookRepository.Commit();

            await this._dispatcher.PublishAsync(events);
        }
    }
}
