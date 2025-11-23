using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment03_efcore.Models
{
    internal class Airline
    {
        public int AirlineId { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }

       
        public List<string> Phones { get; set; } = new();

        [InverseProperty(nameof(Employee.Airline))]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

        [InverseProperty(nameof(Aircraft.Airline))]
        public ICollection<Aircraft> Aircrafts { get; set; } = new HashSet<Aircraft>();

        [InverseProperty(nameof(Transaction.Airline))]
        public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
    }
}
