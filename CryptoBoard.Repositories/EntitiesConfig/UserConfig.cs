using CryptoBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Infra.Data.EntitiesConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(50);

            builder
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(400);

            builder
                .Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(u => u.CreationDate)
                .IsRequired();

            builder
                .HasMany(l => l.WatchLists)
                .WithOne(p => p.User);
        }
    }
}
