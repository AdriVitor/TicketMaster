using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketMaster_Domain.Entities;

namespace TicketMaster_Infra.Data.EntityConfiguration;
public class TicketConfiguration : IEntityTypeConfiguration<Ticket> {
    public void Configure(EntityTypeBuilder<Ticket> builder) {
        builder.ToTable("TICKET");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.CustomerId)
            .HasColumnName("CUSTOMERID")
            .IsRequired();

        builder.Property(t => t.EventId)
            .HasColumnName("EVENTID")
            .HasColumnType("INT")
            .IsRequired();

        builder.Property(t => t.FlagConsumed)
            .HasColumnName("FLAGCONSUMED")
            .HasColumnType("BIT");
    }
}

