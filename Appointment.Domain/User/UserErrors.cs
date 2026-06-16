using Appointment.Domain.Common;

namespace Appointment.Domain.User;


public static class UserErrors
{
    public static  Error IdCannotBeEmpty => Error.Validation("Id cannot be empty.");
    public static  Error NameCannotBeEmpty => Error.Validation("Name cannot be empty.");
    public static  Error EmailCannotBeEmpty => Error.Validation("Email cannot be empty.");
    public static  Error RoleCannotBeNull => Error.Validation("Role cannot be null.");
    public static  Error PhoneNumberCannotBeEmpty => Error.Validation("Phone number cannot be empty.");
    public static  Error PasswordCannotBeEmpty => Error.Validation("Password cannot be empty.");
}