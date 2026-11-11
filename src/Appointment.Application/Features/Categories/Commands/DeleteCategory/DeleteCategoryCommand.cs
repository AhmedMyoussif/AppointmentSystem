using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Categories.Commands.DeleteCategory;
public sealed record DeleteCategoryCommand(Guid Id) : IRequest<Result<Deleted>>;