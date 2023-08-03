using AutoMapper;
using BikeStores.Api.Controllers;
using BikeStores.Application.Interfaces;
using BikeStores.Domain.Dtos;
using BikeStores.Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BikeStores.UnitTests.BrandControllerTests
{
    public class BrandControllerTests
    {
        [Fact]
        public async Task GetBrandAsync_WithUnexistingBrand_ReturnsNotFound()
        {
            //Arrange
            int unexistingBrandId = 999;
            var brandRepositoryMock = new Mock<IBrandRepository>();
            var loggerMock = new Mock<ILogger<BrandController>>();
            var mapperMock = new Mock<IMapper>();

            brandRepositoryMock.Setup(repo => repo.GetAsync(unexistingBrandId)).ReturnsAsync((Brand)null);

            var controller = new BrandController(brandRepositoryMock.Object, loggerMock.Object, mapperMock.Object);

            //Act
            var result = await controller.GetBrandAsync(unexistingBrandId);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetBrand_WithExistingBrand_ReturnsOkWithBrandDto()
        {
            // Arrange
            int existingBrandId = 1;
            var brand = new Brand { BrandId = existingBrandId, BrandName = "Existing Brand" };
            var brandRepositoryMock = new Mock<IBrandRepository>();
            var loggerMock = new Mock<ILogger<BrandController>>();
            var mapperMock = new Mock<IMapper>();

            brandRepositoryMock.Setup(repo => repo.GetAsync(existingBrandId)).ReturnsAsync(brand);
            mapperMock.Setup(mapper => mapper.Map<BrandDto>(brand)).Returns(new BrandDto { BrandId = brand.BrandId, BrandName = brand.BrandName });

            var controller = new BrandController(brandRepositoryMock.Object, loggerMock.Object, mapperMock.Object);

            // Act
            var result = await controller.GetBrandAsync(existingBrandId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result.As<OkObjectResult>();
            okResult.Value.Should().NotBeNull().And.BeAssignableTo<BrandDto>();
            var brandDto = okResult.Value.As<BrandDto>();
            brandDto.BrandId.Should().Be(existingBrandId);
            brandDto.BrandName.Should().Be("Existing Brand");
        }

        [Fact]
        public async Task GetAllBrands_WhenBrandsExist_ReturnsOkWithBrandDtos()
        {
            // Arrange
            var brands = new List<Brand>
            {
                new Brand { BrandId = 1, BrandName = "Brand 1" },
                new Brand { BrandId = 2, BrandName = "Brand 2" },
                new Brand { BrandId = 3, BrandName = "Brand 3" }
            };

            var brandRepositoryMock = new Mock<IBrandRepository>();
            var loggerMock = new Mock<ILogger<BrandController>>();
            var mapperMock = new Mock<IMapper>();

            brandRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(brands);

            mapperMock.Setup(mapper => mapper.Map<IEnumerable<BrandDto>>(brands)).Returns(
                brands.Select(brand => new BrandDto { BrandId = brand.BrandId, BrandName = brand.BrandName }));

            var controller = new BrandController(brandRepositoryMock.Object, loggerMock.Object, mapperMock.Object);

            // Act
            var result = await controller.GetAllBrandsAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result.As<OkObjectResult>();
            okResult.Value.Should().NotBeNull().And.BeAssignableTo<IEnumerable<BrandDto>>();
            var brandDtos = okResult.Value.As<IEnumerable<BrandDto>>();
            brandDtos.Should().HaveCount(brands.Count);
        }

        [Fact]
        public async Task GetAllBrands_WhenNoBrandsExist_ReturnsOkWithEmptyList()
        {
            // Arrange
            var emptyBrands = new List<Brand>();
            var brandRepositoryMock = new Mock<IBrandRepository>();
            var loggerMock = new Mock<ILogger<BrandController>>();
            var mapperMock = new Mock<IMapper>();

            brandRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(emptyBrands);

            var controller = new BrandController(brandRepositoryMock.Object, loggerMock.Object, mapperMock.Object);

            // Act
            var result = await controller.GetAllBrandsAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>(); 
            var okResult = result.As<OkObjectResult>();
            okResult.Value.Should().NotBeNull().And.BeAssignableTo<IEnumerable<BrandDto>>();
            var brandDtos = okResult.Value.As<IEnumerable<BrandDto>>();
            brandDtos.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateBrand_WithValidBrand_ReturnsCreatedAtActionWithBrandDto()
        {
            // Arrange
            var createBrandDto = new CreateBrandDto { BrandName = "New Brand" };
            var brand = new Brand { BrandId = 1, BrandName = createBrandDto.BrandName };
            var brandRepositoryMock = new Mock<IBrandRepository>();
            var loggerMock = new Mock<ILogger<BrandController>>();
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(mapper => mapper.Map<Brand>(createBrandDto)).Returns(brand);
            mapperMock.Setup(mapper => mapper.Map<BrandDto>(brand)).Returns(new BrandDto { BrandId = brand.BrandId, BrandName = brand.BrandName });

            brandRepositoryMock.Setup(repo => repo.CreateAsync(brand)).ReturnsAsync(brand);

            var controller = new BrandController(brandRepositoryMock.Object, loggerMock.Object, mapperMock.Object);

            // Act
            var result = await controller.CreateBrandAsync(createBrandDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = result.As<CreatedAtActionResult>();
            createdResult.ActionName.Should().Be(nameof(BrandController.GetBrandAsync));
            createdResult.Value.Should().NotBeNull().And.BeAssignableTo<BrandDto>();
            var brandDto = createdResult.Value.As<BrandDto>();
            brandDto.BrandId.Should().Be(brand.BrandId);
            brandDto.BrandName.Should().Be(brand.BrandName);
        }

        [Fact]
        public async Task CreateBrand_WithExistingBrandName_ReturnsConflict()
        {
            // Arrange
            var createBrandDto = new CreateBrandDto { BrandName = "Existing Brand" };
            var existingBrand = new Brand { BrandId = 1, BrandName = createBrandDto.BrandName };
            var brandRepositoryMock = new Mock<IBrandRepository>();
            var loggerMock = new Mock<ILogger<BrandController>>();
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(mapper => mapper.Map<Brand>(createBrandDto)).Returns(new Brand { BrandName = createBrandDto.BrandName });

            brandRepositoryMock.Setup(repo => repo.GetByNameAsync(createBrandDto.BrandName)).ReturnsAsync(existingBrand);

            var controller = new BrandController(brandRepositoryMock.Object, loggerMock.Object, mapperMock.Object);

            // Act
            var result = await controller.CreateBrandAsync(createBrandDto);

            // Assert
            result.Should().BeOfType<ConflictObjectResult>(); 
            var conflictResult = result.As<ConflictObjectResult>();
            conflictResult.Value.Should().Be($"Brand with name '{createBrandDto.BrandName}' already exists");
        }

        [Fact]
        public async Task Concurrent_CreateBrand_ShouldNotHaveConcurrencyIssues()
        {
            // Arrange
            var createBrandDto = new CreateBrandDto { BrandName = "New Brand" };
            var brandRepositoryMock = new Mock<IBrandRepository>();
            var loggerMock = new Mock<ILogger<BrandController>>();
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(mapper => mapper.Map<Brand>(createBrandDto)).Returns(new Brand { BrandName = createBrandDto.BrandName });

            brandRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Brand>())).Returns(Task.FromResult(new Brand()));

            var controller = new BrandController(brandRepositoryMock.Object, loggerMock.Object, mapperMock.Object);

            const int concurrentRequests = 5;
            var tasks = new List<Task<IActionResult>>();

            for (int i = 0; i < concurrentRequests; i++)
            {
                tasks.Add(controller.CreateBrandAsync(createBrandDto));
            }

            // Act
            var results = await Task.WhenAll(tasks);

            // Assert
            foreach (var result in results)
            {
                result.Should().BeOfType<CreatedAtActionResult>();
                var createdResult = result.As<CreatedAtActionResult>();
                createdResult.ActionName.Should().Be(nameof(BrandController.GetBrandAsync));
            }
        }

        [Fact]
        public async Task UpdateBrand_WithValidData_ReturnsNoContent()
        {
            // Arrange
            var brandId = 1;
            var updateBrandDto = new UpdateBrandDto { BrandName = "Updated Brand" };
            var existingBrand = new Brand { BrandId = brandId, BrandName = "Old Brand" };

            var brandRepositoryMock = new Mock<IBrandRepository>();
            brandRepositoryMock.Setup(repo => repo.GetAsync(brandId)).ReturnsAsync(existingBrand);

            var loggerMock = new Mock<ILogger<BrandController>>();
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(mapper => mapper.Map(updateBrandDto, existingBrand)).Returns(existingBrand);

            var controller = new BrandController(brandRepositoryMock.Object, loggerMock.Object, mapperMock.Object);

            // Act
            var result = await controller.UpdateBrandAsync(brandId, updateBrandDto);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateBrand_WithNonExistentBrand_ReturnsNotFound()
        {
            // Arrange
            var brandId = 1;
            var updateBrandDto = new UpdateBrandDto { BrandName = "Updated Brand" };

            var brandRepositoryMock = new Mock<IBrandRepository>();
            brandRepositoryMock.Setup(repo => repo.GetAsync(brandId)).ReturnsAsync((Brand)null);

            var loggerMock = new Mock<ILogger<BrandController>>();
            var mapperMock = new Mock<IMapper>();

            var controller = new BrandController(brandRepositoryMock.Object, loggerMock.Object, mapperMock.Object);

            // Act
            var result = await controller.UpdateBrandAsync(brandId, updateBrandDto);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task DeleteBrand_WithBrandNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var brandId = 1;

            var brandRepositoryMock = new Mock<IBrandRepository>();
            brandRepositoryMock.Setup(repo => repo.GetAsync(brandId)).ReturnsAsync((Brand)null);

            var loggerMock = new Mock<ILogger<BrandController>>();
            var mapperMock = new Mock<IMapper>();

            var controller = new BrandController(brandRepositoryMock.Object, loggerMock.Object, mapperMock.Object);

            // Act
            var result = await controller.DeleteBrandAsync(brandId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            brandRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DeleteBrand_WithBrandDeletedSuccessfully_ShouldReturnNoContent()
        {
            // Arrange
            var brandId = 1;
            var existingBrand = new Brand { BrandId = brandId, BrandName = "Old Brand" };

            var brandRepositoryMock = new Mock<IBrandRepository>();
            brandRepositoryMock.Setup(repo => repo.GetAsync(brandId)).ReturnsAsync(existingBrand);

            var loggerMock = new Mock<ILogger<BrandController>>();
            var mapperMock = new Mock<IMapper>();

            var controller = new BrandController(brandRepositoryMock.Object, loggerMock.Object, mapperMock.Object);

            // Act
            var result = await controller.DeleteBrandAsync(brandId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            brandRepositoryMock.Verify(repo => repo.DeleteAsync(brandId), Times.Once);
        }
    }
}