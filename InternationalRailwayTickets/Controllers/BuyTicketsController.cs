using InternationalRailwayTickets.Data;
using InternationalRailwayTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalRailwayTickets.Controllers
{
    public class BuyTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BuyTicketsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Stations = await _context.Stations.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FindTrain(FindTrainModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.FromId == model.ToId)
                {
                    return RedirectToAction("Index");
                }

                var query = from train in _context.TrainInstances
                            where train.Route.Points.Any(e => e.Station.Id == model.FromId) &&
                                  train.Route.Points.Any(e => e.Station.Id == model.ToId) &&
                                  model.FromDate.AddMonths(-1) <= train.StartDate && train.StartDate <= model.FromDate
                            select train;
                var trains = await query.Include(e => e.Route).ThenInclude(e => e.Points).ThenInclude(e => e.Station).ToListAsync();

                trains = trains.Where(
                        train => train.Route.Points.First(e => e.Station.Id == model.FromId).FromStartTime <= train.Route.Points.First(e => e.Station.Id == model.ToId).FromStartTime
                    ).Where(train =>
                        train.GetTimeAtPoint(train.Route.Points.First(e => e.Station.Id == model.FromId)).Date == model.FromDate.Date
                    ).ToList();

                ViewBag.FromId = model.FromId;
                ViewBag.ToId = model.ToId;
                ViewBag.StartDate = model.FromDate.Date;
                ViewBag.FromName = (await _context.Stations.FindAsync(model.FromId)).Name;
                ViewBag.ToName = (await _context.Stations.FindAsync(model.ToId)).Name;

                return View(trains);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> FindCar(Guid fromPointId, Guid toPointId, Guid trainId)
        {
            if (ModelState.IsValid)
            {
                if (fromPointId == toPointId)
                {
                    return RedirectToAction("Index");
                }

                var train = await _context.TrainInstances.Include(e => e.Route).ThenInclude(e => e.Points).ThenInclude(e => e.Station).FirstAsync(e => e.Id == trainId);
                var cars = await _context.TrainCarInstances.Include(e => e.Car).Include(e => e.FromPoint).Include(e => e.ToPoint).Where(e => e.Train.Id == trainId).ToListAsync();

                var fromPoint = train.Route.Points.First(e => e.Id == fromPointId);
                var toPoint = train.Route.Points.First(e => e.Id == toPointId);

                cars = cars.Where(e => e.FromPoint.FromStartTime <= fromPoint.FromStartTime)
                           .Where(e => e.ToPoint.FromStartTime >= toPoint.FromStartTime)
                           .OrderBy(e => e.Number)
                           .ToList();

                ViewBag.Train = train;
                ViewBag.FromId = fromPointId;
                ViewBag.ToId = toPointId;
                ViewBag.FromName = fromPoint.Station.Name;
                ViewBag.ToName = toPoint.Station.Name;
                ViewBag.FromTime = train.GetTimeAtPoint(fromPoint);
                ViewBag.ToTime = train.GetTimeAtPoint(toPoint);

                return View(cars);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> FindPlace(Guid fromPointId, Guid toPointId, Guid carId)
        {
            if (ModelState.IsValid)
            {
                if (fromPointId == toPointId)
                {
                    return RedirectToAction("Index");
                }

                var car = await _context.CarInstances
                                        .Include(e => e.TrainCar).ThenInclude(e => e.Train).ThenInclude(e => e.Route).ThenInclude(e => e.Points).ThenInclude(e => e.Station)
                                        .Include(e => e.Places).ThenInclude(e => e.Tickets).FirstAsync(e => e.Id == carId);

                var train = car.TrainCar.Train;
                var fromPoint = train.Route.Points.First(e => e.Id == fromPointId);
                var toPoint = train.Route.Points.First(e => e.Id == toPointId);

                ViewBag.Train = train;
                ViewBag.Car = car;
                ViewBag.FromId = fromPointId;
                ViewBag.ToId = toPointId;
                ViewBag.FromPoint = fromPoint;
                ViewBag.ToPoint = toPoint;
                ViewBag.FromName = fromPoint.Station.Name;
                ViewBag.ToName = toPoint.Station.Name;
                ViewBag.FromTime = train.GetTimeAtPoint(fromPoint);
                ViewBag.ToTime = train.GetTimeAtPoint(toPoint);

                return View(car.Places.OrderBy(e => e.Number).ToList());
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> BuyTicket(Guid fromPointId, Guid toPointId, Guid placeId)
        {
            if (ModelState.IsValid)
            {
                if (fromPointId == toPointId)
                {
                    return RedirectToAction("Index");
                }

                var place = await _context.PlaceInstances
                                        .Include(e => e.Car).ThenInclude(e => e.TrainCar).ThenInclude(e => e.Train).ThenInclude(e => e.Route).ThenInclude(e => e.Points).ThenInclude(e => e.Station)
                                        .FirstAsync(e => e.Id == placeId);

                var train = place.Car.TrainCar.Train;
                var fromPoint = train.Route.Points.First(e => e.Id == fromPointId);
                var toPoint = train.Route.Points.First(e => e.Id == toPointId);

                ViewBag.Train = train;
                ViewBag.Place = place;
                ViewBag.FromId = fromPointId;
                ViewBag.ToId = toPointId;
                ViewBag.FromName = fromPoint.Station.Name;
                ViewBag.ToName = toPoint.Station.Name;
                ViewBag.FromTime = train.GetTimeAtPoint(fromPoint);
                ViewBag.ToTime = train.GetTimeAtPoint(toPoint);

                return View(new Ticket
                {
                    PlaceInstanceId = place.Id,
                    FromPointId = fromPoint.Id,
                    ToPointId = toPoint.Id
                });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> BuyTicket(Ticket ticket)
        {
            var user = await _userManager.GetUserAsync(User);
            ticket.User = user;
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "MyTickets");
        }
    }
}