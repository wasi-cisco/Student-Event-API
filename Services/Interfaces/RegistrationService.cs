using StudentEvents.Api.DTOs;
using StudentEvents.Api.Models;

namespace StudentEvents.Api.Services
{
    public interface IRegistrationService
    {
        Task<bool> RegisterAsync(RegistrationRequestDto dto);
    }
}
