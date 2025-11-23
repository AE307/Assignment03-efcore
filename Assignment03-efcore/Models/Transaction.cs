using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment03_efcore.Models
{
    internal class Transaction
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey(nameof(Airline))]
        public int AirlineId { get; set; }
        public Airline Airline { get; set; } = null!;
    }
}
