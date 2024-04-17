using System;
using CorredoresF1app_AGGP.Model;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CorredoresF1app_AGGP.Model
{
    public class Area
    {


        public string id { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Imagen { get; set; } = string.Empty;

        public string IdSensor { get; set; } = string.Empty;

        public SensorHumedad SensorHumedad { get; set; }


        public string IdValvula { get; set; } = string.Empty;

        public ElectroValvula valvula { get; set; }

        public string STATUS {  get; set; } = string.Empty;

        public List<RiegoEvent> HistorialRiego { get; set; } = new List<RiegoEvent>();
        public bool Modo {  get; set; }
    }

    public class AreaDTO
    {

        public string Nombre { get; set; } = string.Empty;

        public string Imagen { get; set; } = string.Empty;

        public string refSensor { get; set; } = string.Empty;

        public string refValvula { get; set; } = string.Empty;


    }

    public class AreaStatus
    {
        public string id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string EstadoElectroValvulaText { get; set; } = string.Empty;

    }

    public class AreaUpdateDTO
    {
        public string id { get; set; } = string.Empty;
        public bool Status { get; set; }

    }
}
