namespace Library.Api.Books
{
    using System;
    using System.Threading.Tasks;
    using Library.Api.Core;
    using static Library.Api.Books.Commands;

    public class BookApplicationService : IApplicationService
    {
        private readonly IBookRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public BookApplicationService(IBookRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(ICommand command) => command switch
        {
            V1.RegisterBook cmd =>
                HandleCreate(cmd),
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
