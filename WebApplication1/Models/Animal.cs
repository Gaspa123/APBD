using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Animal
    {
        //[Required(ErrorMessage = "ID required")]
        //[RegularExpression(@"^[0-9]$", ErrorMessage = "ID error")]
        //public int ID { get; }

        [Required(ErrorMessage = "Name required")]
        [MaxLength(100, ErrorMessage = "Too long name")]
        [RegularExpression(@"^[A-Z]*[a-z]+$", ErrorMessage = "Invalid character in name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description required")]
        [MaxLength(200, ErrorMessage = "Too long description")]
        [RegularExpression(@"^[A-Z a-z]+[\d\w\s]+", ErrorMessage = "Invalid character in description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Description required")]
        [MaxLength(100, ErrorMessage = "Too long category")]
        [RegularExpression(@"^[A-Z]*[a-z]+$", ErrorMessage = "Invalid character in category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Description required")]
        [MaxLength(500, ErrorMessage = "Too long area")]
        [RegularExpression(@"^[A-Z]*[a-z]+$", ErrorMessage = "Invalid character in area")]
        public string Area { get; set; }


        public string ToString()
        {
            return ($"{this.Name},{this.Description},{this.Category},{this.Area}");
        }
    }
}
