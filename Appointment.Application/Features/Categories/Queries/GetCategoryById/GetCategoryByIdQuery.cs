using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Categories.Queries.GetCategoryById;


public sealed record GetCategoryByIdQuery(Guid Id) : IRequest<Result<CategoryDto>>;