using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Company
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(60, ErrorMessage = "The maximum length for name is 60")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "This field is requires")]
        [MaxLength(60, ErrorMessage = "The maximum length for Address is 60")]
        public string Address { get; set; } = default!;
        public string Country { get; set; } = default!;
        public ICollection<Employee> Employees { get; set; } = default!;
    }
}
