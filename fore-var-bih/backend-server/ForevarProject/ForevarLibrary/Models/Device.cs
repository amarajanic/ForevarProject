using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForevarLibrary.Models
{
    public class Device
    {
        /// <summary>
        /// Device Id.
        /// </summary>
        [JsonProperty("DeviceId")]
        public string DeviceId { get; set; }
        /// <summary>
        /// Relative temperature.
        /// </summary>
        [JsonProperty("RelativeTemperature")]
        public double RelativeTemperature { get; set; }
        /// <summary>
        /// Relative humidity.
        /// </summary>
        [JsonProperty("RelativeHumidity")]
        public double RelativeHumidity { get; set; }
        /// <summary>
        /// Device latitude value.
        /// </summary>
        [JsonProperty("DeviceLat")]
        public double DeviceLat { get; set; }
        /// <summary>
        /// Device longitude value.
        /// </summary>
        [JsonProperty("DeviceLong")]
        public double DeviceLong { get; set; }
        /// <summary>
        /// Place Id
        /// </summary>
        [JsonProperty("PlaceId")]
        public string PlaceId { get; set; }
        /// <summary>
        /// Name of place.
        /// </summary>
        [JsonProperty("PlaceName")]
        public string PlaceName { get; set; }
        /// <summary>
        /// City Id.
        /// </summary>
        [JsonProperty("CityId")]
        public string CityId { get; set; }
        /// <summary>
        /// Name of the city.
        /// </summary>
        [JsonProperty("CityName")]
        public string CityName { get; set; }
        /// <summary>
        /// Administration Unit.
        /// </summary>
        [JsonProperty("AdministrationUnit")]
        public string AdministrationUnit { get; set; }

        public string GetPlaceId()
        {
            return this.PlaceId;
        }


    }
}
