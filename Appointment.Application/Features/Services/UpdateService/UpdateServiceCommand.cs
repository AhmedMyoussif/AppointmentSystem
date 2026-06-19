using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Services.Commands.UpdateService;

public sealed record UpdateServiceCommand(Guid Id,
                                          string Name,
                                          string? Description,
                                          decimal Price) : IRequest<Result<Updated>>;