using Library.Api.Domain.BookRentals;

namespace Library.Tests.UnitTests.Domain.BookRentals
{
    public class BookReturnDateTests
    {
        [Fact]
        public void Should_Create_With_Success()
        {
            var tomorrow = DateTime.Now.AddDays(1);
            var action = () => BookReturnDate.Create(tomorrow);
            action.Should().NotThrow();
        }

        [Fact]
        public void Should_Throw_Exception()
        {
            var tomorrow = DateTime.Now.AddDays(-1);
            var action = () => BookReturnDate.Create(tomorrow);
            action.Should().ThrowExactly<ArgumentException>();
        }
    }
}
