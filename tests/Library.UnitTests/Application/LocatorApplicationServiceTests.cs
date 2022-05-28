using FluentAssertions;
using Library.Api.Application.Locators;
using Library.Api.Application.Shared;
using Library.Api.Domain.BookRentals;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using static Library.Api.Application.Locators.Commands;

namespace Library.UnitTests.Application
{
    public class LocatorApplicationServiceTests
    {
        private readonly Mock<ILocatorRepository> repositoryMock;
        private readonly Mock<IAuthenticationClient> authenticationClientMock;

        public LocatorApplicationServiceTests()
        {
            repositoryMock = new();
            authenticationClientMock = new();
        }


        [Fact]
        public void Given_CreatingLocator_When_AuthenticationServiceReturnsUnsuccessfulStatusCode_ThenShouldThrowException()
        {
            var registerLocatorCommand = CreateRegistrationCommand();

            authenticationClientMock.Setup(a => a.CreateLocator(It.IsAny<Locator>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));

            Func<Task> action = async () => await GetSut().Handle(registerLocatorCommand);

            action.Should().ThrowExactlyAsync<InvalidOperationException>()
                .WithMessage("Error when creating user in authentication server.");
        }

        [Fact]
        public void Given_CreatingLocator_When_AuthenticationServiceReturnsSuccessfulStatusCode_ThenShouldNotThrowException()
        {
            var registerLocatorCommand = CreateRegistrationCommand();

            authenticationClientMock.Setup(a => a.CreateLocator(It.IsAny<Locator>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            Func<Task> action = async () => await GetSut().Handle(registerLocatorCommand);

            action.Should().NotThrowAsync();
            repositoryMock.Verify(r => r.Add(It.IsAny<Locator>()), Times.Once);
            repositoryMock.Verify(r => r.Commit(), Times.Once);

        }

        private V1.RegisterLocator CreateRegistrationCommand() => new V1.RegisterLocator
        {
            FirstName = "Vinicius",
            LastName = "Mamoré",
            CPF = "00011122233",
            BirthDate = new DateTime(1997, 10, 25),
            Street = "Avenida Afonso Pena",
            City = "Campo Grande",
            Number = "12",
            District = "Tiradentes"
        };

        private LocatorApplicationService GetSut() => new LocatorApplicationService(repositoryMock.Object, authenticationClientMock.Object);
    }
}
