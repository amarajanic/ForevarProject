using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForevarLibrary.Models;
using ForevarLibrary.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForevarApi.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private string authValue = Environment.GetEnvironmentVariable("AUTH_TOKEN");
        DeviceRepository deviceRepository = new DeviceRepository();
        PlaceRepository placeRepository = new PlaceRepository();
  /// <summary>
  /// Get a device by cityId
  /// </summary>
  /// <param name="id">Id of a city you want to get devices with.</param>
  /// <returns>ActionResult of devices.</returns>
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Device>))]
        [ProducesDefaultResponseType]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (Request.Headers["Authorization"].ToString() != authValue)
            {
                return Unauthorized("Access Denied!");
            }
            else
            {
                try
                {
                
                    var deviceEntities = deviceRepository.GetByCityId(id.ToString());
                    var deviceModels = deviceEntities.Select(x => new Device
                    {
                        DeviceId = x.RowKey,
                        RelativeTemperature = x.RelativeTemperature,
                        RelativeHumidity = x.RelativeHumidity,
                        PlaceId = x.PlaceId,
                        PlaceName = x.PlaceName,
                        CityId = x.CityId,
                        CityName = x.CityName,
                        AdministrationUnit = x.AdministrationUnit,
                        DeviceLat = x.DeviceLat,
                        DeviceLong = x.DeviceLong

                    });

                    if (deviceModels.Count() == 0)
                        return NotFound();
                    else
                        return Ok(deviceModels.OrderBy(x=> int.Parse(x.DeviceId)));
                }
                catch (Exception err)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
                }
            }
        }
        /// <summary>
        /// Get all cities
        /// </summary>
        /// <returns>ActionResult of cities.</returns>   
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<City>))]
        [ProducesDefaultResponseType]
        [HttpGet]
        public IActionResult Get()
        {
            if (Request.Headers["Authorization"].ToString() != authValue)
            {
                return Unauthorized("Access Denied!");
            }
            {
                try
                {
                    var placeEntities = placeRepository.GetAll();

                    var cityModels = placeEntities.Select(x => new City
                    {
                        CityId = x.CityId,
                        CityName = x.PartitionKey,
                        CityLat =x.CityLat,
                        CityLong = x.CityLong
                    });

                    var cities = cityModels.GroupBy(x => x.CityId).Select(y => y.First()).ToList().OrderBy(x=> int.Parse(x.CityId));
         
                    foreach (var city in cities)
                    { 
                        city.DeviceNumber = deviceRepository.GetByCityId(city.CityId).Count();
                        city.LowestTemp = deviceRepository.GetByCityId(city.CityId).Min(x => x.RelativeTemperature);
                        city.ColdestPlace = deviceRepository.GetByCityId(city.CityId).Where(x => x.RelativeTemperature == city.LowestTemp).Select(x => x.PlaceName).First();


                    }


                    if (cities.Count() == 0)
                        return NotFound();
                    else
                        return Ok(cities);

                }
                catch (Exception err)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
                }
            }
        }

    }
}
