using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForevarLibrary.Entities
{
    public class DeviceEntity : TableEntity
    {
        public double RelativeTemperature { get; set; }
        public double RelativeHumidity { get; set; }
        public string PlaceId { get; set; }
        public string DeviceId { get; set; }
        public string PlaceName { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string AdministrationUnit { get; set; }
        public double DeviceLong { get; set; }
        public double  DeviceLat { get; set; }

    }
}
