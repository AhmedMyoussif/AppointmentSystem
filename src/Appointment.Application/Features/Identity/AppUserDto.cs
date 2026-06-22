namespace Appointment.Application.Features.Identity;
using System.Security.Claims;

public sealed record AppUserDto(string UserId, string Email, IList<string> Roles, IList<Claim> Claims);