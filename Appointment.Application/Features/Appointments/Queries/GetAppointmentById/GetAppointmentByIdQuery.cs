
using Appointment.Application.Features.Appointments.Queries;
using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Appointments.Queries.GetAppointmentById;

public sealed record GetAppointmentByIdQuery(Guid AppointmentId) : IRequest<Result<AppointmentDto>>;
