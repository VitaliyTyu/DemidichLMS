// IntegrationTests/CoursesControllerIntegrationTests.cs
using AutoDealershipLMS.Controllers;
using AutoDealershipLMS.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Persistence;

using System;
using System.Threading.Tasks;

using Xunit;

public class CoursesControllerIntegrationTests
{
    private readonly DataContext _context;
    private readonly CoursesController _controller;

    public CoursesControllerIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        _context = new DataContext(options);
        _controller = new CoursesController(_context);
    }

    [Fact]
    public async Task CreateCourse_ReturnsOkResult()
    {
        // Arrange
        var course = new Course { Id = 1, Title = "Course 1", Description = "Description 1" };

        // Act
        var result = await _controller.CreateCourse(course);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnCourse = Assert.IsType<Course>(okResult.Value);
        Assert.Equal("Course 1", returnCourse.Title);
    }

    [Fact]
    public async Task DeleteCourse_ReturnsOkResult()
    {
        // Arrange
        var course = new Course { Id = 1, Title = "Course 1", Description = "Description 1" };
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.DeleteCourse(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Course deleted successfully", okResult.Value.GetType().GetProperty("Message").GetValue(okResult.Value));
    }
}