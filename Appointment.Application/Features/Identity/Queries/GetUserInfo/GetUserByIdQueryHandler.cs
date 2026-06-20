using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Common.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Appointment.Application.Features.Identity.Queries.GetUserInfo; 

public class GetUserByIdQueryHandler(IIdentityService identityService,ILogger<GetUserByIdQueryHandler> logger) 
    : IRequestHandler<GetUserByIdQuery, Result<AppUserDto>>
{
    private readonly IIdentityService _identityService = identityService;
    private readonly ILogger<GetUserByIdQueryHandler> logger = logger;

    public async Task<Result<AppUserDto>> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        logger.LogInformation("Fetching user by ID: {UserId}", request.UserId);
        var getUserResult = await _identityService.GetUserByIdAsync(request.UserId ?? string.Empty);

        if (getUserResult.IsError)
        {
            logger.LogError("Error occurred while fetching user by ID: {UserId}", request.UserId);
            return getUserResult.Errors;
        }

        return getUserResult.Value;
    }
}