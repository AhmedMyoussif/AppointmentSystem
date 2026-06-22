using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Appointments.Commands.BookAppointment; 


public sealed record BookAppointmentCommand(Guid CustomerId,
                                            Guid ServiceId,
                                            Guid TimeSlotId,
                                            string? Notes) : IRequest<Result<Guid>>;
