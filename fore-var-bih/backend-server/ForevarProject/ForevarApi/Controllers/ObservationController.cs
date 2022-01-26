using ForevarLibrary.Models;
using ForevarLibrary.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForevarApi.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [ApiController]
    public class ObservationController : ControllerBase
    {
        private string authValue = Environment.GetEnvironmentVariable("AUTH_TOKEN");
        PayloadRepository payloadRepository = new PayloadRepository();
        DeviceRepository deviceRepository = new DeviceRepository();
        /// <summary>
        /// Get all measurements of device by it's id and sort type.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sortBy"></param>
        /// <returns>ActionResult of observations.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Payload>))]
        [ProducesDefaultResponseType]
        [HttpGet("")]
        public IActionResult Get([FromQuery]string id, [FromQuery]string sortBy)
        {
            if (Request.Headers["Authorization"].ToString() != authValue)
            {
                return Unauthorized("Access Denied!");
            }
            else
            {
                try
                {
                    var payloadEntities = payloadRepository.GetByDeviceId(id);
                    var cityName = deviceRepository.GetByDeviceId(id).First().CityName;
                    

                    var models = payloadEntities.Select(x => new Payload
                    {
                        Id = x.RowKey,
                        Temperature = x.Temperature,
                        Humidity = x.Humidity,
                        Lat = x.Lat,
                        Long = x.Long,
                        DeviceId = x.DeviceId,
                        Day = x.Timestamp.Day,
                        Month = x.Timestamp.Month,
                        Year = x.Timestamp.Year,
                        Hour = x.Timestamp.Hour,
                        DateTime = new DateTime(x.Timestamp.Year, x.Timestamp.Month, x.Timestamp.Day, x.Timestamp.Hour, x.Timestamp.Minute, x.Timestamp.Second),
                        CityName = cityName
                    });

                    if (models.Count() == 0)
                        return NotFound();
                    else
                    {
                        switch (sortBy)
                        {
                            case "day":
                                return Ok(models.OrderBy(x => x.DateTime).GroupBy(x => x.DateTime).Select(g => g.First()));
                            case "d":
                                return Ok(models.OrderBy(x => x.DateTime).GroupBy(x => x.DateTime).Select(g => g.First()));
                            case "month":
                                return Ok(models.OrderBy(x => x.DateTime.Month).GroupBy(x => x.DateTime).Select(g => g.First()));
                            case "m":
                                return Ok(models.OrderBy(x => x.DateTime.Month).GroupBy(x => x.DateTime).Select(g => g.First()));
                            default:
                                return StatusCode(StatusCodes.Status400BadRequest);
                        }

                    }
                }
                catch(Exception err)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
                }
            }
        }
    }
   
}

