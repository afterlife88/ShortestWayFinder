using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShortestWayFinder.Web.Contracts;
using ShortestWayFinder.Web.Controllers;
using ShortestWayFinder.Web.Models;
using Xunit;

namespace ShortestWayFinder.Tests.ControllersUnitTests
{
    public class PathControllerTests
    {
        [Fact]
        public async Task GetPoints_ReturnsArrayOfPoints_WhenRequested()
        {
            // Arrange
            var mockService = new Mock<IPathService>();
            var controller = new PathController(mockService.Object);
            // Act
            var result = await controller.GetAllPoints();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<PointDto>>(okResult.Value);
        }
        [Fact]
        public async Task GetAllPath_ReturnsArrayOfPath_WhenRequested()
        {
            // Arrange
            var mockService = new Mock<IPathService>();
            var controller = new PathController(mockService.Object);
            // Act
            var result = await controller.GetAll();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<PathDto>>(okResult.Value);
        }
        [Fact]
        public async Task AddPath_ReturnsBadRequest_WhenFirstPointNameEqualToSecondPointName()
        {
            // Arrange
            var mockService = new Mock<IPathService>();
            var controller = new PathController(mockService.Object);

            var request = new PathDto() { SecondPoint = "some value", FirstPoint = "some value", Time = 2 };

            var result = await controller.AddPath(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
