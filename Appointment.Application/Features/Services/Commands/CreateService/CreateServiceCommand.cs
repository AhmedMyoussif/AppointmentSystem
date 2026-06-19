using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Services.CreateService;

public sealed record CreateServiceCommand(string Name,
                                          string? Description,
                                          decimal Price,
                                          Guid ProviderId,
                                          List<Guid> CategoryIds) : IRequest<Result<Guid>>;
