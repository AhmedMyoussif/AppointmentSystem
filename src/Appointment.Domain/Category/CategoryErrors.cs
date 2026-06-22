using Appointment.Domain.Common;

namespace Appointment.Domain.Category; 

public static class CategoryErrors
{
    public static Error NameCannotBeEmpty =>
         Error.Validation("Name cannot be empty.");
}