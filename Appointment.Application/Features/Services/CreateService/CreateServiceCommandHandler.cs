using Appointment.Application.Common.Errors;
using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using Appointment.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Application.Features.Services.CreateService;



public sealed class CreateServiceCommandHandler
    : IRequestHandler<CreateServiceCommand, Result<Guid>>
{
   private readonly IAppDbContext _context;
    public CreateServiceCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateServiceCommand request, CancellationToken ct)
    {
       var ProviderId = await _context.Users
       .AnyAsync(u=>u.Id == request.ProviderId, ct);

        if (!ProviderId)
        {
            return ApplicationErrors.ProviderNotFound;
        }

       
       var serviceResult = Service.Create(
            Guid.NewGuid(),
            request.Name,
            request.Description ?? string.Empty,
            request.Price,
            request.ProviderId
        );
        
         if (serviceResult.IsError)
        {
            return serviceResult.Errors;
        }

        var service = serviceResult.Value;

        foreach (var categoryId in request.CategoryIds)
        {
           
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == categoryId, ct);
            if (categoryExists)
            {
                service.AddCategory(categoryId);
            }
        }

        _context.Services.Add(service);
        await _context.SaveChangesAsync(ct);

        return service.Id;
    }
}