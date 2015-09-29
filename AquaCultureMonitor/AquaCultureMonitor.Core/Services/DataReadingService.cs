using System;
using System.Collections.Generic;
using System.Linq;
using AquaCultureMonitor.Core.Domain;
using AquaCultureMonitor.Core.Repositories;

namespace AquaCultureMonitor.Core.Services
{
    public class DataReadingService : IDataReadingService
    {
        private readonly IRepository<DataReading> _dataRepository;
        private readonly IRepository<Sensor> _sensorRepository;

        public DataReadingService(IRepository<DataReading> dataRepository, IRepository<Sensor> sensorRepository)
        {
            _dataRepository = dataRepository;
            _sensorRepository = sensorRepository;
        }

        public IEnumerable<DataReading> GetAll()
        {
            return _dataRepository.Get();
        }

        public IEnumerable<DataReading> GetAll(Guid sensorId)
        {
            return _dataRepository.FindAll(x => x.SensorId == sensorId);
        }

        public DataReading Get(Guid id)
        {
            return _dataRepository.Find(x => x.Id == id);
        }

        public void Create(DataReading dataReading)
        {
            if (!SensorExists(dataReading.SensorId)) return;
            _dataRepository.Add(dataReading);
        }

        public void Update(DataReading dataReading)
        {
            if (!SensorExists(dataReading.SensorId)) return;
            _dataRepository.Update(dataReading);
        }

        public void Delete(DataReading dataReading)
        {
            if (!SensorExists(dataReading.SensorId)) return;
            _dataRepository.Remove(dataReading);
        }

        private bool SensorExists(Guid sensorId)
        {
            var sensor = _sensorRepository.Find(x => x.Id == sensorId);
            return sensor != null;
        }

        private void Purge(int numDays)
        {
            var purgeList = _dataRepository.FindAll(x => x.DateTime <= DateTime.Now.AddDays(numDays));

            foreach (var reading in purgeList)
            {
                Delete(reading);
            }
        }
    }
}
