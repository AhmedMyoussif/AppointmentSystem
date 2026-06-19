using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.TimeSlots.Commands.DeleteTimeSlot;

public sealed record DeleteTimeSlotCommand(Guid Id) : IRequest<Result<Deleted>>;