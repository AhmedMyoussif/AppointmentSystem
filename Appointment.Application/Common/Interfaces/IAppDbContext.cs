using Appointment.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Appointment.Domain;
using Appointment.Domain.Common;
using Appointment.Domain.TimeSlots;
using Appointment.Domain.Services;
using Appointment.Domain.Category;
using Appointment.Domain.Identity;

namespace Appointment.Application.Common.Interfaces; 


public interface IAppDbContext
{
    public DbSet<Appointment.Domain.Appointment> Appointments { get; set; }
    public DbSet<User> Users { get;  }
    public DbSet<Service> Services { get;  }
    public DbSet<TimeSlot> TimeSlots { get;  } 
    public DbSet<Category> Categories { get;  }
    public DbSet<RefreshToken> RefreshTokens { get;  }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}