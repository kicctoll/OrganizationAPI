using System;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Moq;
using Xunit;

namespace ApplicationCore.Tests.Services
{
    public class BusinessServiceTests
    {
        private readonly BusinessService _businessService;
        private readonly Mock<IRepository<Business>> _repositoryMock;

        public BusinessServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Business>>();
            _businessService = new BusinessService(_repositoryMock.Object);
        }

        #region GetAllAsync;

        [Fact]
        public async Task GetAllAsync_WhenCalled_ReturnNotNull()
        {
            // Arrange
            _repositoryMock
                .Setup(rep => rep.GetAllAsync())
                .ReturnsAsync(TestData.GetAllBusinesses());

            // Act
            var businesses = await _businessService.GetAllAsync();

            // Assert
            Assert.NotNull(businesses);
        }

        #endregion;

        #region GetItemByIdAsync;

        [Fact]
        public async Task GetItemById_ExistingIdPassed_ReturnNotNull()
        {
            // Arrange
            int businessId = 1;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(businessId))
                .ReturnsAsync(TestData.GetBusinessById(businessId));

            // Act
            var business = await _businessService.GetItemByIdAsync(businessId);

            // Assert
            Assert.NotNull(business);
        }

        [Fact]
        public async Task GetItemById_ExistingIdPassed_ReturnValidObject()
        {
            // Arrange
            int businessId = 1;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(businessId))
                .ReturnsAsync(TestData.GetBusinessById(businessId));

            // Act
            var business = await _businessService.GetItemByIdAsync(businessId);

            // Assert
            Assert.Equal(businessId, business.Id);
        }

        [Fact]
        public async Task GetItemById_UnknownIdPassed_ReturnNull()
        {
            // Arrange
            int businessId = 11;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(businessId))
                .ReturnsAsync(TestData.GetBusinessById(businessId));

            // Act
            var business = await _businessService.GetItemByIdAsync(businessId);

            // Assert
            Assert.Null(business);
        }

        #endregion;

        #region CreateAsync;

        [Fact]
        public async Task CreateAsync_ValidObjectPassed_ReturnCreatedBusiness()
        {
            // Arrange
            var business = new Business("Some name");
            _repositoryMock
                .Setup(rep => rep.AddAsync(business))
                .ReturnsAsync(business);

            // Act
            var createdBusiness = await _businessService.CreateAsync(business);

            // Assert
            Assert.Equal(business.Name, createdBusiness.Name);
        }

        #endregion;

        #region UpdateAsync;

        [Fact]
        public async Task UpdateAsync_UnknownIdPassed_ThrowExceptionIsNotNull()
        {
            // Arrange
            int businessId = 11;
            var updatedBusiness = new Business("Name") { Id = businessId };
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(businessId))
                .ReturnsAsync(TestData.GetBusinessById(businessId));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _businessService.UpdateAsync(businessId, updatedBusiness));

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task UpdateAsync_ExistingIdPassed_NotThrowAnException()
        {
            // Arrange
            int businessId = 1;
            var oldBusiness = TestData.GetBusinessById(businessId);
            var updatedBusiness = new Business("Name") { Id = businessId };

            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(businessId))
                .ReturnsAsync(oldBusiness);
            _repositoryMock
                .Setup(rep => rep.UpdateAsync(oldBusiness, updatedBusiness))
                .Returns(() => Task.FromResult(0));


            // Act
            await _businessService.UpdateAsync(businessId, updatedBusiness);
        }

        #endregion;

        #region DeleteAsync;

        [Fact]
        public async Task DeleteAsync_UnknownIdPassed_ThrowAnException()
        {
            // Arrange
            int businessId = 11;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(businessId))
                .ReturnsAsync(TestData.GetBusinessById(businessId));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _businessService.DeleteAsync(businessId));

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task DeleteAsync_ExistingIdPassed_NotThrowException()
        {
            // Arrange
            int businessId = 1;
            var businessWhichShouldBeDeleted = TestData.GetBusinessById(businessId);

            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(businessId))
                .ReturnsAsync(businessWhichShouldBeDeleted);
            _repositoryMock
                .Setup(rep => rep.DeleteAsync(businessWhichShouldBeDeleted))
                .Returns(() => Task.FromResult(0));

            // Act
            await _businessService.DeleteAsync(businessId);
        }

        #endregion;
    }
}
