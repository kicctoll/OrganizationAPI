using System;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Moq;
using Xunit;

namespace ApplicationCore.Tests.Services
{
    public class OfferingServiceTests
    {
        private readonly Mock<IRepository<Offering>> _repositoryMock;
        private readonly OfferingService _offeringService;

        public OfferingServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Offering>>();
            _offeringService = new OfferingService(_repositoryMock.Object);
        }

        #region GetAllAsync;

        [Fact]
        public async Task GetAllAsync_WhenCalled_ReturnNotNull()
        {
            // Arrange
            _repositoryMock
                .Setup(rep => rep.GetAllAsync())
                .ReturnsAsync(TestData.GetAllOfferings());

            // Act
            var offerings = await _offeringService.GetAllAsync();

            // Assert
            Assert.NotNull(offerings);
        }

        #endregion;

        #region GetItemByIdAsync;

        [Fact]
        public async Task GetItemById_ExistingIdPassed_ReturnNotNull()
        {
            // Arrange
            int offeringId = 1;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(offeringId))
                .ReturnsAsync(TestData.GetOfferingById(offeringId));

            // Act
            var offering = await _offeringService.GetItemByIdAsync(offeringId);

            // Assert
            Assert.NotNull(offering);
        }

        [Fact]
        public async Task GetItemById_ExistingIdPassed_ReturnValidObject()
        {
            // Arrange
            int offeringId = 1;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(offeringId))
                .ReturnsAsync(TestData.GetOfferingById(offeringId));

            // Act
            var offering = await _offeringService.GetItemByIdAsync(offeringId);

            // Assert
            Assert.Equal(offeringId, offering.Id);
        }

        [Fact]
        public async Task GetItemById_UnknownIdPassed_ReturnNull()
        {
            // Arrange
            int offeringId = 11;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(offeringId))
                .ReturnsAsync(TestData.GetOfferingById(offeringId));

            // Act
            var offering = await _offeringService.GetItemByIdAsync(offeringId);

            // Assert
            Assert.Null(offering);
        }

        #endregion;

        #region CreateAsync;

        [Fact]
        public async Task CreateAsync_ValidObjectPassed_ReturnCreatedBusiness()
        {
            // Arrange
            var offering = new Offering("Some name");
            _repositoryMock
                .Setup(rep => rep.AddAsync(offering))
                .ReturnsAsync(offering);

            // Act
            var createdOffering = await _offeringService.CreateAsync(offering);

            // Assert
            Assert.Equal(offering.Name, createdOffering.Name);
        }

        #endregion;

        #region UpdateAsync;

        [Fact]
        public async Task UpdateAsync_UnknownIdPassed_ThrowExceptionIsNotNull()
        {
            // Arrange
            int offeringId = 11;
            var updatedOffering = new Offering("Name") { Id = offeringId };
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(offeringId))
                .ReturnsAsync(TestData.GetOfferingById(offeringId));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _offeringService.UpdateAsync(offeringId, updatedOffering));

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task UpdateAsync_ExistingIdPassed_NotThrowAnException()
        {
            // Arrange
            int offeringId = 1;
            var oldOffering = TestData.GetOfferingById(offeringId);
            var updatedOffering = new Offering("Name") { Id = offeringId };

            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(offeringId))
                .ReturnsAsync(oldOffering);
            _repositoryMock
                .Setup(rep => rep.UpdateAsync(oldOffering, updatedOffering))
                .Returns(() => Task.FromResult(0));


            // Act
            await _offeringService.UpdateAsync(offeringId, updatedOffering);
        }

        #endregion;

        #region DeleteAsync;

        [Fact]
        public async Task DeleteAsync_UnknownIdPassed_ThrowAnException()
        {
            // Arrange
            int offeringId = 11;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(offeringId))
                .ReturnsAsync(TestData.GetOfferingById(offeringId));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _offeringService.DeleteAsync(offeringId));

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task DeleteAsync_ExistingIdPassed_NotThrowException()
        {
            // Arrange
            int offeringId = 1;
            var offeringWhichShouldBeDeleted = TestData.GetOfferingById(offeringId);

            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(offeringId))
                .ReturnsAsync(offeringWhichShouldBeDeleted);
            _repositoryMock
                .Setup(rep => rep.DeleteAsync(offeringWhichShouldBeDeleted))
                .Returns(() => Task.FromResult(0));

            // Act
            await _offeringService.DeleteAsync(offeringId);
        }

        #endregion;
    }
}
