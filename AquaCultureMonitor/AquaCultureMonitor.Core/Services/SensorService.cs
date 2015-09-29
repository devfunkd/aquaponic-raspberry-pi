using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaCultureMonitor.Core.Domain;
using AquaCultureMonitor.Core.Repositories;

namespace AquaCultureMonitor.Core.Services
{
    public class SensorService : ISensorService
    {
        private readonly IRepository<Sensor> _sensorRepository;

        public SensorService(IRepository<Sensor> sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        public IEnumerable<Sensor> GetAll()
        {
            return _sensorRepository.Get();
        }

        public IEnumerable<Sensor> GetAll(Guid apiKey)
        {
            return _sensorRepository.FindAll(x => x.ApiKey == apiKey);
        }

        public Sensor Get(Guid id)
        {
            return _sensorRepository.Find(x => x.Id == id);
        }

        public Guid Create(Sensor sensor)
        {
            _sensorRepository.Add(sensor);
            return sensor.Id;
        }

        public Guid Update(Sensor sensor)
        {
            _sensorRepository.Update(sensor);
            return sensor.Id;
        }

        public void Delete(Sensor sensor)
        {
            _sensorRepository.Remove(sensor);
        }
    }
}
