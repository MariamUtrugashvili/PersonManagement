using Moq;
using PersonManagement.Application.Caching;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.Persons.Queries.GetPersonById;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;
using PersonManagement.Domain.Repositories;

namespace PersonManagement.Application.UnitTests.Persons.Queries
{
    public class GetPersonByIdQueryHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock = new();
        private readonly Mock<ICacheService> _cacheServiceMock = new();
        private readonly GetPersonByIdQueryHandler _handler;

        public GetPersonByIdQueryHandlerTests()
        {
            _handler = new GetPersonByIdQueryHandler(_personRepositoryMock.Object, _cacheServiceMock.Object);
        }

        [Fact]
        public async Task ReturnsCachedValue_IfPresent()
        {
            // Arrange
            var query = new GetPersonByIdQuery { Id = 1 };
            var cached = new GetPersonByIdResponse { Id = 1, FirstName = "Cached", LastName = "User" };
            _cacheServiceMock.Setup(c => c.GetAsync<GetPersonByIdResponse>(It.IsAny<string>())).ReturnsAsync(cached);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(cached, result);
            _cacheServiceMock.Verify(c => c.GetAsync<GetPersonByIdResponse>(It.IsAny<string>()), Times.Once);
            _personRepositoryMock.Verify(r => r.GetByIdNoTrackingAsync(It.IsAny<int>(), It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never);
        }

        [Fact]
        public async Task ReturnsFromRepository_AndCaches_IfNotCached()
        {
            // Arrange
            var query = new GetPersonByIdQuery { Id = 1 };
            _cacheServiceMock.Setup(c => c.GetAsync<GetPersonByIdResponse>(It.IsAny<string>())).ReturnsAsync((GetPersonByIdResponse?)null);

            var person = Person.Create("John", "Doe", new DateTime(1990, 1, 1), "01005030303", Gender.Male);

            _personRepositoryMock.Setup(r => r.GetByIdNoTrackingAsync(It.IsAny<int>(), It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(person);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(person.FirstName, result.FirstName);
            Assert.Equal(person.LastName, result.LastName);
            _cacheServiceMock.Verify(c => c.SetAsync(It.IsAny<string>(), It.IsAny<GetPersonByIdResponse>(), It.IsAny<TimeSpan>()), Times.Once);
            _personRepositoryMock.Verify(r => r.GetByIdNoTrackingAsync(It.IsAny<int>(), It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task ThrowsNotFound_IfPersonDoesNotExist()
        {
            // Arrange
            var query = new GetPersonByIdQuery { Id = 1 };
            _cacheServiceMock.Setup(c => c.GetAsync<GetPersonByIdResponse>(It.IsAny<string>())).ReturnsAsync((GetPersonByIdResponse?)null);
            _personRepositoryMock.Setup(r => r.GetByIdNoTrackingAsync(It.IsAny<int>(), It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync((Person?)null);

            // Act & Assert
            await Assert.ThrowsAsync<PersonNotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}
