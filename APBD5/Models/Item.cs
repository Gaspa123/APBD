using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APBD5.Models
{
    public class Item
    {
        [Required(ErrorMessage = "IdProcuct requested")]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int IdProduct { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Required(ErrorMessage = "IdWarehouse requested")]
        public int IdWarehouse { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Required(ErrorMessage = "Amount requested")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Date requested")]
        public DateTime CreatedAt { get; set; }

       

    }
}
