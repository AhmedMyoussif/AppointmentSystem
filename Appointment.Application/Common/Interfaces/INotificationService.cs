namespace Appointment.Application.Common.Interfaces;

public interface INotificationService
{
    Task SendEmailAsync(string to, CancellationToken ct = default);
    Task SendSmsAsync(string to, CancellationToken ct = default);

}