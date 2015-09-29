using System;
using System.Linq;
using System.Web.Mvc;
using AquaCultureMonitor.Models;
using AquaCultureMonitor.Core.Domain;
using AquaCultureMonitor.Core.Services;

namespace AquaCultureMonitor.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ISensorService _sensorService;
        private readonly IDataReadingService _dataReadingService;

        public HomeController(ISensorService sensorService, IDataReadingService dataReadingService)
        {
            _sensorService = sensorService;
            _dataReadingService = dataReadingService;
        }

        public ActionResult Index()
        {
            var model = _sensorService.GetAll().Select(sensor => new DashboardModel()
            {
                Sensor = sensor,
				Readings = _dataReadingService.GetAll(sensor.Id).OrderBy(x => x.DateTime)
            });

            return View(model);
        }

        public ActionResult Manage()
        {
            var model = new SensorModel
            {
                Sensors = _sensorService.GetAll().ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Manage(SensorModel model)
        {
            if (ModelState.IsValid)
            {
                var sensor = new Sensor()
                {
                    Type = model.Type,
                    Name = model.Name,
                    IsActive = true
                };

                _sensorService.Create(sensor);
                ViewBag.Message = "Sensor has been created successfully";
                return RedirectToAction("Manage");
            }

            model.Sensors = _sensorService.GetAll().ToList();
            return View(model);
        }

        public ActionResult Sensor(Guid id)
        {
            var sensor = _sensorService.Get(id);
            if (sensor == null) return HttpNotFound();

            var model = new SensorModel()
            {
                Id = sensor.Id,
                ApiKey = sensor.ApiKey,
                Type = sensor.Type,
                Name = sensor.Name,
                IsActive = sensor.IsActive,
                Readings = _dataReadingService.GetAll(sensor.Id).ToList()
            };

            return View(model);
        }


    }
}