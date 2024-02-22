using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketMaster_Domain.Entities;

namespace TicketMaster_Infra.Data.EntityConfiguration;
public class CustomerConfiguration : IEntityTypeConfiguration<Customer> {
    public void Configure(EntityTypeBuilder<Customer> builder) {
        builder.ToTable("CUSTOMER");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasColumnName("NAME")
            .HasColumnType("VARCHAR")
            .HasMaxLength(70)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasColumnName("EMAIL")
            .HasMaxLength(70)
            .IsRequired();

        builder.Property(c => c.CPF)
            .HasColumnName("CPF")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(c => c.Password)
            .HasColumnName("PASSWORD")
            .HasMaxLength(30)
            .IsRequired();

        builder.HasMany(c => c.Tickets)
            .WithOne(t => t.Customer)
            .HasForeignKey(t => t.CustomerId);
    }
}

