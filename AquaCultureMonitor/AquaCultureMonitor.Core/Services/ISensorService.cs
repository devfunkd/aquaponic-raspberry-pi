using System;
using System.Collections.Generic;
using AquaCultureMonitor.Core.Domain;

namespace AquaCultureMonitor.Core.Services
{
    public interface ISensorService
    {
        IEnumerable<Sensor> GetAll();
        IEnumerable<Sensor> GetAll(Guid apiKey);
        Sensor Get(Guid id);
        Guid Create(Sensor sensor);
        Guid Update(Sensor sensor);
        void Delete(Sensor sensor);
    }
}