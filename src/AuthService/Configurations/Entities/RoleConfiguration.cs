using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Configurations.Entities
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "Guest",
                    NormalizedName = "GUEST"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Name = "Tutor",
                    NormalizedName = "TUTOR"
                },
                new IdentityRole
                {
                    Name = "Operator",
                    NormalizedName = "OPERATOR"
                },
                new IdentityRole
                {
                    Name = "Adminstrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            );
        }
    }
}