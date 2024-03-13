using System;
using System.Collections.Generic;
using System.Text;

namespace CorredoresF1app_AGGP.Model
{
    public class SensorHumedad
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int ValorActualHumedad { get; set; }
    }

}
