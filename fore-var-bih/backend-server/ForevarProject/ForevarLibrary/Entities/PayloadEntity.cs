using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForevarLibrary.Entities
{
    public class PayloadEntity : TableEntity
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string DeviceId { get; set; }
        public string CitnyName { get; set; }

    }
}
