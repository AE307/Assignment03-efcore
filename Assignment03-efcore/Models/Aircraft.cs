using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment03_efcore.Models
{
    internal class Aircraft
    {
        public int AircraftId { get; set; }
        public string Model { get; set; }
        public int Capacity { get; set; }
        public Crew AircraftCrew { get; set; } = null!;

        [ForeignKey(nameof(Airline))]
        public int AirlineId { get; set; }
        public Airline Airline { get; set; } = null!;

        [InverseProperty(nameof(RouteAssignment.Aircraft))]
        public ICollection<RouteAssignment> RouteAssignments { get; set; } = new HashSet<RouteAssignment>();
    }
}
