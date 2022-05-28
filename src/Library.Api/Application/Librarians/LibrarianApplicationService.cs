namespace Library.Api.Application.Librarians
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Domain.Users;
    using static Commands;

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

            await _repository.Commit();
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
