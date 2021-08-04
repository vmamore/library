namespace Library.Api.Application.Books
{
    using System;
    using System.Threading.Tasks;
    using Library.Api.Application.Core;
    using Library.Api.Application.Shared;
    using Library.Api.Domain.Books;
    using Library.Api.Domain.Core;
    using static Library.Api.Application.Books.Commands;

    public class BookApplicationService : IApplicationService
    {
        private readonly IBookRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHolidayClient _holidayClient;

        public BookApplicationService(IBookRepository repository, IUnitOfWork unitOfWork, IHolidayClient holidayClient)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _holidayClient = holidayClient;
        }

        public Task Handle(ICommand command) => command switch
        {
            V1.RegisterBook cmd =>
                HandleCreate(cmd),
            V1.RentBook cmd =>
                HandleUpdate(cmd.BookId, async c => await c.Rent(cmd.PersonId, cmd.LibrarianId, _holidayClient.GetNextBusinessDate)),
            V1.ReturnBook cmd =>
                HandleUpdate(cmd.BookId, c => c.Returned(cmd.LibrarianId, cmd.Condition)),
            _ => Task.CompletedTask
        };

        private async Task HandleCreate(V1.RegisterBook command)
        {
            var newBook = Book.Create(command.Title, command.Author, command.ReleasedYear, command.Pages, command.Version);

            await _repository.Add(newBook);

            await _unitOfWork.Commit();
        }

        private async Task HandleUpdate(
            Guid bookId,
            Action<Book> operation
        )
        {
            var book = await _repository
                .Load(bookId);

            if (book == null)
                throw new InvalidOperationException(
                    $"Entity with id {bookId} cannot be found"
                );

            operation(book);

            await _unitOfWork.Commit();
        }
    }
}
