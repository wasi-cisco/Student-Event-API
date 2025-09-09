using Microsoft.AspNetCore.Mvc;
using StudentEvents.Api.DTOs;
using StudentEvents.Api.Services;

namespace StudentEvents.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationsController(IRegistrationService svc) : ControllerBase
    {
        // POST /api/registrations
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var ok = await svc.RegisterAsync(dto);
            if (!ok) return BadRequest("Invalid registration or duplicate/not upcoming.");
            return Ok(new { message = "Registered." });
        }
    }
}
