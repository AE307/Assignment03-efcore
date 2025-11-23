using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment03_efcore.Models
{
    [PrimaryKey(nameof(RouteId), nameof(AircraftId))]
    internal class RouteAssignment
    {
        [ForeignKey(nameof(Route))]
        public int RouteId { get; set; }
        [InverseProperty(nameof(Route.AircraftAssignments))]
        public Route Route { get; set; }= null!;

        [ForeignKey(nameof(Aircraft))]
        public int AircraftId { get; set; }
        [InverseProperty(nameof(Aircraft.RouteAssignments))]
        public Aircraft Aircraft { get; set; }= null!;

        public int Duration { get; set; }   
        public decimal Price { get; set; }  
    }
}
