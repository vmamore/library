namespace Library.Api.Application.Librarians
{
    using System;
    using System.Threading.Tasks;
    using Library.Api.Application.Core;
    using Library.Api.Domain.Users;
    using static Library.Api.Application.Librarians.Commands;

    public class LibrarianApplicationService : IApplicationService
    {
        private readonly ILibrarianRepository _repository;

        public LibrarianApplicationService(ILibrarianRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(ICommand command) => command switch
        {
            V1.RegisterLibrarian cmd =>
                HandleCreate(cmd),
            _ => Task.CompletedTask
        };

        private async Task HandleCreate(V1.RegisterLibrarian cmd)
        {
            var newLibrarian = Librarian.Create(
                cmd.FirstName,
                cmd.LastName,
                cmd.BirthDate,
                cmd.CPF,
                cmd.City, cmd.District, cmd.Street, cmd.Number);

            await _repository.Add(newLibrarian);
        }

        private async Task HandleUpdate(
            Guid librarianId,
            Action<Librarian> operation
        )
        {
            var book = await _repository
                .Load(librarianId);

            if (book == null)
                throw new InvalidOperationException(
                    $"Entity with id {librarianId} cannot be found"
                );

            operation(book);
        }
    }
}
