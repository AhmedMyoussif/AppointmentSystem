using Appointment.Domain.Common;
using Appointment.Domain.Common.Results;
using Appointment.Domain.Services;

namespace Appointment.Domain.Category; 

public class Category : AuditableEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; } = string.Empty;
    
    private readonly List<ServiceCategory> _serviceCategories = new();
    public IReadOnlyCollection<ServiceCategory> ServiceCategories => _serviceCategories.AsReadOnly();

    private Category()
    {}

    private Category(Guid id, string name, string description) : base(id)
    {
        
        Name = name;
        Description = description;
    }

    public static Result<Category> Create(Guid id, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return CategoryErrors.NameCannotBeEmpty;
    
        var category = new Category(id, name.Trim(), description);
        return category;
    }

    public void Update(string name, string? description)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            Name = name.Trim();
        }

        if (description is not null)
        {
            Description = description;
        }
    }
}