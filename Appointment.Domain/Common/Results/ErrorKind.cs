namespace Appointment.Domain.Common ;

public enum ErrorKind
{
    Failure,
    Unexpected,
    Validation,
    Conflict,
    NotFound,
    Unauthorized,
    Forbidden,
}