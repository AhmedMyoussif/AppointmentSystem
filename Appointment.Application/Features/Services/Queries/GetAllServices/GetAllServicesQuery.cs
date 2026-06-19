using Appointment.Application.Features.Services.CreateService;
using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Services.Queries.GetAllServices;

public sealed record GetAllServicesQuery() : IRequest<Result<List<ServiceDto>>>;