using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.EfConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> opt)
        {
            opt.ToTable("User");
            opt.HasKey(e => e.Login);
            opt.Property(e => e.Login).ValueGeneratedOnAdd();

            opt.Property(e => e.RefreshToken).IsRequired().HasMaxLength(100);
            opt.Property(e => e.RefreshTokenExp).IsRequired().HasMaxLength(100);
            opt.Property(e => e.Salt).IsRequired().HasMaxLength(100);
            opt.Property(e => e.Password).IsRequired().HasMaxLength(100);

            var user1 = SecurityHelpers.GetHashedPasswordAndSalt("Paweł");
            var user2 = SecurityHelpers.GetHashedPasswordAndSalt("Java");
            opt.HasData(new User
            {
                Login = "Piotr",
                Password = user1.Item1,
                RefreshToken = "didiidieieicinflcfikmejckjeklalkjmikjfnkohnjuijohnujdasdsa",
                RefreshTokenExp = DateTime.Today.AddHours(1),
                Salt = user1.Item2

            });

            opt.HasData(new User
            {
                Login = "C#",
                Password = user2.Item1,
                RefreshToken = "aaaaaaaaaaaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbbbbbbccccccccccccccde",
                RefreshTokenExp = DateTime.Today.AddHours(1),
                Salt = user2.Item2
            });
        }
    }
}
