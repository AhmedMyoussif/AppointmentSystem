using Appointment.Application.Common.Errors;
using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Category;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.Categories.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<Success>>
{
    private readonly IAppDbContext _context;

    public UpdateCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Success>> Handle(UpdateCategoryCommand request, CancellationToken ct)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == request.Id, ct);

        if (category is null)
        {
            return ApplicationErrors.CategoryNotFound;
        }

        var isNameDuplicate = await _context.Categories
            .AnyAsync(c => c.Id != request.Id && c.Name.ToLower() == request.Name.ToLower(), ct);

        if (isNameDuplicate)
        {
            return ApplicationErrors.CategoryNameAlreadyExists;
        }

        category.Update(request.Name, request.Description ?? string.Empty);
        
        await _context.SaveChangesAsync(ct);
        return new Success();
    }
}