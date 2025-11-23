using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment03_efcore.Models
{
    [Owned]
    internal class Crew
    {
        
        public string? MajorPilot { get; set; }
        public string? AssistantPilot { get; set; }
        public string? Host1 { get; set; }
        public string? Host2 { get; set; }
        
    }
}
