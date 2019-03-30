using System;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Moq;
using Xunit;

namespace ApplicationCore.Tests.Services
{
    public class DepartmentServiceTests
    {
        private readonly Mock<IRepository<Department>> _repositoryMock;
        private readonly DepartmentService _departmentService;

        public DepartmentServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Department>>();
            _departmentService = new DepartmentService(_repositoryMock.Object);
        }

        #region GetAllAsync;

        [Fact]
        public async Task GetAllAsync_WhenCalled_ReturnNotNull()
        {
            // Arrange
            _repositoryMock
                .Setup(rep => rep.GetAllAsync())
                .ReturnsAsync(TestData.GetAllDepartments());

            // Act
            var departments = await _departmentService.GetAllAsync();

            // Assert
            Assert.NotNull(departments);
        }

        #endregion;

        #region GetItemByIdAsync;

        [Fact]
        public async Task GetItemById_ExistingIdPassed_ReturnNotNull()
        {
            // Arrange
            int departmentId = 1;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(departmentId))
                .ReturnsAsync(TestData.GetDepartmentById(departmentId));

            // Act
            var department = await _departmentService.GetItemByIdAsync(departmentId);

            // Assert
            Assert.NotNull(department);
        }

        [Fact]
        public async Task GetItemById_ExistingIdPassed_ReturnValidObject()
        {
            // Arrange
            int departmentId = 1;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(departmentId))
                .ReturnsAsync(TestData.GetDepartmentById(departmentId));

            // Act
            var department = await _departmentService.GetItemByIdAsync(departmentId);

            // Assert
            Assert.Equal(departmentId, department.Id);
        }

        [Fact]
        public async Task GetItemById_UnknownIdPassed_ReturnNull()
        {
            // Arrange
            int departmentId = 11;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(departmentId))
                .ReturnsAsync(TestData.GetDepartmentById(departmentId));

            // Act
            var department = await _departmentService.GetItemByIdAsync(departmentId);

            // Assert
            Assert.Null(department);
        }

        #endregion;

        #region CreateAsync;

        [Fact]
        public async Task CreateAsync_ValidObjectPassed_ReturnCreatedBusiness()
        {
            // Arrange
            var department = new Department("Some name");
            _repositoryMock
                .Setup(rep => rep.AddAsync(department))
                .ReturnsAsync(department);

            // Act
            var createdDepartment = await _departmentService.CreateAsync(department);

            // Assert
            Assert.Equal(department.Name, createdDepartment.Name);
        }

        #endregion;

        #region UpdateAsync;

        [Fact]
        public async Task UpdateAsync_UnknownIdPassed_ThrowExceptionIsNotNull()
        {
            // Arrange
            int departmentId = 11;
            var updatedDepartment = new Department("Name") { Id = departmentId };
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(departmentId))
                .ReturnsAsync(TestData.GetDepartmentById(departmentId));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _departmentService.UpdateAsync(departmentId, updatedDepartment));

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task UpdateAsync_ExistingIdPassed_NotThrowAnException()
        {
            // Arrange
            int departmentId = 1;
            var oldDepartment = TestData.GetDepartmentById(departmentId);
            var updatedDepartment = new Department("Name") { Id = departmentId };

            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(departmentId))
                .ReturnsAsync(oldDepartment);
            _repositoryMock
                .Setup(rep => rep.UpdateAsync(oldDepartment, updatedDepartment))
                .Returns(() => Task.FromResult(0));


            // Act
            await _departmentService.UpdateAsync(departmentId, updatedDepartment);
        }

        #endregion;

        #region DeleteAsync;

        [Fact]
        public async Task DeleteAsync_UnknownIdPassed_ThrowAnException()
        {
            // Arrange
            int departmentId = 11;
            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(departmentId))
                .ReturnsAsync(TestData.GetDepartmentById(departmentId));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _departmentService.DeleteAsync(departmentId));

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task DeleteAsync_ExistingIdPassed_NotThrowException()
        {
            // Arrange
            int departmentId = 1;
            var departmentWhichShouldBeDeleted = TestData.GetDepartmentById(departmentId);

            _repositoryMock
                .Setup(rep => rep.GetByIdAsync(departmentId))
                .ReturnsAsync(departmentWhichShouldBeDeleted);
            _repositoryMock
                .Setup(rep => rep.DeleteAsync(departmentWhichShouldBeDeleted))
                .Returns(() => Task.FromResult(0));

            // Act
            await _departmentService.DeleteAsync(departmentId);
        }

        #endregion;
    }
}
