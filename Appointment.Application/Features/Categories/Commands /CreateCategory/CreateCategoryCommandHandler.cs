using Appointment.Application.Common.Errors;
using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Category;
using Appointment.Application.Features.Categories.Commands_.CreateCategory;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandler 
    : IRequestHandler<CreateCategoryCommand, Result<Guid>>
{
    private readonly IAppDbContext _context;

    public CreateCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken ct)
    {
        
        var isNameUnique = await _context.Categories
            .AllAsync(c => c.Name.ToLower() != request.Name.ToLower(), ct);

        if (!isNameUnique)
        {
            return ApplicationErrors.CategoryNameAlreadyExists;
        }

        
        var categoryResult = Domain.Category.Category.Create(
            Guid.NewGuid(),
            request.Name,
            request.Description ?? string.Empty
        );

        if (categoryResult.IsError)
        {
            return categoryResult.Errors;
        }

       
        var category = categoryResult.Value;

        _context.Categories.Add(category);
        await _context.SaveChangesAsync(ct);

        return category.Id;
    }
}