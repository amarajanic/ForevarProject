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
    public class SearchController : ControllerBase
    {
        private string authValue = Environment.GetEnvironmentVariable("AUTH_TOKEN");
        DeviceRepository deviceRepository = new DeviceRepository();
        /// <summary>
        /// Filter devices by cityId, cityName, placeName, aUnit.
        /// </summary>
        /// <param name="cityId">CityId parameter.</param>
        /// <param name="cityName">CityName parameter.</param>
        /// <param name="placeName">PlaceName parameter.</param>
        /// <param name="aUnit">AdministrationUnit parameter.</param>
        /// <returns>ActionResult of devices.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Device>))]
        [ProducesDefaultResponseType]
        [HttpGet("")]
        public IActionResult Get([FromQuery] string cityId, [FromQuery] string cityName, [FromQuery] string placeName, [FromQuery] string aUnit)
        {
            if (Request.Headers["Authorization"].ToString() != authValue)
            {
                return Unauthorized("Access Denied!");
            }
            else
            {
                if (string.IsNullOrEmpty(cityId) && string.IsNullOrEmpty(cityName) && string.IsNullOrEmpty(placeName) &&
                string.IsNullOrEmpty(aUnit))
                {
                    try
                    {
                
                        var deviceEntities = deviceRepository.GetAll();
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
   
                        return Ok(deviceModels.OrderBy(x => int.Parse(x.DeviceId)));
                    }
                    catch (Exception err)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
                    }
                }
                else
                {
                    try
                    {

                        var deviceEntities = deviceRepository.GetAll();
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

                        var queryable = deviceModels.AsQueryable();
                        if(cityId!=null || cityName!=null)
                        queryable = queryable.Where(x => x.CityId == cityId || x.CityName == cityName);
                        if(aUnit!=null)
                        queryable = queryable.Where(x => x.AdministrationUnit == aUnit);
                        if(placeName!=null)
                        queryable = queryable.Where(x => x.PlaceName==placeName); 

                        var model = queryable.ToList();

                        if (model.Count() == 0)
                            return NotFound();
                        else
                            return Ok(model.OrderBy(x =>int.Parse(x.DeviceId)));
                    }
                    catch (Exception err)
                    {
                        return NotFound(err.Message);
                    }
                }
            }
        
        }
    }
}
