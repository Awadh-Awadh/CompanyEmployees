using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs;

public class EmployeeForCreationDTO
{
    [Required(ErrorMessage = "Name is a required field")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Name field is 30")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Age is a required field.")]
    public int Age { get; set; }
    [Required(ErrorMessage = "Position is a required field.")]
    [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
    public string Position { get; set; }
}
