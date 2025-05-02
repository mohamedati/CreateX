using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public  class GenericResponse
    {
        public bool IsSuccess { get; set; }
       public string? Message {  get; set; }

        public object?  Data {  get; set; }

        public Dictionary<string, string[]> Errors { get; set; }
    }
}
