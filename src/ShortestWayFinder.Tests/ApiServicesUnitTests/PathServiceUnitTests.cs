using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Moq;
using ShortestWayFinder.Domain.DatabaseModels;
using ShortestWayFinder.Domain.Infrastructure.Contracts;
using ShortestWayFinder.Web.Configuration;
using ShortestWayFinder.Web.Exceptions;
using ShortestWayFinder.Web.Models;
using ShortestWayFinder.Web.Services;
using Xunit;

namespace ShortestWayFinder.Tests.ApiServicesUnitTests
{
    public class PathServiceUnitTests
    {
        [Fact]
        public async Task GetAllPoints_ReturnsListOtPointsDto_AfterMappingModelToDto()
        {
            // Arrange
            var mockRepo = new Mock<IPathRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(GetPathList()));
            var service = new PathService(mockRepo.Object);
            AutomapperConfiguration.Load();

            // Act
            var result = await service.GetPointsAsync();

            // Assert
            var list = Assert.IsType<List<PointDto>>(result); ;
            Assert.Equal(5, list.Count);
        }
        [Fact]
        public async Task GetAllPath_ReturnListOfPathDto_AfterMappingModelToDto()
        {
            // Arrange
            var mockRepo = new Mock<IPathRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(GetPathList()));
            var service = new PathService(mockRepo.Object);
            AutomapperConfiguration.Load();

            // Act
            var result = await service.GetAllExistedPathsAsync();

            // Assert
            var list = Assert.IsType<List<PathDto>>(result); ;
            Assert.Equal(4, list.Count);
        }
        [Fact]
        public async Task AddPath_ReturnTrue_AfterSuccessfulOperation()
        {
            // Arrange
            var mockRepo = new Mock<IPathRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(GetPathList()));
            var service = new PathService(mockRepo.Object);
            AutomapperConfiguration.Load();

            var objectToAdd = new PathDto() { FirstPoint = "First point", SecondPoint = "Second point", Time = 55 };
            var resultAfterAdd = await service.CreatePathAsync(objectToAdd);

            Assert.Equal(true, resultAfterAdd);
        }
        [Fact]
        public async Task AddPath_ReturnException_AfterRequestWithNegativeOrZetoTime()
        {
            // Arrange
            var mockRepo = new Mock<IPathRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(GetPathList()));
            var service = new PathService(mockRepo.Object);
            AutomapperConfiguration.Load();

            var objectToAdd = new PathDto() { FirstPoint = "Sagrada Familia", SecondPoint = "La Pedrera", Time = 0 };

            var aggregateException = Assert.Throws<AggregateException>(() => service.CreatePathAsync(objectToAdd).Wait());
            var timeIsNotPositiveException = aggregateException.InnerExceptions.
                FirstOrDefault(x => x.GetType() == typeof(TimeIsNotPositiveException)) as TimeIsNotPositiveException;
            
            Assert.NotNull(timeIsNotPositiveException);
            Assert.Equal("Time must be a positive number!", timeIsNotPositiveException.Message);
        }
        private IEnumerable<Path> GetPathList()
        {
            List<Path> paths = new List<Path>
            {
                new Path {FirstPoint = "Sagrada Familia", SecondPoint = "Las Ramblas", EstimatingTime = 40},
                new Path {FirstPoint = "Sagrada Familia", SecondPoint = "La Pedrera", EstimatingTime = 60},
                new Path {FirstPoint = "Sagrada Familia", SecondPoint = "Picasso Museum", EstimatingTime = 25},
                new Path {FirstPoint = "Las Ramblas", SecondPoint = "Gothic Quarter", EstimatingTime = 12}
            };
            return paths;
        }
    }
}
