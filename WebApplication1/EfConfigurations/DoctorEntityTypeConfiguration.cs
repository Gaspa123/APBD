using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.EfConfigurations
{
    public class DoctorEntityTypeConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> opt)
        {
            opt.ToTable("Doctor");
            opt.HasKey(e => e.IdDoctor);
            opt.Property(e => e.IdDoctor).ValueGeneratedOnAdd();

            opt.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            opt.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            opt.Property(e => e.Email).IsRequired().HasMaxLength(100);



            opt.HasData(new Doctor
            {
                IdDoctor = 1,
                FirstName = "Piotr",
                LastName = "A",
                Email = "gmail1@gmail.com"
            });
            opt.HasData(new Doctor
            {
                IdDoctor = 2,
                FirstName = "Adam",
                LastName = "B",
                Email = "gmail2@gmail.com"
            });

            opt.HasData(new Doctor
            {
                IdDoctor = 3,
                FirstName = "Jan",
                LastName = "C",
                Email = "gmail3@gmail.com"
            });
           
        }
    }
}
