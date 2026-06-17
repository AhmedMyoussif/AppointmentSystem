namespace Appointment.Application.Features.Categories.Queries;

public sealed record CategoryDto(Guid Id,
                                 string Name,
                                 string Description);