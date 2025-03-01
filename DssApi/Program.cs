using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Регистрируем HttpClient для вызова CoursesService.
// Имя хоста "courseservice" соответствует имени сервиса в docker-compose.
builder.Services.AddHttpClient<ICourseServiceClient, CourseServiceClient>(client =>
{
    client.BaseAddress = new Uri("http://courseservice:5000/");
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();


// Интерфейс для обращения к CoursesService
public interface ICourseServiceClient
{
    Task<IEnumerable<CourseDto>> GetCoursesAsync();
}

public class CourseServiceClient : ICourseServiceClient
{
    private readonly HttpClient _httpClient;

    public CourseServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<CourseDto>> GetCoursesAsync()
    {
        var response = await _httpClient.GetAsync("api/courses");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<CourseDto>>() 
               ?? Enumerable.Empty<CourseDto>();
    }
}

// DTO для курса
public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
