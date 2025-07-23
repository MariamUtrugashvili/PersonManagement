using Moq;
using PersonManagement.Application.Persons.Commands.CreatePerson;
using PersonManagement.Application.Exceptions;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;
using PersonManagement.Domain.Repositories;

namespace PersonManagement.Application.UnitTests.Persons.Commands
{
    public class CreatePersonCommandHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock = new();
        private readonly CreatePersonCommandHandler _handler;

        public CreatePersonCommandHandlerTests()
        {
            _handler = new CreatePersonCommandHandler(_personRepositoryMock.Object);
        }

        [Fact]
        public async Task ThrowsException_WhenPersonAlreadyExists()
        {
            // Arrange
            var command = new CreatePersonCommand
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                PersonalNumber = "01005030303",
                Gender = Gender.Male,
                PhoneNumbers = []
            };
            _personRepositoryMock.Setup(r => r.ExistsByPersonalNumberAsync(command.PersonalNumber, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<PersonAlreadyExistsException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task CreatesPerson_Successfully()
        {
            // Arrange
            var command = new CreatePersonCommand
            {
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = new DateTime(1985, 5, 5),
                PersonalNumber = "02006040404",
                Gender = Gender.Female,
                PhoneNumbers =
                [
                    new() { PhoneNumberType = PhoneNumberType.Mobile, Number = "123456789" }
                ]
            };
            _personRepositoryMock.Setup(r => r.ExistsByPersonalNumberAsync(command.PersonalNumber, It.IsAny<CancellationToken>())).ReturnsAsync(false);
            
            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _personRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()), Times.Once);
            _personRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.NotNull(response);
        }
    }
}
