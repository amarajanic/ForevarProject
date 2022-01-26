using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForevarLibrary.DataAccess;
using ForevarLibrary.Entities;
using ForevarLibrary.Models;
using ForevarLibrary.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForevarApi.Controllers
{
    [Route("api/place/{PlaceId}/[controller]")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private string authValue = Environment.GetEnvironmentVariable("AUTH_TOKEN");
        DeviceRepository repository = new DeviceRepository();
        /// <summary>
        /// Get all devices by placeId.
        /// </summary>
        /// <param name="PlaceId">Id of a place you want get devices with.</param>
        /// <returns>ActionResult of devices.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Device>))]
        [ProducesDefaultResponseType]
        [HttpGet]
        public IActionResult Get(string PlaceId)
        {
            if (Request.Headers["Authorization"].ToString() != authValue)
            {
                return Unauthorized("Access Denied!");
            }
            else
            {
                try
                {
                  var entities = repository.GetByPlaceId(PlaceId);

                  var models = entities.Select(x => new Device
                  {
                      DeviceId = x.DeviceId,
                      RelativeTemperature =x.RelativeTemperature,
                      RelativeHumidity = x.RelativeHumidity,
                      PlaceId = x.PlaceId,
                      PlaceName = x.PlaceName,
                      CityId = x.CityId,
                      CityName = x.CityName,
                      AdministrationUnit = x.AdministrationUnit,
                      DeviceLat = x.DeviceLat,
                      DeviceLong = x.DeviceLong

                  });

                    if (models.Count() == 0)
                        return NotFound();
                    else
                        return Ok(models.OrderBy(x => int.TryParse(x.DeviceId, out int result)));
                }
                catch (Exception err)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, err.Message);

                }
            }
           
        }
        /// <summary>
        /// Get devices by placeId and deviceId.
        /// </summary>
        /// <param name="PlaceId">PlaceId parameter.</param>
        /// <param name="DeviceId">DeviceId parameter.</param>
        /// <returns>ActionResult of devices.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Device))]
        [ProducesDefaultResponseType]
        [HttpGet("{DeviceId}")]
        public IActionResult Get(int PlaceId,string DeviceId)
        {
            if (Request.Headers["Authorization"].ToString() != authValue)
            {
                return Unauthorized("Access Denied!");

            }
            else
            {
                try
                {
                    var entities = repository.GetByPlaceId(PlaceId.ToString());

                    if (!entities.Any(x => x.DeviceId == DeviceId))
                    {
                        return NotFound();
                    }

                    var models = entities.Select(x => new Device
                    {
                        DeviceId = x.DeviceId,
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

                    if (models.Count() == 0)
                        return NotFound();
                    else
                        return Ok(models.OrderBy(x => int.TryParse(x.DeviceId, out int result)));
                }
                catch (Exception err)
                {
                    return NotFound(err.Message);

                }
            }

        }
        /// <summary>
        /// Post a device by its model and placeId.
        /// </summary>
        /// <param name="model">Device model.</param>
        /// <param name="PlaceId">PlaceId parameter.</param>
        /// <returns>ActionResult with status code and message.</returns>
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        [HttpPost]
        public IActionResult Post([FromBody] Device model, string PlaceId)
        {
            if (Request.Headers["Authorization"].ToString() != authValue)
            {
                return Unauthorized("Access Denied!");

            }
            else
            {
                var entities = repository.GetAll();

                if (!entities.Any(x => x.PlaceId.ToString() == PlaceId))
                {
                    return StatusCode(StatusCodes.Status405MethodNotAllowed);
                }

                try
                {
                    repository.Create(new DeviceEntity
                    {
                        PartitionKey = model.DeviceId,
                        RowKey = model.DeviceId,
                        DeviceId = model.DeviceId,
                        PlaceId = PlaceId,
                        RelativeHumidity = model.RelativeHumidity,
                        RelativeTemperature = model.RelativeTemperature,
                        PlaceName = model.PlaceName,
                        AdministrationUnit = model.AdministrationUnit,
                        CityId = model.CityId,
                        CityName = model.CityName,
                        DeviceLong = model.DeviceLong,
                        DeviceLat = model.DeviceLat
                    });
                    return StatusCode(StatusCodes.Status201Created,"Object created!");
                }
                catch (Exception err)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, err.Message);

                }
            }
        }

    }
}
