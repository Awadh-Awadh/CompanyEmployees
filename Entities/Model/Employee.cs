using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Employee
    {
        public int Id { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage = "Age is a required field")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage ="Age is a required field")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Position is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
        public string Position { get; set; } = default!;
        public int CompanyId { get; set; }
        public Company Company { get; set; } = default!;

    }
}
