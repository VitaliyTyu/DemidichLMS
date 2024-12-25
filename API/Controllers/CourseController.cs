using Application.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CoursesController : BaseApiController
    {
        private readonly ICoursesService _coursesService;
        public CoursesController(ICoursesService coursesService)
        {
            _coursesService = coursesService;

        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> GetCourses()
        {
            return await _coursesService.GetCourses();
        }
    }
}


