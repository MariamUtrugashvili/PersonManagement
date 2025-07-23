using Moq;
using PersonManagement.Application.Persons.Commands.DeletePerson;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.Caching;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;
using PersonManagement.Domain.Repositories;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using PersonManagement.Application.Constants;
using MediatR;

namespace PersonManagement.Application.UnitTests.Persons.Commands
{
    public class DeletePersonCommandHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock = new();
        private readonly Mock<ICacheService> _cacheServiceMock = new();
        private readonly DeletePersonCommandHandler _handler;

        public DeletePersonCommandHandlerTests()
        {
            _handler = new DeletePersonCommandHandler(_personRepositoryMock.Object, _cacheServiceMock.Object);
        }

        [Fact]
        public async Task ThrowsException_WhenPersonNotFound()
        {
            // Arrange
            var command = new DeletePersonCommand { Id = 1 };
            _personRepositoryMock.Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync((Person?)null);

            // Act & Assert
            await Assert.ThrowsAsync<PersonNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task DeletesPerson_AndRemovesCache()
        {
            // Arrange
            var command = new DeletePersonCommand { Id = 2 };
            var person = Person.Create("Jane", "Smith", new DateTime(1985, 5, 5), "02006040404", Gender.Female);
            person.AddPhoneNumber(PhoneNumberType.Mobile, "123456789");
            
            _personRepositoryMock.Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(person);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            Assert.True(person.IsDeleted);
            _personRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _cacheServiceMock.Verify(c => c.RemoveAsync(It.Is<string>(k => k == CacheConstants.GetPersonCacheKey(command.Id))), Times.Once);
        }
    }
}
