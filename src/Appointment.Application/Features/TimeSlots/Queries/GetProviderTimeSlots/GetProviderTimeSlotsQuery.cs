using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.TimeSlots.Queries.GetProviderTimeSlots;

public sealed record GetProviderTimeSlotsQuery(Guid ProviderId) 
    : IRequest<Result<List<TimeSlotDto>>>;