using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Phone).HasMaxLength(20);

        builder.Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(20); 
        
        builder.OwnsOne(u => u.Name, name =>
            {
                name.Property(n => n.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("FirstName");

                name.Property(n => n.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LastName");
            }); 
        
        builder.OwnsOne(u => u.Address, address =>
            {
                address.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("City");

                address.Property(a => a.Street)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Street");

                address.Property(a => a.Number)
                    .IsRequired()
                    .HasColumnName("Number");

                address.Property(a => a.ZipCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("ZipCode");

                address.OwnsOne(a => a.Geolocation, geo =>
                {
                    geo.Property(g => g.Latitude)
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnName("GeoLat");

                    geo.Property(g => g.Longitude)
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnName("GeoLong");
                });
            });
    }
}
