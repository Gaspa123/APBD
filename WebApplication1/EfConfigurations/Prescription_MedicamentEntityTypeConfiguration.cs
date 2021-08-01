using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.EfConfigurations
{
    public class Prescription_MedicamentEntityTypeConfiguration : IEntityTypeConfiguration<Prescription_Medicament>
    {
        public void Configure(EntityTypeBuilder<Prescription_Medicament> opt)
        {
            opt.ToTable("Prescription_Medicament");
            opt.HasKey(e => e.IdMedicament);
            opt.HasKey(e => e.IdPrescription);
            opt.Property(e => e.Dose);
            opt.Property(e => e.Details).IsRequired().HasMaxLength(100);

           opt.HasData (new Prescription_Medicament { IdPrescription = 1, IdMedicament = 1, Details = "details1", Dose = 2 });

            opt.HasData(new Prescription_Medicament { IdPrescription = 2, IdMedicament = 2, Details = "details2", Dose = null });

            opt.HasData(new Prescription_Medicament { IdPrescription = 3, IdMedicament = 3, Details = "details3", Dose = 100 });
        }
    }
}
