using Assignment03_efcore.DatabaseContext;
using Assignment03_efcore.Models;

namespace Assignment03_efcore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using AirLineDbContext airlineDbContext = new AirLineDbContext();

            #region CRUD
            var egyptAir = new Airline
            {
                Name = "EgyptAir",
                ContactPerson = "Ahmed Ali",
                Phones = new List<string> { "0123456789", "0113654789" },
                Address = "Cairo"
            };
            airlineDbContext.Airlines.Add(egyptAir);
            airlineDbContext.SaveChanges();

            var aircraft = new Aircraft
            {
                Model = "Model01",
                Capacity = 180,
                AirlineId = egyptAir.AirlineId
            };
            airlineDbContext.Aircrafts.Add(aircraft);
            airlineDbContext.SaveChanges();

            var trans = new Transaction
            {
                Amount = 50000,
                Description = "Tickets",
                Date = DateTime.Now,
                AirlineId = egyptAir.AirlineId
            };
            airlineDbContext.Transactions.Add(trans);
            airlineDbContext.SaveChanges();

            var employees = airlineDbContext.Employees.Where(e => e.AirlineId == egyptAir.AirlineId).ToList();

            var transactions = airlineDbContext.Transactions.Where(t => t.AirlineId == egyptAir.AirlineId)
            .Select(t => new { t.TransactionId, t.Description, t.Amount }).ToList();

            var result = airlineDbContext.Airlines.Select(a => new{Airline = a.Name,EmployeeCount = a.Employees.Count()}).ToList();

            var model01 = airlineDbContext.Aircrafts.First(a => a.Model == "Model01");
            model01.Capacity = 200;
            airlineDbContext.SaveChanges();

            var oldTrans = airlineDbContext.Transactions.Where(t => t.Date.Year < 2020).ToList();
            airlineDbContext.Transactions.RemoveRange(oldTrans);
            airlineDbContext.SaveChanges();

            var route = new Route
            {
                From = "Cairo",
                To = "Dubai",
                Classification = "International",
                Distance = 2400
            };
            airlineDbContext.Routes.Add(route);
            airlineDbContext.SaveChanges();

            var assign = new RouteAssignment
            {
                RouteId = route.RouteId,
                AircraftId = model01.AircraftId,
                Duration = 4,
                Price = 3000
            };
            airlineDbContext.RouteAssignments.Add(assign);
            airlineDbContext.SaveChanges();


            #endregion
        }
    }
}
