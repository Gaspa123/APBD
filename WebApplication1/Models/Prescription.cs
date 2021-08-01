using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Prescription
    {
       
        public int IdPrescription { get; set; }
      
        public DateTime Date { get; set; }
        
        public DateTime DueDate { get; set; }

        public virtual int IdPatient { get; set; }

        public virtual int IdDoctor { get; set; }

        public virtual Patient Patient { get; set; }
      
        public virtual Doctor Doctor { get; set; }

        public virtual ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; }
    }
}
