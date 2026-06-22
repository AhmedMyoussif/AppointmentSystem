using Appointment.Domain.Common;

namespace Appointment.Application.Common.Errors;
public static class ApplicationErrors
{
    public static readonly Error InvalidCredentials = 
        Error.Validation("Auth.InvalidCredentials", "The email or password provided is incorrect.");

    public static readonly Error EmailAlreadyExists = 
        Error.Conflict("Auth.EmailConflict", "A user with this email already exists.");

    public static readonly Error UnauthorizedAccess = 
        Error.Failure("Auth.Unauthorized", "You are not authorized to perform this action.");


    public static readonly Error UserNotFound = 
        Error.NotFound("User.NotFound", "The requested user profile was not found.");

    public static readonly Error ProviderNotFound = 
        Error.NotFound("Provider.NotFound", "The specified service provider does not exist.");

    public static readonly Error ServiceNotFound = 
        Error.NotFound("Service.NotFound", "The requested service does not exist.");

    public static readonly Error AppointmentNotFound = 
        Error.NotFound("Appointment.NotFound", "The requested appointment was not found.");

    public static readonly Error TimeSlotNotFound = 
        Error.NotFound("TimeSlot.NotFound", "The selected time slot does not exist or has been deleted.");

    public static readonly Error TimeSlotAlreadyBooked = 
        Error.Conflict("Appointment.SlotAlreadyBooked", "This time slot has just been booked by another customer.");

    public static readonly Error ProviderNotAvailable = 
        Error.Failure("Provider.Unavailable", "The provider is not accepting appointments on the selected date.");

    public static readonly Error CategoryNameAlreadyExists = 
        Error.Conflict("Category.NameConflict", "A category with this name already exists.");

    public static readonly Error CategoryNotFound =
        Error.NotFound("Category.NotFound", "The requested category was not found.");

    public static readonly Error CategoryCannotBeDeletedWithActiveServices =
        Error.Failure("Category.DeleteFailed", "Cannot delete category because it has active services linked to it.");


    public static readonly Error ServiceCannotBeDeletedWithActiveAppointments =
        Error.Failure("Service.DeleteFailed", "Cannot delete service because it has active appointments linked to it.");

    public static readonly Error TimeSlotOverlapsWithExisting =
        Error.Conflict("TimeSlot.Overlap", "The specified time slot overlaps with an existing time slot for the provider.");


    public static readonly Error CannotDeleteBookedTimeSlot =
        Error.Failure("TimeSlot.DeleteFailed", "Cannot delete the time slot because it has been booked by a customer.");


   public static readonly Error ExpiredAccessTokenInvalid =
        Error.Validation("Auth.ExpiredAccessTokenInvalid", "The expired access token provided is invalid.");

    public static readonly Error UserIdClaimInvalid =
        Error.Validation("Auth.UserIdClaimInvalid", "The user ID claim in the token is invalid.");

    public static readonly Error RefreshTokenExpired =
        Error.Validation("Auth.RefreshTokenExpired", "The refresh token has expired or is invalid.");
}