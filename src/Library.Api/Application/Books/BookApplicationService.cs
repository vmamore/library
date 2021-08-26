namespace Library.Api.Application.Books
{
    using System.Threading.Tasks;
    using Library.Api.Application.Core;
    using Library.Api.Domain.Inventory;
    using static Library.Api.Application.Books.Commands;

    public class BookApplicationService : IApplicationService
    {
        private readonly IBookRepository _repository;

        public BookApplicationService(IBookRepository repository) => _repository = repository;

        public Task Handle(ICommand command) => command switch
        {
            V1.RegisterBook cmd => HandleCreate(cmd)
        };

        private async Task HandleCreate(V1.RegisterBook command)
        {
            var newBook = Book.Create(command.Title, command.Author, command.ReleasedYear, command.Pages, command.Version);

            await _repository.Add(newBook);
        }
    }
}
