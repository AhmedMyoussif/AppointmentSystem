using System.Security.Claims;
using Appointment.Application.Features.Identity;
using Appointment.Domain.Common.Results;

namespace Appointment.Application.Common.Interfaces;

public interface ITokenProvider
{
    Task<Result<TokenResponse>> GenerateJwtTokenAsync(AppUserDto user, CancellationToken ct = default);

    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
