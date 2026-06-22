using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Appointments.Queries;

public sealed record GetCustomerAppointmentsQuery(Guid CustomerId) : ICachedQuery<Result<List<AppointmentDto>>>
{
    public string CacheKey => $"GetCustomerAppointments:{CustomerId}";

    public string[] Tags => new[] { $"CustomerAppointments:{CustomerId}" };

    public TimeSpan Expiration => TimeSpan.FromMinutes(10);
}