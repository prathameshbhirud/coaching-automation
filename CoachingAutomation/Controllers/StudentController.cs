using Microsoft.AspNetCore.Mvc;
using CoachingAutomation.Jobs;

namespace CoachingAutomation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly NotificationJob _job;

    public StudentController(NotificationJob job)
    {
        _job = job;
    }

    [HttpGet("run")]
    public async Task<IActionResult> RunJob()
    {
        await _job.Run();
        return Ok("Notifications sent!");
    }
}