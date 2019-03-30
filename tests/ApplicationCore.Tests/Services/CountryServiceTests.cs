using System;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Moq;
using Xunit;

namespace ApplicationCore.Tests.Services
{
    public class CountryServiceTests
    {
        private readonly CountryService _countryService;
        private readonly Mock<IRepository<Country>> _repositoryMock;

        public CountryServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Country>>();
            _countryService = new CountryService(_repositoryMock.Object);
        }

        #region GetAllAsync;

        [Fact]
        public async Task GetAllAsync_WhenCalled_ReturnNotNull()
        {
            // Arrange
            _repositoryMock
                .Setup(rep => rep.GetAllAsync())
                .ReturnsAsync(TestData.GetAllCountries());

            // Act
            var countries = await _countryService.GetAllAsync();

            // Assert
            Assert.NotNull(countries);
        }

        #endregion;

        #region GetItemByIdAsync;

        [Fact]
        public async Task GetItemById_ExistingIdPassed_ReturnNotNull()
        {
            // Arrange
            int countryId = 1;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(countryId))
                .ReturnsAsync(TestData.GetCountyById(countryId));

            // Act
            var country = await _countryService.GetItemByIdAsync(countryId);

            // Assert
            Assert.NotNull(country);
        }

        [Fact]
        public async Task GetItemById_ExistingIdPassed_ReturnValidObject()
        {
            // Arrange
            int countryId = 1;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(countryId))
                .ReturnsAsync(TestData.GetCountyById(countryId));

            // Act
            var country = await _countryService.GetItemByIdAsync(countryId);

            // Assert
            Assert.Equal(countryId, country.Id);
        }

        [Fact]
        public async Task GetItemById_UnknownIdPassed_ReturnNull()
        {
            // Arrange
            int countryId = 11;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(countryId))
                .ReturnsAsync(TestData.GetCountyById(countryId));

            // Act
            var country = await _countryService.GetItemByIdAsync(countryId);

            // Assert
            Assert.Null(country);
        }

        #endregion;

        #region CreateAsync;

        [Fact]
        public async Task CreateAsync_ValidObjectPassed_ReturnCreatedCountry()
        {
            // Arrange
            var country = new Country("Some name", 201);
            _repositoryMock
                .Setup(rep => rep.AddAsync(country))
                .ReturnsAsync(country);

            // Act
            var createdCountry = await _countryService.CreateAsync(country);

            // Assert
            Assert.Equal(country.Name, createdCountry.Name);
            Assert.Equal(country.Code, createdCountry.Code);
        }

        #endregion;

        #region UpdateAsync;

        [Fact]
        public async Task UpdateAsync_UnknownIdPassed_ThrowExceptionIsNotNull()
        {
            // Arrange
            int countryId = 11;
            var updatedCountry = new Country("Name", 202) { Id = countryId };
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(countryId))
                .ReturnsAsync(TestData.GetCountyById(countryId));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _countryService.UpdateAsync(countryId, updatedCountry));

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task UpdateAsync_ExistingIdPassed_NotThrowAnException()
        {
            // Arrange
            int countryId = 1;
            var oldCountry = TestData.GetCountyById(countryId);
            var updatedCountry = new Country("Name", 202) { Id = countryId };

            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(countryId))
                .ReturnsAsync(oldCountry);
            _repositoryMock
                .Setup(rep => rep.UpdateAsync(oldCountry, updatedCountry))
                .Returns(() => Task.FromResult(0));


            // Act
            await _countryService.UpdateAsync(countryId, updatedCountry);
        }

        #endregion;

        #region DeleteAsync;

        [Fact]
        public async Task DeleteAsync_UnknownIdPassed_ThrowAnException()
        {
            // Arrange
            int countryId = 11;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(countryId))
                .ReturnsAsync(TestData.GetCountyById(countryId));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _countryService.DeleteAsync(countryId));

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task DeleteAsync_ExistingIdPassed_NotThrowException()
        {
            // Arrange
            int countryId = 1;
            var countryWhichShouldBeDeleted = TestData.GetCountyById(countryId);

            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(countryId))
                .ReturnsAsync(countryWhichShouldBeDeleted);
            _repositoryMock
                .Setup(rep => rep.DeleteAsync(countryWhichShouldBeDeleted))
                .Returns(() => Task.FromResult(0));

            // Act
            await _countryService.DeleteAsync(countryId);
        }

        #endregion;
    }
}
