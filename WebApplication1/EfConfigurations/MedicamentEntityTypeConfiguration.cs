using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.EfConfigurations
{
    public class MedicamentEntityTypeConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> opt)
        {
            opt.ToTable("Medicament");
            opt.HasKey(e => e.IdMedicament);
            opt.Property(e => e.IdMedicament).ValueGeneratedOnAdd();

            opt.Property(e => e.Name).IsRequired().HasMaxLength(100);
            opt.Property(e => e.Description).IsRequired().HasMaxLength(100);
            opt.Property(e => e.Type).IsRequired().HasMaxLength(100);



            opt.HasData(new Medicament 
            {
                IdMedicament = 1, 
                Name = "Apap",
                Description = "Ból", 
                Type = "paracetamol" 
            });

            opt.HasData(new Medicament 
            { 
                IdMedicament = 2,
                Name = "Ibuprom",
                Description = "Gorączka",
                Type = "ibuprofen"
            });
            opt.HasData(new Medicament 
            { 
                IdMedicament = 3,
                Name = "Ibuprom Max",
                Description = "Wysoka gorączka",
                Type = "ibuprofen"
            });
        }
    }
}
