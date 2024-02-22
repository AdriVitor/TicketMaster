using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketMaster_Domain.Entities;

namespace TicketMaster_Infra.Data.EntityConfiguration;
public class EventConfiguration : IEntityTypeConfiguration<Event> {
    public void Configure(EntityTypeBuilder<Event> builder) {
        builder.ToTable("EVENT");

        builder.HasKey(e=>e.Id);

        builder.Property(e => e.Name)
            .HasColumnName("NAME")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnName("DESCRIPTION")
            .HasMaxLength(150);

        builder.Property(e => e.TotalAmount)
            .HasColumnName("TOTALAMOUNT")
            .HasColumnType("INT");

        builder.Property(e => e.CurrentQuantityTickets)
            .HasColumnName("CURRENTQUANTITYTICKETS")
            .HasColumnType("INT");

        builder.Property(e => e.FederativeUnit)
            .HasColumnName("FEDERATIVEUNIT")
            .HasColumnType("INT")
            .IsRequired();

        builder.Property(e=>e.ProducerId)
            .HasColumnName("PRODUCERID")
            .HasColumnType("INT")
            .IsRequired();

        builder.HasOne(e=>e.Producer)
            .WithMany(p=>p.Events)
            .HasForeignKey(e=>e.ProducerId);
    }
}

