using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AquaCultureMonitor.Core.Domain;

namespace AquaCultureMonitor.Models
{
    public class SensorModel
    {
        public Guid Id { get; set; }

        public Guid ApiKey { get; set; }
        [Required]
        [Range(1, int.MaxValue)]

        public SensorType Type { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public List<Sensor> Sensors { get; set; } 
        public List<DataReading> Readings { get; set; } 
    }

    public class DataSetModel
    {
        public Guid SensorId { get; set; }
        public decimal MetaValue { get; set; }
    }
}