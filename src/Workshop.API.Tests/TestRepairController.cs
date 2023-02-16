using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Moq;
using System.Security.Claims;
using Workshop.API.Controllers;
using Workshop.API.Dtos;
using Workshop.API.Models;
using Workshop.API.Services;

namespace Workshop.API.Tests
{
    public class TestRepairController
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectData()
        {
            // Arrange
            var userId = "94aebb64-a48a-4440-abdc-0a5eec477f30";
            var repairs = new List<Repair> { new Repair { ... }, new Repair { ... } };
            var mockRepository = new Mock<IRepairRepository>();
            mockRepository.Setup(repo => repo.GetUserRepairs(userId)).Returns(repairs);

            var mockTokenService = new Mock<ITokenService>();
            mockTokenService.Setup(service => service.GetClaimsPrincipalFromToken(It.IsAny<string>())).Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
        new Claim(ClaimTypes.NameIdentifier, userId)
            }, "mock")));

            var controller = new RepairController(mockRepository.Object, mockTokenService.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Authorization, "Bearer test-token");

            // Act
            var result = await controller.GetAllAsync();

            // Assert
            var okObjectResult = result as OkObjectResult;
            var returnedRepairs = okObjectResult.Value as IEnumerable<RepairDto>;
            Assert.Equal(repairs.Select(r => r.AsDto()), returnedRepairs);
        }
    }
}