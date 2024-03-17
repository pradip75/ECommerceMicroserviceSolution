using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GlobalErrorHandling
{
    public class ProblemDetails
    { 
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int? Status { get; set; }
        public string? Detail { get; set; }
        public string? Instance { get; set; }
        public IDictionary<string, object?> Extensions { get; }
    }
}

