using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.EfConfigurations
{
    public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> opt)
        {
            opt.ToTable("Patient");
            opt.HasKey(e => e.IdPatient);
            opt.Property(e => e.IdPatient).ValueGeneratedOnAdd();

            opt.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            opt.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            opt.Property(e => e.BirthDate).IsRequired();

           

            opt.HasData(new Patient 
            {
                IdPatient = 1,
                BirthDate = DateTime.Today, 
                FirstName = "Piotr",
                LastName = "D"
                
            });

            opt.HasData(new Patient 
            {
                IdPatient = 2,
                BirthDate = DateTime.Today, 
                FirstName = "Kasia", 
                LastName = "E"
                 
            });

            opt.HasData(new Patient 
            { 
                IdPatient = 3 ,
                BirthDate = DateTime.Today,
                FirstName = "Kacper",
                LastName = "F"
                
            });

          
        }
    }
}
