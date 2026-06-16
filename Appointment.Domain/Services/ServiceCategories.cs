using Appointment.Domain.Common;

namespace Appointment.Domain.Services;

public class ServiceCategory
{
    public Guid ServiceId { get; private set; }
    public Service Service { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category.Category Category { get; private set; }


    private ServiceCategory() { }


    public ServiceCategory(Guid serviceId, Guid categoryId) 
    {
        ServiceId = serviceId;
        CategoryId = categoryId;
    }
}