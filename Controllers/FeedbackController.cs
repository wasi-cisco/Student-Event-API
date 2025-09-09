using Microsoft.AspNetCore.Mvc;
using StudentEvents.Api.DTOs;
using StudentEvents.Api.Services;

namespace StudentEvents.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController(IFeedbackService svc) : ControllerBase
    {
        // POST /api/feedback
        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] FeedbackCreateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var (ok, message) = await svc.SubmitAsync(dto);
            return ok ? Ok(new { message }) : BadRequest(new { message });
        }
    }
}
