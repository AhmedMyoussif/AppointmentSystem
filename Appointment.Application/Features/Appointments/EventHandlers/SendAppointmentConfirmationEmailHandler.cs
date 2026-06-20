using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Appointments.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Appointment.Application.Features.Appointments.EventHandlers;

public sealed class SendAppointmentConfirmationEmailHandler(
    INotificationService notificationService, // الإنترفيس المسؤول عن الرسايل عندك
    IAppDbContext context,
    ILogger<SendAppointmentConfirmationEmailHandler> logger)
    : INotificationHandler<AppointmentCreated>
{
    private readonly INotificationService _notificationService = notificationService;
    private readonly IAppDbContext _context = context;
    private readonly ILogger<SendAppointmentConfirmationEmailHandler> _logger = logger;

    public async Task Handle(AppointmentCreated notification, CancellationToken ct)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Customer)
            .Include(a => a.Service)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == notification.AppointmentId, ct);

        if (appointment is null)
        {
            _logger.LogError("Appointment with Id '{AppointmentId}' does not exist.", notification.AppointmentId);
            return;
        }

        _logger.LogInformation("Sending confirmation notifications for Appointment {Id}", appointment.Id);

        if (appointment.Customer is not null)
        {
            await _notificationService.SendEmailAsync(appointment.Customer.Email, ct);
            await _notificationService.SendSmsAsync(appointment.Customer.PhoneNumber, ct);
        }
    }
}