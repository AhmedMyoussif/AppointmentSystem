using Appointment.Domain.Common;
using Appointment.Domain.Common.Results;
using Appointment.Domain.Identity;

namespace Appointment.Domain.Users; 
public sealed class User : AuditableEntity
{
    public string FirstName { get;private set; }
    public string LastName { get;private set; }
    
    public string Email { get;private set; }
    public string PhoneNumber { get; private set; }
    public string Password { get; private set; }


    public Role Role { get; private set; }

    private User()
    {}

    private User(Guid id, string firstName, string lastName, string email,string phoneNumber, string password, Role role) : base(id)
    {
       
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
        Role = role;
    }

    public static Result<User> Create(Guid id, string firstName, string lastName, string email, string phoneNumber, string password, Role role = Role.User)
    {
        if (id == Guid.Empty)
            return UserErrors.IdCannotBeEmpty;

        if (string.IsNullOrWhiteSpace(firstName))
            return UserErrors.NameCannotBeEmpty;

        if (string.IsNullOrWhiteSpace(lastName))
            return UserErrors.NameCannotBeEmpty;

        if (string.IsNullOrWhiteSpace(email))
            return UserErrors.EmailCannotBeEmpty;

        if(string.IsNullOrWhiteSpace(phoneNumber))
            return UserErrors.PhoneNumberCannotBeEmpty;

        if (string.IsNullOrWhiteSpace(password))
            return UserErrors.PasswordCannotBeEmpty;

    
        var user = new User(id, firstName.Trim(), lastName.Trim(), email, phoneNumber.Trim(), password, role);
        return user;
    }

}