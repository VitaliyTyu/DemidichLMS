using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using Xunit;

public class CoursesControllerUnitTests
{
    private readonly Mock<DataContext> _mockContext;
    private readonly CoursesController _controller;

    public CoursesControllerUnitTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        _mockContext = new Mock<DataContext>(options);
        _controller = new CoursesController(_mockContext.Object);
    }

    [Fact]
    public async Task GetAllCourses_ReturnsOkResult()
    {
        // Arrange
        var mockCourses = new List<Course>
        {
            new Course { Id = 1, Title = "Course 1", Description = "Description 1" },
            new Course { Id = 2, Title = "Course 2", Description = "Description 2" }
        }.AsQueryable();

        var mockDbSet = new Mock<DbSet<Course>>();
        mockDbSet.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(mockCourses.Provider);
        mockDbSet.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(mockCourses.Expression);
        mockDbSet.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(mockCourses.ElementType);
        mockDbSet.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(mockCourses.GetEnumerator());

        _mockContext.Setup(c => c.Courses).Returns(mockDbSet.Object);

        // Act
        var result = await _controller.GetAllCourses();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnCourses = Assert.IsType<List<Course>>(okResult.Value);
        Assert.Equal(2, returnCourses.Count);
    }

    [Fact]
    public async Task GetCourseById_ReturnsNotFound_WhenCourseDoesNotExist()
    {
        // Arrange
        var mockCourses = new List<Course>().AsQueryable();

        var mockDbSet = new Mock<DbSet<Course>>();
        mockDbSet.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(mockCourses.Provider);
        mockDbSet.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(mockCourses.Expression);
        mockDbSet.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(mockCourses.ElementType);
        mockDbSet.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(mockCourses.GetEnumerator());

        _mockContext.Setup(c => c.Courses).Returns(mockDbSet.Object);

        // Act
        var result = await _controller.GetCourseById(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}