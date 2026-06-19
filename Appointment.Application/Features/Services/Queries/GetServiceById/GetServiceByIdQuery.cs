using Appointment.Application.Features.Services.CreateService;
using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Services.Queries.GetServiceById;

public sealed record GetServiceByIdQuery(Guid Id) : IRequest<Result<ServiceDto>>;