using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketMaster_Domain.Entities;

namespace TicketMaster_Infra.Data.EntityConfiguration;
public class ProducerConfiguration : IEntityTypeConfiguration<Producer> {
    public void Configure(EntityTypeBuilder<Producer> builder) {
        builder.ToTable("PRODUCER");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasColumnName("NAME")
            .HasColumnType("VARCHAR")
            .HasMaxLength(70)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasColumnName("EMAIL")
            .HasMaxLength(70)
            .IsRequired();

        builder.Property(p => p.CPF)
            .HasColumnName("CPF")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(p => p.Password)
            .HasColumnName("PASSWORD")
            .HasMaxLength(30)
            .IsRequired();

        builder.HasMany(p => p.Events)
            .WithOne(e => e.Producer)
            .HasForeignKey(e => e.ProducerId);
    }
}

