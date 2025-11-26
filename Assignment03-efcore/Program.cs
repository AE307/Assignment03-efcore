using Assignment03_efcore.DatabaseContext;
using Assignment03_efcore.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment03_efcore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using AirLineDbContext airlineDbContext = new AirLineDbContext();

            #region CRUD
            //var egyptAir = new Airline
            //{
            //    Name = "EgyptAir",
            //    ContactPerson = "Ahmed Ali",
            //    Phones = new List<string> { "0123456789", "0113654789" },
            //    Address = "Cairo"
            //};
            //airlineDbContext.Airlines.Add(egyptAir);
            //airlineDbContext.SaveChanges();

            //var aircraft = new Aircraft
            //{
            //    Model = "Model01",
            //    Capacity = 180,
            //    AirlineId = egyptAir.AirlineId
            //};
            //airlineDbContext.Aircrafts.Add(aircraft);
            //airlineDbContext.SaveChanges();

            //var trans = new Transaction
            //{
            //    Amount = 50000,
            //    Description = "Tickets",
            //    Date = DateTime.Now,
            //    AirlineId = egyptAir.AirlineId
            //};
            //airlineDbContext.Transactions.Add(trans);
            //airlineDbContext.SaveChanges();

            //var employees = airlineDbContext.Employees.Where(e => e.AirlineId == egyptAir.AirlineId).ToList();

            //var transactions = airlineDbContext.Transactions.Where(t => t.AirlineId == egyptAir.AirlineId)
            //.Select(t => new { t.TransactionId, t.Description, t.Amount }).ToList();

            //var result = airlineDbContext.Airlines.Select(a => new{Airline = a.Name,EmployeeCount = a.Employees.Count()}).ToList();

            //var model01 = airlineDbContext.Aircrafts.First(a => a.Model == "Model01");
            //model01.Capacity = 200;
            //airlineDbContext.SaveChanges();

            //var oldTrans = airlineDbContext.Transactions.Where(t => t.Date.Year < 2020).ToList();
            //airlineDbContext.Transactions.RemoveRange(oldTrans);
            //airlineDbContext.SaveChanges();

            //var route = new Route
            //{
            //    From = "Cairo",
            //    To = "Dubai",
            //    Classification = "International",
            //    Distance = 2400
            //};
            //airlineDbContext.Routes.Add(route);
            //airlineDbContext.SaveChanges();

            //var assign = new RouteAssignment
            //{
            //    RouteId = route.RouteId,
            //    AircraftId = model01.AircraftId,
            //    Duration = 4,
            //    Price = 3000
            //};
            //airlineDbContext.RouteAssignments.Add(assign);
            //airlineDbContext.SaveChanges();

            //var employeeToAdd = new Employee
            //{
            //    Name = "Ahmed Elsayed",
            //    AirlineId = 3
            //};
            //airlineDbContext.Employees.Add(employeeToAdd);
            //airlineDbContext.SaveChanges();


            #endregion
            #region Section A : Loading Related Data 
            #region Q1
            var egyptair = airlineDbContext.Airlines.Where(al=>al.Name =="EgyptAir").Include(ac=>ac.Aircrafts)
                .ThenInclude(ra => ra.RouteAssignments).ThenInclude(r => r.Route).FirstOrDefault();
            if (egyptair != null)
            {
                Console.WriteLine($"Airline: {egyptair.Name}");
                foreach (var aircraft in egyptair.Aircrafts)
                {
                    Console.WriteLine($"\tAircraft Model: {aircraft.Model}, Capacity: {aircraft.Capacity}");
                    foreach (var assignment in aircraft.RouteAssignments)
                    {
                        Console.WriteLine($"\t\tRoute From: {assignment.Route.From} To: {assignment.Route.To}, Distance: {assignment.Route.Distance} km");
                    }
                }
            }
            #endregion
            #region Q2
            var airlinesWithEmployees = airlineDbContext.Airlines
                .Include(e => e.Employees)
                .ToList();
            foreach(var airline in airlinesWithEmployees)
             {
                Console.WriteLine($"Airline: {airline.Name}");
                foreach(var employee in airline.Employees)
                {
                    Console.WriteLine($"\tEmployee ID: {employee.EmployeeId}, Employee Name: {employee.Name}, Airline ID: {employee.AirlineId}");
                }
            }
            #endregion
            #region Q3
            var transactionsWithAirline = airlineDbContext.Transactions.Where(t=>t.Amount>10000).Include(al => al.Airline).ToList();
            foreach(var transaction in transactionsWithAirline)
            {
                Console.WriteLine($"Airline: {transaction.Airline.Name},Amount: {transaction.Amount}");
            }
            #endregion
            #region Q4
            var routesWithAircraftModels = airlineDbContext.Routes.Select(r => new
            {
                Route = r,
                AircraftModels = r.AircraftAssignments
                    .Select(ar => ar.Aircraft.Model)
                    .ToList()
            }).ToList();
            foreach (var routeInfo in routesWithAircraftModels)
            {
                Console.WriteLine($"Route From: {routeInfo.Route.From} To: {routeInfo.Route.To}");
                foreach (var model in routeInfo.AircraftModels)
                {
                    Console.WriteLine($"\tAircraft Model: {model}");
                }
            }
            #endregion
            #region Q5
            var aircraftsWithAirline = airlineDbContext.Aircrafts.Include(ac => ac.Airline).ToList();
            foreach (var ac in aircraftsWithAirline)
            {
                Console.WriteLine($"{ac.Model} - {ac.Airline.Name} - {ac.Airline.Phones}");
            }

            #endregion
            #endregion
            #region Section B : Join Operators 
            #region Q1
            var result = from emp in airlineDbContext.Employees
                         join air in airlineDbContext.Airlines
                         on emp.AirlineId equals air.AirlineId
                         select new
                         {
                             EmployeeName = emp.Name,
                             AirlineName = air.Name
                         };

            foreach (var item in result)
                Console.WriteLine($"{item.EmployeeName} - {item.AirlineName}");

            #endregion
            #region Q2
            var result2 = airlineDbContext.RouteAssignments
                .Join(airlineDbContext.Aircrafts,
                    assigned => assigned.AircraftId,
                    ac => ac.AircraftId,
                    (assigned, ac) => new { assigned, ac })
                .Join(airlineDbContext.Airlines,
                    combined => combined.ac.AirlineId,
                    air => air.AirlineId,
                    (combined, air) => new { combined.assigned, combined.ac, air })
                .Join(airlineDbContext.Routes,
                    combined => combined.assigned.RouteId,
                    route => route.RouteId,
                    (combined, route) => new
                    {
                        Route = route.From + " → " + route.To,
                        AircraftModel = combined.ac.Model,
                        AirlineName = combined.air.Name
                    }).ToList();

            foreach (var item in result2)
                Console.WriteLine($"{item.Route} - {item.AircraftModel} - {item.AirlineName}");


            #endregion
            #region Q3
            var result3 = from ac in airlineDbContext.Aircrafts
                         group ac by ac.Airline.Name into g
                         select new
                         {
                             Airline = g.Key,
                             Models = g.Select(x => x.Model).ToList()
                         };

            foreach (var item in result3)
            {
                Console.WriteLine(item.Airline);
                Console.WriteLine("Models: " + string.Join(", ", item.Models));
            }

            #endregion
            #region Q4
            var result4 = airlineDbContext.Transactions
                .Where(t => t.Amount > 20000)
                .Join(airlineDbContext.Airlines,
                    t => t.AirlineId,
                    a => a.AirlineId,
                    (t, a) => new
                    {
                        t.AirlineId,
                        t.Amount,
                        t.Description,
                        AirlineName = a.Name
                    }).ToList();
            foreach(var item in result4)
                Console.WriteLine($"{item.AirlineId} - {item.Amount} - {item.Description} - {item.AirlineName}");

            #endregion
            #endregion
        }
    }
}
