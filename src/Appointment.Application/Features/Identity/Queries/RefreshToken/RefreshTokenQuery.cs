using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Identity.Queries.RefreshToken;

public record RefreshTokenQuery(string RefreshToken , string ExpiredAccessToken) : IRequest<Result<TokenResponse>>;