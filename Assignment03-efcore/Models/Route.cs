using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment03_efcore.Models
{
    internal class Route
    {
        public int RouteId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Classification { get; set; }
        public int Distance { get; set; }

        [InverseProperty(nameof(RouteAssignment.Route))]
        public ICollection<RouteAssignment> AircraftAssignments { get; set; } = new HashSet<RouteAssignment>();
    }
}
