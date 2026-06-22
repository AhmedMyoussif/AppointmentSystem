using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.TimeSlots.Queries.GetAvailableTimeSlots;

public sealed record GetAvailableTimeSlotsQuery(Guid ProviderId) 
    : IRequest<Result<List<TimeSlotDto>>>;