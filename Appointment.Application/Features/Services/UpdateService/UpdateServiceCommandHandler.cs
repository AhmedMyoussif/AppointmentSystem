using Appointment.Application.Common.Errors;
using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.Services.Commands.UpdateService;

public sealed class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, Result<Updated>>
{
    private readonly IAppDbContext _context;

    public UpdateServiceCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Updated>> Handle(UpdateServiceCommand request, CancellationToken ct)
    {
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Id == request.Id, ct);

        if (service is null)
        {
            return ApplicationErrors.ServiceNotFound; 
        }

        service.Update(request.Name, request.Description ?? string.Empty, request.Price);

        await _context.SaveChangesAsync(ct);

        return Result.Updated;
    }
}