using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Categories.Commands_.CreateCategory; 

public sealed record CreateCategoryCommand(string Name, string Description) : IRequest<Result<Guid>>;