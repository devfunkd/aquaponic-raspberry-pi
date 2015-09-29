using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AquaCultureMonitor.Core.Repositories;
using AquaCultureMonitor.Hubs;
using AquaCultureMonitor.Models;
using AquaCultureMonitor.Core.Domain;
using AquaCultureMonitor.Core.Services;

namespace AquaCultureMonitor.Controllers
{
    [RoutePrefix("sensor")]
    public class SensorController : ApiController
    {
        private readonly ISensorService _sensorService;
        private readonly IDataReadingService _sensorDataService;

        public SensorController(ISensorService sensorService, IDataReadingService sensorDataService)
        {
            _sensorService = sensorService;
            _sensorDataService = sensorDataService;
        }

        [HttpGet]
        [Route("list/{apiKey}")]
        public HttpResponseMessage List(string apiKey)
        {
            var sensors = _sensorService.GetAll(new Guid(apiKey));
            return (sensors == null) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse(HttpStatusCode.OK, sensors);
        }

        [HttpGet]
        [Route("{sensorId}")]
        public HttpResponseMessage Get(string sensorId)
        {
            var sensor = _sensorService.Get(new Guid(sensorId));
            return (sensor == null) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse(HttpStatusCode.OK, sensor);
        }

        [HttpPut]
        [Route("create")]
        public HttpResponseMessage Create(SensorModel model)
        {
            if (ModelState.IsValid)
            {
                var sensor = new Sensor()
                {
                    ApiKey = model.ApiKey,
                    Type = model.Type,
                    Name = model.Name,
                    IsActive = true
                };

                _sensorService.Create(sensor);
                return Request.CreateResponse(HttpStatusCode.OK, sensor);

            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "[ERROR]: Sensor could not be added because there was missing or invalid data provided.");
        }

        [HttpPost]
        [Route("{sensorId}/reading")]
        public HttpResponseMessage Create(DataSetModel model)
        {
            if (ModelState.IsValid)
            {
                var reading = new DataReading()
                {
                    SensorId = model.SensorId,
                    DateTime = DateTime.Now.AddHours(1),
                    MetaValue = model.MetaValue
                };

                _sensorDataService.Create(reading);
                SensorHub.Update(reading.SensorId, string.Format("{0:0.00}{1}", reading.MetaValue, reading.Sensor.Symbol), reading.DateTime.ToString("MMM dd, yyyy h:mmtt"));
                return Request.CreateResponse(HttpStatusCode.OK, reading);

            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "[ERROR]: Data reading could not be added because there was missing or invalid data provided.");
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(SensorModel model)
        {
            if (ModelState.IsValid)
            {
                var sensor = _sensorService.Get(model.Id);
                if (sensor == null) return Request.CreateResponse(HttpStatusCode.NotFound);

                sensor.Name = model.Name;
                sensor.Type = model.Type;
                sensor.IsActive = sensor.IsActive;

                _sensorService.Update(sensor);
                return Request.CreateResponse(HttpStatusCode.OK, sensor);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "[ERROR]: Sensor could not be updated because there was missing or invalid data provided.");
        }

        [HttpGet]
        [Route("data/{sensorId}")]
        public HttpResponseMessage Data(string sensorId)
        {
            var data = _sensorDataService.GetAll(new Guid(sensorId)).OrderBy(x => x.DateTime).Select(o => new
            {
                label = o.DateTime.ToString("MM-dd-yy h:mmtt"),
                y = o.MetaValue
            });

            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("{sensorId}/status")]
        public HttpResponseMessage Status(string sensorId)
        {
            if (!string.IsNullOrEmpty(sensorId))
            {
                var sensor = _sensorService.Get(new Guid(sensorId));
                if (sensor == null) return Request.CreateResponse(HttpStatusCode.NotFound);

				var dataReadings = _sensorDataService.GetAll(new Guid(sensorId)).OrderBy(x => x.DateTime).ToList();

                if (dataReadings.Any())
                {
                    var hours = (DateTime.Now - dataReadings.Last().DateTime).TotalHours;
                    if (hours <= 2) return Request.CreateResponse(HttpStatusCode.OK, new { status = "sensor online" }); 
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { status = "communication error" });
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = "sensor not found" });
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(Guid sensorId)
        {
            var sensor = _sensorService.Get(sensorId);
            if (sensor == null) return Request.CreateResponse(HttpStatusCode.NotFound);
            
            _sensorService.Delete(sensor);
            return Request.CreateResponse(HttpStatusCode.OK, sensor);
        }
    }
}
