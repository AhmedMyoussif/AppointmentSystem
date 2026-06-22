using Appointment.Domain.Appointments.Events;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace Appointment.Application.Features.Appointments.EventHandlers;

public sealed class AppointmentCacheInvalidationEventHandler(
    HybridCache cache, 
    ILogger<AppointmentCacheInvalidationEventHandler> logger)
    : INotificationHandler<AppointmentCreated>
{
    private readonly HybridCache _cache = cache;
    private readonly ILogger<AppointmentCacheInvalidationEventHandler> _logger = logger;

    public async Task Handle(AppointmentCreated notification, CancellationToken ct)
    {
        _logger.LogInformation("[CACHE] Evicting appointments cache due to new appointment creation.");
        
        await _cache.RemoveByTagAsync("appointments", ct);
    }
}