using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CompanyForCreationDto
    {
        [Required(ErrorMessage ="Name for a company is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address for a company is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Address for a company is required")]
        public string Country { get; set; }
    }
}
