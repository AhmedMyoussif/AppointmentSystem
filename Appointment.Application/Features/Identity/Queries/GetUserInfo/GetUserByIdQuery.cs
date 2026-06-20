namespace Appointment.Application.Features.Identity.Queries.GetUserInfo;
using Appointment.Domain.Common.Results;
using MediatR;

public record GetUserByIdQuery(string? UserId) : IRequest<Result<AppUserDto>>;