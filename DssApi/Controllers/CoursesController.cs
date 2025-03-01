using Microsoft.AspNetCore.Mvc;

namespace DssApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseServiceClient _courseServiceClient;

        public CoursesController(ICourseServiceClient courseServiceClient)
        {
            _courseServiceClient = courseServiceClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var courses = await _courseServiceClient.GetCoursesAsync();
            return Ok(courses);
        }
    }
}
