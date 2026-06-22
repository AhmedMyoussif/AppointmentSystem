using Appointment.Application.Common.Errors;
using Appointment.Application.Common.Interfaces;
using Appointment.Application.Features.Services.CreateService;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.Services.Queries.GetServiceById;

public sealed class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, Result<ServiceDto>>
{
    private readonly IAppDbContext _context;

    public GetServiceByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ServiceDto>> Handle(GetServiceByIdQuery request, CancellationToken ct)
    {
        var serviceDto = await _context.Services
            .AsNoTracking()
            .Where(s => s.Id == request.Id)
            .Select(s => new ServiceDto(
                s.Id,
                s.Name,
                s.Description,
                s.Price,
                s.ProviderId,
                _context.Users.Where(u => u.Id == s.ProviderId).Select(u => u.FirstName).FirstOrDefault() ?? "Unknown Provider",
                s.ServiceCategories.Select(sc => sc.Category.Name).ToList() 
            ))
            .FirstOrDefaultAsync(ct);

        if (serviceDto is null)
        {
            return ApplicationErrors.ServiceNotFound;
        }

        return serviceDto;
    }
}