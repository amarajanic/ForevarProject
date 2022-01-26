using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForevarLibrary.Models
{
    public class Place
    {
        /// <summary>
        /// Place Id.
        /// </summary>
        [JsonProperty("PlaceId")]
        public string PlaceId { get; set; }
        /// <summary>
        /// Name of a place.
        /// </summary>

        [JsonProperty("PlaceName")]
        public string PlaceName { get; set; }
        /// <summary>
        /// Place longitude value.
        /// </summary>
        [JsonProperty("PlaceLong")]
        public double PlaceLong { get; set; }
        /// <summary>
        /// Place latitude value.
        /// </summary>
        [JsonProperty("PlaceLat")]
        public double PlaceLat { get; set; }
        /// <summary>
        /// Place Administration Unit.
        /// </summary>
        [JsonProperty("AdministrationUnit")]
        public string AdministrationUnit { get; set; }
        /// <summary>
        /// City Id.
        /// </summary>
        [JsonProperty("CityId")]
        public string CityId { get; set; }
        /// <summary>
        /// City name.
        /// </summary>
        [JsonProperty("CityName")]
        public string CityName { get; set; }

    }
}
