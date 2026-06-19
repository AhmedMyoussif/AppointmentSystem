using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Services.Commands.DeleteService;

public sealed record DeleteServiceCommand(Guid Id) : IRequest<Result<Deleted>>;