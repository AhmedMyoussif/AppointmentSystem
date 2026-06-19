using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.TimeSlots.Commands.CreateTimeSlot;

public sealed record CreateTimeSlotCommand(Guid ProviderId,
                                           DateTime StartTime,
                                           DateTime EndTime) : IRequest<Result<Guid>>;