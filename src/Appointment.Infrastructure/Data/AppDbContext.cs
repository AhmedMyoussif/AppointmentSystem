using Appointment.Application.Common.Interfaces;
using Appointment.Domain.Category;
using Appointment.Domain.Identity;
using Appointment.Domain.Services;
using Appointment.Domain.TimeSlots;
using Appointment.Domain.Users;
using Appointment.Infrastructure.Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Infrastructure.Data;


public class AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : IdentityDbContext<AppUser>(options), IAppDbContext
{

    public DbSet<Appointment.Domain.Appointments.Appointment> Appointments { get; set; }

    public DbSet<Service> Services => throw new NotImplementedException();

    public DbSet<TimeSlot> TimeSlots => throw new NotImplementedException();

    public DbSet<Category> Categories => throw new NotImplementedException();

    public DbSet<RefreshToken> RefreshTokens => throw new NotImplementedException();

    DbSet<User> IAppDbContext.Users => throw new NotImplementedException();
}