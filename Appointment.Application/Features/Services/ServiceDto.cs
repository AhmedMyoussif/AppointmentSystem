namespace Appointment.Application.Features.Services.CreateService;

public sealed record ServiceDto(Guid Id,
                                string Name,
                                string? Description,
                                decimal Price,
                                Guid ProviderId,
                                string ProviderName,
                                List<string> Categories);