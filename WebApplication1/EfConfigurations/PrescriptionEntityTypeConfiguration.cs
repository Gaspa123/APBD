using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.EfConfigurations
{
    public class PrescriptionEntityTypeConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> opt)
        {
            opt.ToTable("Prescription");

            opt.HasKey(e => e.IdPrescription);
            opt.Property(e => e.IdPrescription).ValueGeneratedOnAdd();

            opt.Property(e => e.Date).IsRequired();
            opt.Property(e => e.DueDate).IsRequired();

            opt.HasOne(e => e.Patient)
                .WithMany(s => s.Prescriptions)
                .HasForeignKey(s => s.IdPatient);

            opt.HasOne(e => e.Doctor)
                .WithMany(s => s.Prescriptions)
                .HasForeignKey(s => s.IdDoctor);

            

            opt.HasData(new Prescription 
            {
                
                IdPatient = 1,
                IdPrescription = 1,
                Date = DateTime.Today,
                DueDate = DateTime.Today.AddDays(10),
                IdDoctor = 1
            });

            opt.HasData(new Prescription 
            {
                IdPatient = 2,
                IdPrescription = 2,
                Date = DateTime.Today,
                DueDate = DateTime.Today.AddDays(20),
                IdDoctor = 2
               
            });

            opt.HasData(new Prescription 
            {
                IdPatient = 3,
                IdPrescription = 3,
                Date = DateTime.Today, 
                DueDate = DateTime.Today.AddDays(30), 
                IdDoctor = 3
            });

           
        }
    }
}
