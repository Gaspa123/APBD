using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DTOs.Res
{
    public class GetPrescriptionRes
    {
        public int IdPrescription { get; set; }
        public int IdPatient { get; set; }  
        public string FirstNamePatient { get; set; }
        public string LastNamePatient { get; set; }
        public DateTime BirthDatePatient { get; set; }

        public int IdDoctor { get; set; }
        public string FirstNameDoctor { get; set; }

        public string LastNameDoctor { get; set; }

        public string EmailDoctor { get; set; }

        public ICollection<Medicament> Medicaments { get; set; }
    }
}
