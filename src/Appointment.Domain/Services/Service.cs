using Appointment.Domain.Common;
using Appointment.Domain.Common.Results;
using Appointment.Domain.Users;

namespace Appointment.Domain.Services; 

public class Service : AuditableEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    public Guid ProviderId { get; private set; }
    public User Provider { get; private set; }

    private readonly List<ServiceCategory> _serviceCategories = new();
    public IReadOnlyCollection<ServiceCategory> ServiceCategories => _serviceCategories.AsReadOnly();

    private Service()
    {}
    private Service(Guid id, string name, string description, decimal price, Guid providerId) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        ProviderId = providerId;
       
    }
    public static Result<Service> Create(Guid id, string name, string description, decimal price, Guid providerId)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ServiceErrors.NameCannotBeEmpty;

        if (price < 0)
            return ServiceErrors.PriceCannotBeNegative;

        if (providerId == Guid.Empty)
            return ServiceErrors.ProviderIdCannotBeEmpty;

        

        var service = new Service(id, name.Trim(), description, price, providerId);
        return service;
    }

    public void AddCategory(Guid categoryId)
    {
        if (categoryId == Guid.Empty) return;

        
        if (!_serviceCategories.Any(sc => sc.CategoryId == categoryId))
        {
            _serviceCategories.Add(new ServiceCategory(this.Id, categoryId));
        }
    }
    public void Update(string name, string description, decimal price)
    {
        if (!string.IsNullOrWhiteSpace(name)) Name = name.Trim();
        if (price >= 0) Price = price;
        Description = description;
    }

}