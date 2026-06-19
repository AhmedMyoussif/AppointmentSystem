using Appointment.Application.Common.Errors;
using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.Services.Commands.DeleteService;

public sealed class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, Result<Deleted>>
{
    private readonly IAppDbContext _context;

    public DeleteServiceCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Deleted>> Handle(DeleteServiceCommand request, CancellationToken ct)
    {
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Id == request.Id, ct);

        if (service is null)
        {
            return ApplicationErrors.ServiceNotFound;
        }

        var hasActiveAppointments = await _context.Appointments
            .AnyAsync(a => a.ServiceId == request.Id, ct);

        if (hasActiveAppointments)
        {
            return ApplicationErrors.ServiceCannotBeDeletedWithActiveAppointments;
        }

        _context.Services.Remove(service);
        await _context.SaveChangesAsync(ct);

        return Result.Deleted;
    }
}