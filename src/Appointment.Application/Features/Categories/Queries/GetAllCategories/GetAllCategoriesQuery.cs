using Appointment.Domain.Common.Results;
using MediatR;

namespace Appointment.Application.Features.Categories.Queries.GetAllCategories;

public sealed record GetAllCategoriesQuery() : IRequest<Result<List<CategoryDto>>>;