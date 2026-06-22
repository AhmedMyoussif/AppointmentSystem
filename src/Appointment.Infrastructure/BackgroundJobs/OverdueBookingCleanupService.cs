using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Appointment.Application.Common.Interfaces;
using Appointment.Domain;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Infrastructure.BackgroundJobs;

public class OverdueBookingCleanupService(
    IServiceScopeFactory scopeFactory,
    ILogger<OverdueBookingCleanupService> logger,
    TimeProvider dateTime) : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly ILogger<OverdueBookingCleanupService> _logger = logger;
    private readonly TimeProvider _dateTime = dateTime;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(5));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            _logger.LogInformation("Starting overdue booking cleanup at {Time}", _dateTime.GetUtcNow());
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
                var cutoff = _dateTime.GetUtcNow().AddMinutes(-15);

                var overdueAppointments = await db.Appointments
                    .Where(a => a.Status == AppointmentStatus.Pending && a.CreatedAtUtc <= cutoff)
                    .ToListAsync(stoppingToken);

                if (overdueAppointments.Count > 0)
                {
                    foreach (var appointment in overdueAppointments)
                    {
                        
                        appointment.Cancel();

                        
                        var timeSlot = await db.TimeSlots
                            .FirstOrDefaultAsync(ts => ts.Id == appointment.TimeSlotId, stoppingToken);
                        
                        if (timeSlot is not null)
                        {
                            timeSlot.Release(); 
                        }

                    
                        _logger.LogInformation("Cancelled overdue appointment {AppointmentId} created at {CreatedAt}", appointment.Id, appointment.CreatedAtUtc);
                    }

                    await db.SaveChangesAsync(stoppingToken);

                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred during overdue booking cleanup");
            }
        }
    }
}