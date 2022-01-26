using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForevarLibrary.Entities
{
    public class PlaceEntity : TableEntity
    {
        public double PlaceLong { get; set; }

        public double PlaceLat { get; set; }

        public string AdministrationUnit { get; set; }

        public string PlaceId { get; set; }

        public string CityId { get; set; }
        public double CityLat { get; set; }
        public double CityLong { get; set; }


    }
}
