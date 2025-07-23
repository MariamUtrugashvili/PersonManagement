using MediatR;
using Moq;
using PersonManagement.Application.Caching;
using PersonManagement.Application.Constants;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.Persons.Commands.AddRelatedPerson;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;
using PersonManagement.Domain.Repositories;

namespace PersonManagement.Application.UnitTests.Persons.Commands
{
    public class AddRelatedPersonCommandHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock = new();
        private readonly Mock<ICacheService> _cacheServiceMock = new();
        private readonly AddRelatedPersonCommandHandler _handler;

        public AddRelatedPersonCommandHandlerTests()
        {
            _handler = new AddRelatedPersonCommandHandler(_personRepositoryMock.Object, _cacheServiceMock.Object);
        }

        [Fact]
        public async Task ThrowsException_WhenPersonNotFound()
        {
            //Arrange
            var command = new AddRelatedPersonCommand { PersonId = 1, RelatedToPersonId = 2, RelationType = RelationType.Family };
            
            _personRepositoryMock.Setup(r => r.GetByIdAsync(command.PersonId, It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync((Person?)null);
            
            //Act & Assert
            await Assert.ThrowsAsync<PersonNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task ThrowsException_WhenRelatedPersonNotFound()
        {
            //Arrange
            var command = new AddRelatedPersonCommand { PersonId = 1, RelatedToPersonId = 2, RelationType = RelationType.Family };
            var person = Person.Create("John", "Doe", new DateTime(1990, 1, 1), "01005030303", Gender.Male);
            
            _personRepositoryMock.Setup(r => r.GetByIdAsync(command.PersonId, It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(person);
            _personRepositoryMock.Setup(r => r.ExistsByIdAsync(command.RelatedToPersonId, It.IsAny<CancellationToken>())).ReturnsAsync(false);

            //Act & Assert
            await Assert.ThrowsAsync<PersonNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task ThrowsException_WhenRelationAlreadyExists()
        {
            //Arrange
            var command = new AddRelatedPersonCommand { PersonId = 1, RelatedToPersonId = 2, RelationType = RelationType.Family };
            var person = Person.Create("John", "Doe", new DateTime(1990, 1, 1), "01005030303", Gender.Male);
            var related = Person.Create("Jane", "Smith", new DateTime(1985, 5, 5), "02006040404", Gender.Female);
            person.AddRelatedPerson(command.RelatedToPersonId, command.RelationType);

            _personRepositoryMock.Setup(r => r.GetByIdAsync(command.PersonId, It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(person);
            _personRepositoryMock.Setup(r => r.ExistsByIdAsync(command.RelatedToPersonId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            //Act & Assert
            await Assert.ThrowsAsync<RelationAlreadyExistsException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task AddsRelatedPerson_AndRemovesCache()
        {
            //Arrange
            var command = new AddRelatedPersonCommand { PersonId = 1, RelatedToPersonId = 2, RelationType = RelationType.Family };
            var person = Person.Create("John", "Doe", new DateTime(1990, 1, 1), "01005030303", Gender.Male);
            _personRepositoryMock.Setup(r => r.GetByIdAsync(command.PersonId, It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(person);
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
