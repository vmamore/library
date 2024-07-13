namespace Library.Api.Application.Librarians
{
    using System.Threading.Tasks;
    using Domain.Users;
    using Shared;
    using static Commands;

    public class LibrarianApplicationService(ILibrarianRepository repository) : IApplicationService
    {
        public Task Handle(ICommand command) => command switch
        {
            V1.RegisterLibrarian cmd => this.HandleCreate(cmd),
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

            await repository.Add(newLibrarian);

            await repository.Commit();
        }

        private async Task HandleUpdate(
            Guid librarianId,
            Action<Librarian> operation
        )
        {
            var book = await repository
                .Load(librarianId);

            if (book == null)
                throw new InvalidOperationException(
                    $"Entity with id {librarianId} cannot be found"
                );

            operation(book);
        }
    }
}
