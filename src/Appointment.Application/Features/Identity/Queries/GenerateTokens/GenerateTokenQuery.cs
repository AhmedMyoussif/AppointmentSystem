namespace Appointment.Application.Features.Identity.Queries.GenerateTokens;

using Appointment.Domain.Common.Results;
using MediatR;

public record GenerateTokenQuery(
    string Email,
    string Password) : IRequest<Result<TokenResponse>>;