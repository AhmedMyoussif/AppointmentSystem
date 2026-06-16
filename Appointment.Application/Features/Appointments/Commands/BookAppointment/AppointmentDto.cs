namespace Appointment.Application.Features.Appointments.Commands.BookAppointment;

public sealed record AppointmentDto(Guid Id,
                                    Guid CustomerId,
                                    string CustomerName,
                                    Guid ServiceId,
                                    string ServiceName,
                                    DateTime AppointmentDate,
                                    TimeSpan StartTime,
                                    TimeSpan EndTime,
                                    string Status,
                                    decimal PriceAtBooking,
                                    string Notes);