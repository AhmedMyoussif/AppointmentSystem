using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.Categories.Queries.GetAllCategories;

public sealed class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<CategoryDto>>>
{
    private readonly IAppDbContext _context;

    public GetAllCategoriesQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken ct)
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Select(c => new CategoryDto(c.Id, c.Name, c.Description))
            .ToListAsync(ct);

        return categories;
    }
}
