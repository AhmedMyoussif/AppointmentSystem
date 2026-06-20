using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Appointment.Domain.TimeSlots;
using Appointment.Domain.Users;

namespace Appointment.Infrastructure.Data.Configurations;

public sealed class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
{
    public void Configure(EntityTypeBuilder<TimeSlot> builder)
    {
        builder.ToTable("TimeSlots");

        builder.HasKey(ts => ts.Id);

        builder.Property(ts => ts.StartTime)
            .IsRequired();

        builder.Property(ts => ts.EndTime)
            .IsRequired();

        builder.Property(ts => ts.IsBooked)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne<User>() 
            .WithMany()
            .HasForeignKey(ts => ts.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);
}