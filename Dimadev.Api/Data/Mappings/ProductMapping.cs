using Dimadev.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dimadev.Api.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
        .IsRequired()
        .HasColumnType("VARCHAR")
        .HasMaxLength(80);

        builder.Property(x => x.Slug)
        .IsRequired()
        .HasColumnType("VARCHAR")
        .HasMaxLength(80);

        builder.Property(x => x.Description)
        .IsRequired(false)
        .HasColumnType("VARCHAR")
        .HasMaxLength(255);

        builder.Property(x => x.Price)
        .IsRequired()
        .HasColumnType("DECIMAL(18,2)");

        builder.Property(x => x.IsActive)
        .IsRequired()
        .HasColumnType("BIT");

    }
}
