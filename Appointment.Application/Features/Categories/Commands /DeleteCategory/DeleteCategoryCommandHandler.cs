using Appointment.Application.Common.Errors;
using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.Categories.Commands.DeleteCategory;

public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<Deleted>>
{
    private readonly IAppDbContext _context;

    public DeleteCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Deleted>> Handle(DeleteCategoryCommand request, CancellationToken ct)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == request.Id, ct);

        if (category is null)
        {
            return ApplicationErrors.CategoryNotFound;
        }

        
        var hasServices = await _context.Services
            .AnyAsync(s => s.Id == request.Id, ct);

        if (hasServices)
        {
            return ApplicationErrors.CategoryCannotBeDeletedWithActiveServices;
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(ct);

        return new Deleted();
    }
}