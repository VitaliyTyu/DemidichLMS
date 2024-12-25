using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Courses.Any()) return;

            var courses = new List<Course>
            {
                new Course
                {
                    Title = "Past Course 1",
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    UpdatedAt = DateTime.UtcNow.AddMonths(-1),
                    Description = "Course 2 months ago",
                    Content = "This is the content for course 1",
                },
                new Course
                {
                    Title = "Past Course 2",
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    UpdatedAt = DateTime.UtcNow.AddMonths(-1),
                    Description = "Course 2 months ago",
                    Content = "This is the content for course 2",
                },
                new Course
                {
                    Title = "Past Course 3",
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    UpdatedAt = DateTime.UtcNow.AddMonths(-1),
                    Description = "Course 2 months ago",
                    Content = "This is the content for course 3",
                },
                new Course
                {
                    Title = "Past Course 4",
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    UpdatedAt = DateTime.UtcNow.AddMonths(-1),
                    Description = "Course 2 months ago",
                    Content = "This is the content for course 4",
                },
                new Course
                {
                    Title = "Past Course 5",
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    UpdatedAt = DateTime.UtcNow.AddMonths(-1),
                    Description = "Course 2 months ago",
                    Content = "This is the content for course 5",
                },
                new Course
                {
                    Title = "Past Course 6",
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    UpdatedAt = DateTime.UtcNow.AddMonths(-1),
                    Description = "Course 2 months ago",
                    Content = "This is the content for course 6",
                },
                new Course
                {
                    Title = "Past Course 7",
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    UpdatedAt = DateTime.UtcNow.AddMonths(-1),
                    Description = "Course 2 months ago",
                    Content = "This is the content for course 7",
                },
                new Course
                {
                    Title = "Past Course 8",
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    UpdatedAt = DateTime.UtcNow.AddMonths(-1),
                    Description = "Course 2 months ago",
                    Content = "This is the content for course 8",
                },
                new Course
                {
                    Title = "Past Course 9",
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    UpdatedAt = DateTime.UtcNow.AddMonths(-1),
                    Description = "Course 2 months ago",
                    Content = "This is the content for course 9",
                },
                new Course
                {
                    Title = "Past Course 10",
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    UpdatedAt = DateTime.UtcNow.AddMonths(-1),
                    Description = "Course 2 months ago",
                    Content = "This is the content for course 10",
                },
            };

            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }
    }
}