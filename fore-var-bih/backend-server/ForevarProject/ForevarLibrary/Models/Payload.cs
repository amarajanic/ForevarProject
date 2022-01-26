using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForevarLibrary.Models
{
    public class Payload
    {
        /// <summary>
        /// Payload Id.
        /// </summary>
        [JsonProperty("Id")]
        public string Id { get; set; }
        /// <summary>
        /// Payload temperature.
        /// </summary>
        [JsonProperty("Temperature")]
        public double Temperature { get; set; }
        /// <summary>
        /// Payload humidity.
        /// </summary>
        [JsonProperty("Humidity")]
        public double Humidity { get; set; }
        /// <summary>
        /// Payload latitude value.
        /// </summary>
        [JsonProperty("Lat")]
        public double Lat { get; set; }
        /// <summary>
        /// Payload longitude value.
        /// </summary>
        [JsonProperty("Long")]
        public double Long { get; set; }
        /// <summary>
        /// Device Id.
        /// </summary>
        [JsonProperty("DeviceId")]
        public string DeviceId { get; set; }
        /// <summary>
        /// Day
        /// </summary>
        [JsonProperty("Day")]
        public int Day { get; set; }
        /// <summary>
        /// Month
        /// </summary>
        [JsonProperty("Month")]
        public int Month { get; set; }
        /// <summary>
        /// Year
        /// </summary>
        [JsonProperty("Year")]
        public int Year { get; set; }
        /// <summary>
        /// Hour
        /// </summary>
        [JsonProperty("Hour")]
        public int Hour { get; set; }
        /// <summary>
        /// Date and time of device measurement.
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// City of device measurement.
        /// </summary>
        public string CityName { get; set; }

    }
}
