using Appointment.Domain.Common;

namespace Appointment.Domain.Services; 

public static class ServiceErrors
{
    public static Error NameCannotBeEmpty =>
         Error.Validation("Name cannot be empty.");
    public static Error PriceCannotBeNegative =>
         Error.Validation("Price cannot be negative.");
    public static Error ProviderIdCannotBeEmpty =>
         Error.Validation("Provider ID cannot be empty.");
    
}