using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForevarLibrary.Models
{
    public class City
    {
        /// <summary>
        /// City Id.
        /// </summary>
        [JsonProperty("CityId")]
        public string CityId { get; set; }
        /// <summary>
        /// Name of city.
        /// </summary>
        [JsonProperty("CityName")]
        public string CityName { get; set; }
        /// <summary>
        /// City longitude value.
        /// </summary>
        [JsonProperty("CityLong")]
        public double CityLong { get; set; }
        /// <summary>
        /// City latitude value.
        /// </summary>
        [JsonProperty("CityLat")]
        public double CityLat { get; set; }
        /// <summary>
        /// Number of devices.
        /// </summary>
        [JsonProperty("DeviceNumber")]
        public int DeviceNumber { get; set; }
        /// <summary>
        /// Device with lowest temperature measured.
        /// </summary>
        [JsonProperty("LowestTemp")]
        public double LowestTemp { get; set; }
        /// <summary>
        /// Coldest place in the city.
        /// </summary>
        [JsonProperty("ColdestPlace")]
        public string ColdestPlace { get; set; }
    }
}
