using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Appointments.Queries;

public sealed record GetCustomerAppoinmentsQuery(Guid CustomerId) : IRequest<Result<List<AppointmentDto>>>;