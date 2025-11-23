using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment03_efcore.Models
{
    internal class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Airline))]
        public int AirlineId { get; set; }
        public Airline Airline { get; set; } = null!;
    }
}
