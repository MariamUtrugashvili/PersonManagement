using Moq;
using PersonManagement.Application.Persons.Queries.SearchPersons;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;
using PersonManagement.Domain.Repositories;

namespace PersonManagement.Application.UnitTests.Persons.Queries
{
    public class SearchPersonsQueryHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock = new();
        private readonly SearchPersonsQueryHandler _handler;

        public SearchPersonsQueryHandlerTests()
        {
            _handler = new SearchPersonsQueryHandler(_personRepositoryMock.Object);
        }

        [Fact]
        public async Task ReturnsPagedPersons_WhenPersonsExist()
        {
            // Arrange
            var query = new SearchPersonsQuery { Q = null, FirstName = "John", PageNumber = 1, PageSize = 2 };
            var persons = new List<Person>
            {
                Person.Create("John", "Doe", new DateTime(1990, 1, 1), "01005030303", Gender.Male),
                Person.Create("John", "Smith", new DateTime(1985, 5, 5), "02006040404", Gender.Male)
            };
            _personRepositoryMock.Setup(r => r.SearchAsync(query.Q, query.FirstName, query.LastName, query.PersonalNumber, query.PageNumber, query.PageSize, It.IsAny<CancellationToken>()))
                .ReturnsAsync(persons);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(2, result.Items.Count);
            Assert.All(result.Items, item => Assert.Equal("John", item.FirstName));
            _personRepositoryMock.Verify(r => r.SearchAsync(query.Q, query.FirstName, query.LastName, query.PersonalNumber, query.PageNumber, query.PageSize, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ReturnsEmpty_WhenNoPersonsFound()
        {
            // Arrange
            var query = new SearchPersonsQuery { Q = "Nonexistent", PageNumber = 1, PageSize = 10 };
            _personRepositoryMock.Setup(r => r.SearchAsync(query.Q, query.FirstName, query.LastName, query.PersonalNumber, query.PageNumber, query.PageSize, It.IsAny<CancellationToken>()))
                .ReturnsAsync([]);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(0, result.TotalCount);
            Assert.Empty(result.Items);
            _personRepositoryMock.Verify(r => r.SearchAsync(query.Q, query.FirstName, query.LastName, query.PersonalNumber, query.PageNumber, query.PageSize, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
