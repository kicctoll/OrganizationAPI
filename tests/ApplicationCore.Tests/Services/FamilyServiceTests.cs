using System;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Moq;
using Xunit;

namespace ApplicationCore.Tests.Services
{
    public class FamilyServiceTests
    {
        private readonly Mock<IRepository<Family>> _repositoryMock;
        private readonly FamilyService _familyService;

        public FamilyServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Family>>();
            _familyService = new FamilyService(_repositoryMock.Object);
        }

        #region GetAllAsync;

        [Fact]
        public async Task GetAllAsync_WhenCalled_ReturnNotNull()
        {
            // Arrange
            _repositoryMock
                .Setup(rep => rep.GetAllAsync())
                .ReturnsAsync(TestData.GetAllFamilies());

            // Act
            var families = await _familyService.GetAllAsync();

            // Assert
            Assert.NotNull(families);
        }

        #endregion;

        #region GetItemByIdAsync;

        [Fact]
        public async Task GetItemById_ExistingIdPassed_ReturnNotNull()
        {
            // Arrange
            int familyId = 1;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(familyId))
                .ReturnsAsync(TestData.GetFamilyById(familyId));

            // Act
            var family = await _familyService.GetItemByIdAsync(familyId);

            // Assert
            Assert.NotNull(family);
        }

        [Fact]
        public async Task GetItemById_ExistingIdPassed_ReturnValidObject()
        {
            // Arrange
            int familyId = 1;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(familyId))
                .ReturnsAsync(TestData.GetFamilyById(familyId));

            // Act
            var family = await _familyService.GetItemByIdAsync(familyId);

            // Assert
            Assert.Equal(familyId, family.Id);
        }

        [Fact]
        public async Task GetItemById_UnknownIdPassed_ReturnNull()
        {
            // Arrange
            int familyId = 11;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(familyId))
                .ReturnsAsync(TestData.GetFamilyById(familyId));

            // Act
            var family = await _familyService.GetItemByIdAsync(familyId);

            // Assert
            Assert.Null(family);
        }

        #endregion;

        #region CreateAsync;

        [Fact]
        public async Task CreateAsync_ValidObjectPassed_ReturnCreatedBusiness()
        {
            // Arrange
            var family = new Family("Some name");
            _repositoryMock
                .Setup(rep => rep.AddAsync(family))
                .ReturnsAsync(family);

            // Act
            var createdFamily = await _familyService.CreateAsync(family);

            // Assert
            Assert.Equal(family.Name, createdFamily.Name);
        }

        #endregion;

        #region UpdateAsync;

        [Fact]
        public async Task UpdateAsync_UnknownIdPassed_ThrowExceptionIsNotNull()
        {
            // Arrange
            int familyId = 11;
            var updatedFamily = new Family("Name") { Id = familyId };
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(familyId))
                .ReturnsAsync(TestData.GetFamilyById(familyId));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _familyService.UpdateAsync(familyId, updatedFamily));

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task UpdateAsync_ExistingIdPassed_NotThrowAnException()
        {
            // Arrange
            int familyId = 1;
            var oldFamily = TestData.GetFamilyById(familyId);
            var updatedFamily = new Family("Name") { Id = familyId };

            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(familyId))
                .ReturnsAsync(oldFamily);
            _repositoryMock
                .Setup(rep => rep.UpdateAsync(oldFamily, updatedFamily))
                .Returns(() => Task.FromResult(0));


            // Act
            await _familyService.UpdateAsync(familyId, updatedFamily);
        }

        #endregion;

        #region DeleteAsync;

        [Fact]
        public async Task DeleteAsync_UnknownIdPassed_ThrowAnException()
        {
            // Arrange
            int familyId = 11;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(familyId))
                .ReturnsAsync(TestData.GetFamilyById(familyId));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _familyService.DeleteAsync(familyId));

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task DeleteAsync_ExistingIdPassed_NotThrowException()
        {
            // Arrange
            int familyId = 1;
            var familyWhichShouldBeDeleted = TestData.GetFamilyById(familyId);

            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(familyId))
                .ReturnsAsync(familyWhichShouldBeDeleted);
            _repositoryMock
                .Setup(rep => rep.DeleteAsync(familyWhichShouldBeDeleted))
                .Returns(() => Task.FromResult(0));

            // Act
            await _familyService.DeleteAsync(familyId);
        }

        #endregion;
    }
}
