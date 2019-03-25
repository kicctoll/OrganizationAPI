using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Moq;
using Xunit;

namespace ApplicationCore.Tests.Services
{
    public class OrganizationServiceTests
    {
        private readonly OrganizationService _organizationService;
        private readonly Mock<IRepository<Organization>> _repositoryMock;

        public OrganizationServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Organization>>();
            _organizationService = new OrganizationService(_repositoryMock.Object);
        }

        #region GetAllAsync;

        [Fact]
        public async Task GetAllAsync_WhenCalled_ReturnNotNullAndCountShouldBe10()
        {
            // Arrange
            var expectedOrganizations = TestData.GetAllOrganizations();
            _repositoryMock
                .Setup(rep => rep.GetAllAsync())
                .ReturnsAsync(expectedOrganizations);

            // Act
            var result = await _organizationService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedOrganizations.Count, result.Count);
        }

        #endregion;

        #region GetItemByIdAsync;

        [Fact]
        public async Task GetItemByIdAsync_ExistingIdPassed_ReturnAnObjectWithPassedId()
        {
            // Arrange
            int passedId = 1;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(passedId))
                .ReturnsAsync(TestData.GetOrganizationById(passedId));

            // Act
            var organization = await _organizationService.GetItemByIdAsync(passedId);

            // Assert
            Assert.Equal(passedId, organization.Id);
        }

        [Fact]
        public async Task GetItemByIdAsync_UnknownIdPassed_ReturnNull()
        {
            // Arrange
            int passedId = 11;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(passedId))
                .ReturnsAsync(TestData.GetOrganizationById(passedId));

            // Act
            var organization = await _organizationService.GetItemByIdAsync(passedId);

            // Assert
            Assert.Null(organization);
        }

        #endregion;

        #region CreateAsync;

        [Fact]
        public async Task CreateAsync_ValidObjectPassed_ReturnCreatedOrganization()
        {
            // Arrange
            var organization = new Organization("Organization 1", 1, "Type 1", "Owner 1");
            _repositoryMock
                .Setup(rep => rep.AddAsync(organization))
                .ReturnsAsync(organization);

            // Act
            var createdOrganization = await _organizationService.CreateAsync(organization);

            // Assert
            Assert.IsType<Organization>(createdOrganization);
            Assert.Equal(organization.Name, createdOrganization.Name);
        }

        #endregion;

        #region UpdateAsync;

        [Fact]
        public async Task UpdateAsync_ExistingIdPassed_NewOrganizationIdEqualToPassedId()
        {
            // Arrange
            var organizationId = 1;
            var oldOrganization = TestData.GetOrganizationById(organizationId);
            var newOrganization = new Organization("Updated organization 1", 1, "Updated type 1", "Updated owner 1") { Id = 1 };

            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(organizationId))
                .ReturnsAsync(oldOrganization);
            _repositoryMock
                .Setup(rep => rep.UpdateAsync(oldOrganization, newOrganization))
                .Returns(() => Task.FromResult(0));

            // Act
            await _organizationService.UpdateAsync(organizationId, newOrganization);

            // Assert
            Assert.Equal(organizationId, newOrganization.Id);
        }

        [Fact]
        public async Task UpdateAsync_UnknownIdPassed_ThrowsException()
        {
            // Arrange
            var organizationId = 11;
            var oldOrganization = TestData.GetOrganizationById(organizationId);
            var newOrganization = new Organization("Updated organization 11", 1, "Updated type 11", "Updated owner 11") { Id = 11 };

            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(organizationId))
                .ReturnsAsync(oldOrganization);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _organizationService.UpdateAsync(organizationId, newOrganization));

            // Arrange
            Assert.Equal($"Entity type {typeof(Organization).Name} with id {organizationId} doesn't exist!", exception.Message);
        }

        #endregion;

        #region DeleteAsync;

        [Fact]
        public async Task DeleteAsync_ExistingIdPassed_NotThrowException()
        {
            // Arrange
            var organizationId = 1;
            var organizationThatShouldBeDeleted = TestData.GetOrganizationById(organizationId);

            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(organizationId))
                .ReturnsAsync(organizationThatShouldBeDeleted);
            _repositoryMock
                .Setup(rep => rep.DeleteAsync(organizationThatShouldBeDeleted))
                .Returns(() => Task.FromResult(0));

            // Act
            await _organizationService.DeleteAsync(organizationId);
        }

        [Fact]
        public async Task DeleteAsync_UnknownIdPassed_ThrowAnExceptionAndValidExceptionMessage()
        {
            // Arrange
            var organizationId = 11;
            var organizationThatShouldBeDeleted = TestData.GetOrganizationById(organizationId);

            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(organizationId))
                .ReturnsAsync(organizationThatShouldBeDeleted);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _organizationService.DeleteAsync(organizationId));

            // Assert
            Assert.Equal($"Entity type {typeof(Organization).Name} with id {organizationId} doesn't exist!", exception.Message);
        }

        #endregion DeleteAsync;
    }
}
