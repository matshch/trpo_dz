﻿using InternationalRailwayTickets.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalRailwayTickets.Views
{
    [Authorize]
    public class MyTicketsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public MyTicketsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: MyTickets
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var result = await _context.Tickets.Where(e => e.User.Id == userId)
                .Include(e => e.FromPoint).ThenInclude(e => e.Station)
                .Include(e => e.ToPoint).ThenInclude(e => e.Station)
                .Include(e => e.PlaceInstance).ThenInclude(e => e.Car).ThenInclude(e => e.TrainCar)
                    .ThenInclude(e => e.Train).ThenInclude(e => e.Route).ThenInclude(e => e.Points).ThenInclude(e => e.Station)
                .ToListAsync();
            return View(result
                .GroupBy(e => e.PlaceInstance.Car.TrainCar.Train.Id.ToString() + e.FromPoint.Id.ToString() + e.ToPoint.Id.ToString())
                .OrderBy(e => e.First().PlaceInstance.Car.TrainCar.Train.StartDate.Add(e.First().PlaceInstance.Car.TrainCar.Train.Route.StartTime).Add(e.First().FromPoint.FromStartTime))
                .ThenBy(e => e.First().ToPoint.FromStartTime));
        }

        [HttpGet]
        public async Task<IActionResult> Pay(Guid id)
        {
            var userId = _userManager.GetUserId(User);
            var ticket = await _context.Tickets.Include(e => e.User).FirstAsync(e => e.Id == id);
            if (ticket.User.Id != userId)
            {
                return Forbid();
            }
            ticket.Paid = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}