using System;
using CorredoresF1app_AGGP.Model;
using System.Collections.Generic;
using System.Text;

namespace CorredoresF1app_AGGP.Model
{
    public class Area
    {
        public string id { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string IdSensor { get; set; } = string.Empty;

        public SensorHumedad sensorHumedad { get; set; } = null;

        public string IdValvula { get; set; } = string.Empty;

        public ElectroValvula electroValvula { get; set; } = null;

        public string Imagen { get; set; } = string.Empty;

        public List<RiegoEvent> HistorialRiego { get; set; } = new List<RiegoEvent>();
    }
    public class AreaDTO
    {

        public string Nombre { get; set; } = string.Empty;

        public string Imagen { get; set; } = string.Empty;

        public string refSensor { get; set; } = string.Empty;

        public string refValvula { get; set; } = string.Empty;


    }
}
