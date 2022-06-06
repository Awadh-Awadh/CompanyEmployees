using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities.ErrorModel
{
    // chapter 5
    public class ErrorDetails
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
       
    }
}
