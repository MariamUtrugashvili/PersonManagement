using MediatR;
using Moq;
using PersonManagement.Application.Caching;
using PersonManagement.Application.Constants;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.Persons.Commands.UpdatePerson;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;
using PersonManagement.Domain.Repositories;

namespace PersonManagement.Application.UnitTests.Persons.Commands
{
    public class UpdatePersonCommandHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock = new();
        private readonly Mock<ICacheService> _cacheServiceMock = new();
        private readonly UpdatePersonCommandHandler _handler;

        public UpdatePersonCommandHandlerTests()
        {
            _handler = new UpdatePersonCommandHandler(_personRepositoryMock.Object, _cacheServiceMock.Object);
        }

        [Fact]
        public async Task ThrowsException_WhenPersonNotFound()
        {
            // Arrange
            var command = new UpdatePersonCommand { Id = 1 };
            
            //Act
            _personRepositoryMock.Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync((Person?)null);
            
            //Assert
            await Assert.ThrowsAsync<PersonNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task UpdatesPerson_AndRemovesCache()
        {
            //Arrange
            var command = new UpdatePersonCommand
            {
                Id = 2,
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
            var person = Person.Create("Jane", "Smith", command.DateOfBirth, command.PersonalNumber, command.Gender);
            person.AddPhoneNumber(PhoneNumberType.Mobile, "555123123");

            _personRepositoryMock.Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(person);
          
            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(Unit.Value, result);
            _personRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _cacheServiceMock.Verify(c => c.RemoveAsync(It.Is<string>(k => k == CacheConstants.GetPersonCacheKey(command.Id))), Times.Once);
        }
    }
}
