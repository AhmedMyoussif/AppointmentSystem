using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Appointment.Domain.Appointments;
using Appointment.Domain.Services;
using Appointment.Domain.TimeSlots;
using Appointment.Domain.Users;
using Appointment.Domain;

namespace Appointment.Infrastructure.Data.Configurations;

public sealed class AppointmentConfiguration : IEntityTypeConfiguration<Domain.Appointments.Appointment>
{
    public void Configure(EntityTypeBuilder<Domain.Appointments.Appointment> builder)
    {
        builder.ToTable("Appointments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.AppointmentDate)
            .IsRequired();

        builder.Property(a => a.StartTime)
            .IsRequired();

        builder.Property(a => a.EndTime)
            .IsRequired();

        builder.Property(a => a.PriceAtBooking)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(a => a.Notes)
            .HasMaxLength(500);

        builder.Property(a => a.Status)
            .HasConversion(
                s => s.ToString(),
                s => (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus), s))
            .HasMaxLength(20)
            .IsRequired();

        
        builder.HasOne(a => a.Customer)
            .WithMany()
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Service)
            .WithMany()
            .HasForeignKey(a => a.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<TimeSlot>()
            .WithMany()
            .HasForeignKey(a => a.TimeSlotId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}