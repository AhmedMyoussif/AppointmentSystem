using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Appointments.Commands.CancelAppointment;

public sealed record CancelAppointmentCommand(Guid AppointmentId,
                                              string? CancellationReason) : IRequest<Result<Success>>;