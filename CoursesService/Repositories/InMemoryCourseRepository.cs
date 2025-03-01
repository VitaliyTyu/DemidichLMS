using CoursesService.Models;
using System.Collections.Concurrent;

namespace CoursesService.Repositories
{
    public class InMemoryCourseRepository : ICourseRepository
    {
        private readonly ConcurrentDictionary<int, Course> _courses = new();
        private int _nextId = 1;

        public Task AddCourseAsync(Course course)
        {
            course.Id = _nextId++;
            _courses[course.Id] = course;
            return Task.CompletedTask;
        }

        public Task DeleteCourseAsync(int id)
        {
            _courses.TryRemove(id, out _);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return Task.FromResult(_courses.Values.AsEnumerable());
        }

        public Task<Course?> GetCourseByIdAsync(int id)
        {
            _courses.TryGetValue(id, out var course);
            return Task.FromResult(course);
        }

        public Task UpdateCourseAsync(Course course)
        {
            _courses[course.Id] = course;
            return Task.CompletedTask;
        }
    }
}
