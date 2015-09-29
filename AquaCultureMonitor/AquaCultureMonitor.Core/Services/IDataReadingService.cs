using System;
using System.Collections.Generic;
using AquaCultureMonitor.Core.Domain;

namespace AquaCultureMonitor.Core.Services
{
    public interface IDataReadingService
    {
        IEnumerable<DataReading> GetAll();
        IEnumerable<DataReading> GetAll(Guid sensorId);
        DataReading Get(Guid id);
        void Create(DataReading dataReading);
        void Update(DataReading dataReading);
        void Delete(DataReading dataReading);
    }
}