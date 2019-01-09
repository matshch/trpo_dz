using InternationalRailwayTickets.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalRailwayTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MockController(ApplicationDbContext context)
        {
            _context = context;
        }

        private Station ННовгород = new Station { Name = "Н.Новгород" };
        private Station Дзержинск = new Station { Name = "Дзержинск" };
        private Station Владимир = new Station { Name = "Владимир" };
        private Station Москва = new Station { Name = "Москва" };
        private Station Тверь = new Station { Name = "Тверь" };
        private Station Окуловка = new Station { Name = "Окуловка" };
        private Station Чудово = new Station { Name = "Чудово" };
        private Station СПетербур = new Station { Name = "С-Петербур" };

        // GET: api/Mock
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Station>>> Mock()
        {
            var trains = new[] {
                GenerateMegapolis019(),
                GenerateMegapolis020(),
                GenerateSapsan758(),
                GenerateSapsan771(),
                GenerateNevskiy747(),
                GenerateNevskiy748()
            };
            foreach (var train in trains)
            {
                _context.Trains.Add(train);

                foreach (var schedule in train.TrainSchedules)
                {
                    foreach (var date in schedule.Schedule.GetDates(schedule.FromDate).TakeWhile(e => e <= schedule.ToDate))
                    {
                        _context.TrainInstances.Add(train.CreateInstance(date));
                    }
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private Train GenerateMegapolis019()
        {
            var train = new Train
            {
                Number = "019У",
                Name = "Мегаполис"
            };

            var trainSchedule = new TrainSchedule
            {
                FromDate = new DateTime(2019, 1, 1),
                ToDate = new DateTime(2019, 3, 28),
                Route = new Route
                {
                    StartTime = new TimeSpan(22, 29, 0)
                },
                Schedule = new DailySchedule(new DateTime(2019, 1, 1), 1)
            };

            var trainRoute = new List<RoutePoint> {
                new RoutePoint { FromStartTime = new TimeSpan(), Station = СПетербур },
                new RoutePoint { FromStartTime = new TimeSpan(5, 33, 0), Station = Тверь },
                new RoutePoint { FromStartTime = new TimeSpan(7, 28, 0), Station = Москва }
            };
            trainRoute.ForEach(e => trainSchedule.Route.Points.Add(e));

            GenerateMegapolisCars(trainRoute).ForEach(e => trainSchedule.TrainCars.Add(e));

            train.TrainSchedules.Add(trainSchedule);

            return train;
        }

        private Train GenerateMegapolis020()
        {
            var train = new Train
            {
                Number = "020У",
                Name = "Мегаполис"
            };

            var trainSchedule = new TrainSchedule
            {
                FromDate = new DateTime(2019, 1, 2),
                ToDate = new DateTime(2019, 3, 28),
                Route = new Route
                {
                    StartTime = new TimeSpan(0, 20, 0)
                },
                Schedule = new DailySchedule(new DateTime(2019, 1, 2), 1)
            };

            var trainRoute = new List<RoutePoint> {
                new RoutePoint { FromStartTime = new TimeSpan(), Station = Москва },
                new RoutePoint { FromStartTime = new TimeSpan(1, 56, 0), Station = Тверь },
                new RoutePoint { FromStartTime = new TimeSpan(8, 39, 0), Station = СПетербур }
            };
            trainRoute.ForEach(e => trainSchedule.Route.Points.Add(e));

            GenerateMegapolisCars(trainRoute).ForEach(e => trainSchedule.TrainCars.Add(e));

            train.TrainSchedules.Add(trainSchedule);

            return train;
        }

        private List<TrainCar> GenerateMegapolisCars(List<RoutePoint> trainRoute)
        {
            var trainCars = new List<TrainCar> {
                new TrainCar { Number = 2, Car = new Car { ServiceClass = "2К" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 9, Car = new Car { ServiceClass = "2К" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 10, Car = new Car { ServiceClass = "2К" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 11, Car = new Car { ServiceClass = "2К" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 12, Car = new Car { ServiceClass = "2К" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 3, Car = new Car { ServiceClass = "2Э МЖ У1" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 6, Car = new Car { ServiceClass = "1Б У1" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 4, Car = new Car { ServiceClass = "1У У0" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
            };
            GenerateTwoFloorPlaces(trainCars[0].Car.Places, 38);
            GenerateTwoFloorPlaces(trainCars[1].Car.Places, 38);
            GenerateTwoFloorPlaces(trainCars[2].Car.Places, 38);
            GenerateTwoFloorPlaces(trainCars[3].Car.Places, 38);
            GenerateTwoFloorPlaces(trainCars[4].Car.Places, 38);
            GenerateTwoFloorPlaces(trainCars[5].Car.Places, 38);
            GenerateOneFloorPlaces(trainCars[6].Car.Places, 18);
            GenerateOneFloorPlaces(trainCars[7].Car.Places, 18);
            return trainCars;
        }

        private Train GenerateSapsan758()
        {
            var train = new Train
            {
                Number = "758*Н",
                Name = "Сапсан"
            };

            var trainSchedule = new TrainSchedule
            {
                FromDate = new DateTime(2019, 1, 7),
                ToDate = new DateTime(2019, 4, 6),
                Route = new Route
                {
                    StartTime = new TimeSpan(5, 5, 0)
                },
                Schedule = new DailySchedule(new DateTime(2019, 1, 7), 1)
            };

            var trainRoute = new List<RoutePoint> {
                new RoutePoint { FromStartTime = new TimeSpan(), Station = ННовгород },
                new RoutePoint { FromStartTime = new TimeSpan(0, 23, 0), Station = Дзержинск },
                new RoutePoint { FromStartTime = new TimeSpan(1, 59, 0), Station = Владимир },
                new RoutePoint { FromStartTime = new TimeSpan(4, 13, 0), Station = Москва },
                new RoutePoint { FromStartTime = new TimeSpan(5, 25, 0), Station = Тверь },
                new RoutePoint { FromStartTime = new TimeSpan(6, 40, 0), Station = Окуловка },
                new RoutePoint { FromStartTime = new TimeSpan(7, 22, 0), Station = Чудово },
                new RoutePoint { FromStartTime = new TimeSpan(8, 15, 0), Station = СПетербур }
            };
            trainRoute.ForEach(e => trainSchedule.Route.Points.Add(e));

            GenerateSapsanCars(trainRoute).ForEach(e => trainSchedule.TrainCars.Add(e));

            train.TrainSchedules.Add(trainSchedule);

            return train;
        }

        private Train GenerateSapsan771()
        {
            var train = new Train
            {
                Number = "771Н",
                Name = "Сапсан"
            };

            var trainSchedule = new TrainSchedule
            {
                FromDate = new DateTime(2019, 1, 7),
                ToDate = new DateTime(2019, 4, 6),
                Route = new Route
                {
                    StartTime = new TimeSpan(17, 0, 0)
                },
                Schedule = new DailySchedule(new DateTime(2019, 1, 7), 1)
            };

            var trainRoute = new List<RoutePoint> {
                new RoutePoint { FromStartTime = new TimeSpan(), Station = СПетербур },
                new RoutePoint { FromStartTime = new TimeSpan(1, 24, 0), Station = Окуловка },
                new RoutePoint { FromStartTime = new TimeSpan(2, 40, 0), Station = Тверь },
                new RoutePoint { FromStartTime = new TimeSpan(4, 8, 0), Station = Москва },
                new RoutePoint { FromStartTime = new TimeSpan(6, 6, 0), Station = Владимир },
                new RoutePoint { FromStartTime = new TimeSpan(7, 41, 0), Station = Дзержинск },
                new RoutePoint { FromStartTime = new TimeSpan(8, 3, 0), Station = ННовгород }
            };
            trainRoute.ForEach(e => trainSchedule.Route.Points.Add(e));

            GenerateSapsanCars(trainRoute).ForEach(e => trainSchedule.TrainCars.Add(e));

            train.TrainSchedules.Add(trainSchedule);

            return train;
        }

        private List<TrainCar> GenerateSapsanCars(List<RoutePoint> trainRoute)
        {
            var trainCars = new List<TrainCar> {
                new TrainCar { Number = 1, Car = new Car { ServiceClass = "1В У1" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 2, Car = new Car { ServiceClass = "1С У1" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 5, Car = new Car { ServiceClass = "2Е У1" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 10, Car = new Car { ServiceClass = "2В Д У1" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 3, Car = new Car { ServiceClass = "2С Ж У0" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 6, Car = new Car { ServiceClass = "2С У0" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 7, Car = new Car { ServiceClass = "2С У0" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 8, Car = new Car { ServiceClass = "2С Ж У0" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 9, Car = new Car { ServiceClass = "2С У0" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
            };
            GenerateSapsanFirstPlaces(trainCars[0].Car.Places);
            GenerateOneFloorPlaces(trainCars[1].Car.Places, 52);
            GenerateOneFloorPlaces(trainCars[2].Car.Places, 40);
            GenerateOneFloorPlaces(trainCars[3].Car.Places, 52);
            trainCars[3].Car.Places.Remove(trainCars[3].Car.Places.First(e => e.Number == 50));
            GenerateOneFloorPlaces(trainCars[4].Car.Places, 66);
            GenerateOneFloorPlaces(trainCars[5].Car.Places, 56);
            GenerateOneFloorPlaces(trainCars[6].Car.Places, 66);
            GenerateOneFloorPlaces(trainCars[7].Car.Places, 66);
            GenerateOneFloorPlaces(trainCars[8].Car.Places, 66);
            return trainCars;
        }

        private Train GenerateNevskiy747()
        {
            var train = new Train
            {
                Number = "747А",
                Name = "Невский экспресс"
            };

            var trainSchedule = new TrainSchedule
            {
                FromDate = new DateTime(2019, 1, 7),
                ToDate = new DateTime(2019, 4, 5),
                Route = new Route
                {
                    StartTime = new TimeSpan(13, 10, 0)
                },
                Schedule = new WeeklySchedule(new DateTime(2019, 1, 7), new[] {
                    DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                    DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Sunday
                })
            };
            trainSchedule.Schedule.IncludedDates.Add(new DateTime(2019, 3, 9));
            trainSchedule.Schedule.ExcludedDates.Add(new DateTime(2019, 3, 8));

            var trainRoute = new List<RoutePoint> {
                new RoutePoint { FromStartTime = new TimeSpan(), Station = СПетербур },
                new RoutePoint { FromStartTime = new TimeSpan(4, 5, 0), Station = Москва }
            };
            trainRoute.ForEach(e => trainSchedule.Route.Points.Add(e));

            GenerateNevskiyCars(trainRoute).ForEach(e => trainSchedule.TrainCars.Add(e));

            train.TrainSchedules.Add(trainSchedule);

            return train;
        }

        private Train GenerateNevskiy748()
        {
            var train = new Train
            {
                Number = "748А",
                Name = "Невский экспресс"
            };

            var trainSchedule = new TrainSchedule
            {
                FromDate = new DateTime(2019, 1, 10),
                ToDate = new DateTime(2019, 4, 7),
                Route = new Route
                {
                    StartTime = new TimeSpan(13, 40, 0)
                },
                Schedule = new DailySchedule(new DateTime(2019, 1, 10), 1)
            };
            trainSchedule.Schedule.ExcludedDates.Add(new DateTime(2019, 3, 9));

            var trainRoute = new List<RoutePoint> {
                new RoutePoint { FromStartTime = new TimeSpan(), Station = Москва },
                new RoutePoint { FromStartTime = new TimeSpan(4, 5, 0), Station = СПетербур }
            };
            trainRoute.ForEach(e => trainSchedule.Route.Points.Add(e));

            GenerateNevskiyCars(trainRoute).ForEach(e => trainSchedule.TrainCars.Add(e));

            train.TrainSchedules.Add(trainSchedule);

            return train;
        }

        private List<TrainCar> GenerateNevskiyCars(List<RoutePoint> trainRoute)
        {
            var trainCars = new List<TrainCar> {
                new TrainCar { Number = 2, Car = new Car { ServiceClass = "1Ж Ж У1" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 11, Car = new Car { ServiceClass = "1Ж Ж У1" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 3, Car = new Car { ServiceClass = "1Р У1" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 4, Car = new Car { ServiceClass = "1Р У1" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 5, Car = new Car { ServiceClass = "1Р Д У1" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 6, Car = new Car { ServiceClass = "1Р У1" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 8, Car = new Car { ServiceClass = "1Р У1" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
                new TrainCar { Number = 9, Car = new Car { ServiceClass = "1Р У1" }, FromPoint = trainRoute.First(), ToPoint = trainRoute.Last() },
            };
            GenerateOneFloorPlaces(trainCars[0].Car.Places, 48);
            GenerateOneFloorPlaces(trainCars[1].Car.Places, 48);
            GenerateOneFloorPlaces(trainCars[2].Car.Places, 48);
            GenerateOneFloorPlaces(trainCars[3].Car.Places, 48);
            GenerateOneFloorPlaces(trainCars[4].Car.Places, 38);
            GenerateOneFloorPlaces(trainCars[5].Car.Places, 48);
            GenerateOneFloorPlaces(trainCars[6].Car.Places, 48);
            GenerateOneFloorPlaces(trainCars[7].Car.Places, 48);
            return trainCars;
        }

        private void GenerateTwoFloorPlaces(ICollection<Place> places, uint limit)
        {
            for (var i = 1u; i <= limit; ++i)
            {
                places.Add(new Place { Number = i, Level = (i % 2 == 0) ? 2u : 1u, Floor = 1 });
            }
        }

        private void GenerateOneFloorPlaces(ICollection<Place> places, uint limit)
        {
            for (var i = 1u; i <= limit; ++i)
            {
                places.Add(new Place { Number = i, Level = 1, Floor = 1 });
            }
        }

        private void GenerateSapsanFirstPlaces(ICollection<Place> places)
        {
            for (var i = 1u; i <= 30; ++i)
            {
                if ((i <= 20 && i % 4 == 0) || i == 22 || i == 26)
                {
                    continue;
                }
                places.Add(new Place { Number = i, Level = 1, Floor = 1 });
            }
        }
    }
}
