using MediatR;
using Moq;
using PersonManagement.Application.Caching;
using PersonManagement.Application.Constants;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.Persons.Commands.DeleteRelatedPerson;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;
using PersonManagement.Domain.Repositories;

namespace PersonManagement.Application.UnitTests.Persons.Commands
{
    public class DeleteRelatedPersonCommandHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock = new();
        private readonly Mock<ICacheService> _cacheServiceMock = new();
        private readonly DeleteRelatedPersonCommandHandler _handler;

        public DeleteRelatedPersonCommandHandlerTests()
        {
            _handler = new DeleteRelatedPersonCommandHandler(_personRepositoryMock.Object, _cacheServiceMock.Object);
        }

        [Fact]
        public async Task ThrowsException_WhenPersonNotFound()
        {
            //Arrange
            var command = new DeleteRelatedPersonCommand { PersonId = 1, RelatedToPersonId = 2 };
            
            //Act
            _personRepositoryMock.Setup(r => r.GetByIdAsync(command.PersonId, It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync((Person?)null);

            //Assert
            await Assert.ThrowsAsync<PersonNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task ThrowsException_WhenRelatedPersonNotFound()
        {
            //Arrange
            var command = new DeleteRelatedPersonCommand { PersonId = 1, RelatedToPersonId = 2 };
            var person = Person.Create("John", "Doe", new DateTime(1990, 1, 1), "01005030303", Gender.Male);

            _personRepositoryMock.Setup(r => r.GetByIdAsync(command.PersonId, It.IsAny<CancellationToken>(), false, true)).ReturnsAsync(person);
            _personRepositoryMock.Setup(r => r.ExistsByIdAsync(command.RelatedToPersonId, It.IsAny<CancellationToken>())).ReturnsAsync(false);

            //Act & Assert
            await Assert.ThrowsAsync<PersonNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task ThrowsException_WhenRelationDoesNotExist()
        {
            //Arrange
            var command = new DeleteRelatedPersonCommand { PersonId = 1, RelatedToPersonId = 2 };
            var person = Person.Create("John", "Doe", new DateTime(1990, 1, 1), "01005030303", Gender.Male);

            _personRepositoryMock.Setup(r => r.GetByIdAsync(command.PersonId, It.IsAny<CancellationToken>(), false, true)).ReturnsAsync(person);
            _personRepositoryMock.Setup(r => r.ExistsByIdAsync(command.RelatedToPersonId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            //Act & Assert
            await Assert.ThrowsAsync<RelationDoesNotExistException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task DeletesRelatedPerson_AndRemovesCache()
        {
            //Arrange
            var command = new DeleteRelatedPersonCommand { PersonId = 1, RelatedToPersonId = 2 };
            var person = Person.Create("John", "Doe", new DateTime(1990, 1, 1), "01005030303", Gender.Male);

            person.AddRelatedPerson(command.RelatedToPersonId, RelationType.Family);

            _personRepositoryMock.Setup(r => r.GetByIdAsync(command.PersonId, It.IsAny<CancellationToken>(), false, true)).ReturnsAsync(person);
            _personRepositoryMock.Setup(r => r.ExistsByIdAsync(command.RelatedToPersonId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(Unit.Value, result);
            _personRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _cacheServiceMock.Verify(c => c.RemoveAsync(It.Is<string>(k => k == CacheConstants.GetPersonCacheKey(command.PersonId))), Times.Once);
        }
    }
}
