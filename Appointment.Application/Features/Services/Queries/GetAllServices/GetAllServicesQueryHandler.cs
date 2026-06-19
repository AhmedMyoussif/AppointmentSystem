using Appointment.Application.Common.Interfaces;
using Appointment.Application.Features.Services.CreateService;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.Services.Queries.GetAllServices;

public sealed class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, Result<List<ServiceDto>>>
{
    private readonly IAppDbContext _context;

    public GetAllServicesQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<ServiceDto>>> Handle(GetAllServicesQuery request, CancellationToken ct)
    {
        var services = await _context.Services
            .AsNoTracking()
            .OrderBy(s => s.Name)
            .Select(s => new ServiceDto(
                s.Id,
                s.Name,
                s.Description,
                s.Price,
                s.ProviderId,
                _context.Users.Where(u => u.Id == s.ProviderId).Select(u => u.FirstName).FirstOrDefault() ?? "Unknown Provider",
                s.ServiceCategories.Select(sc => sc.Category.Name).ToList()
            ))
            .ToListAsync(ct);

        return services;
    }
}