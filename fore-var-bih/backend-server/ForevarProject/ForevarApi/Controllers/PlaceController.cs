using ForevarLibrary.DataAccess;
using ForevarLibrary.Entities;
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
    public class PlaceController : ControllerBase
    {
        private string authValue = Environment.GetEnvironmentVariable("AUTH_TOKEN");
        PlaceRepository placeRepository = new PlaceRepository();
        /// <summary>
        /// Get all places.
        /// </summary>
        /// <returns>ActionResult of all places.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Place>))]
        [ProducesDefaultResponseType]
        [HttpGet]
        public IActionResult Get()
        {
            if (Request.Headers["Authorization"].ToString() != authValue)
            {
                return Unauthorized("Access Denied!");
            }
            else
            {
                try
                {              
                    var placeEntities = placeRepository.GetAll();
                    var placeModels = placeEntities.Select(x => new Place
                    {
                        PlaceId = x.PlaceId,
                        PlaceName = x.RowKey,
                        PlaceLat = x.PlaceLat,
                        PlaceLong = x.PlaceLong,
                        AdministrationUnit = x.AdministrationUnit,
                        CityId = x.CityId,
                        CityName = x.PartitionKey
                    });

                    return Ok(placeModels.OrderBy(x => x.CityName));
                }
                catch (Exception err)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, err.Message);

                }
            }
        }
        /// <summary>
        /// Get place by id.
        /// </summary>
        /// <param name="id">PlaceId parameter.</param>
        /// <returns>ActionResult of place.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Place))]
        [ProducesDefaultResponseType]
        [HttpGet("{id:int}")]
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
                    var placeEntities = placeRepository.GetById(id.ToString());
                    var placeModels = placeEntities.Select(x => new Place
                    {
                        PlaceId = x.PlaceId,
                        PlaceName = x.RowKey,
                        PlaceLat = x.PlaceLat,
                        PlaceLong = x.PlaceLong,
                        AdministrationUnit = x.AdministrationUnit,
                        CityId = x.CityId,
                        CityName = x.PartitionKey,                 
                    });

                    if (placeModels.Count() == 0)
                        return NotFound();
                    else
                        return Ok(placeModels.OrderBy(x => int.Parse(x.PlaceId)));
                }
                catch (Exception err)
                {
                    return NotFound(err.Message);
                }
            }
        }
        /// <summary>
        /// Get place by placename.
        /// </summary>
        /// <param name="placeName">PlaceName parameter.</param>
        /// <returns>ActionResult of place.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Place))]
        [ProducesDefaultResponseType]
        [HttpGet("{placeName}")]
        public IActionResult Get(string placeName)
        {
            if (Request.Headers["Authorization"].ToString() != authValue)
            {
                return Unauthorized("Access Denied!");
            }
            else
            {

                try
                {
                    var placeEntities = placeRepository.GetByPlaceName(placeName);
                    var placeModels = placeEntities.Select(x => new Place
                    {
                        PlaceId = x.PlaceId,
                        PlaceName = x.RowKey,
                        PlaceLat = x.PlaceLat,
                        PlaceLong = x.PlaceLong,
                        AdministrationUnit = x.AdministrationUnit,
                        CityId = x.CityId,
                        CityName = x.PartitionKey
                    });

                    if (placeModels.Count() == 0)
                        return NotFound();
                    else
                        return Ok(placeModels.OrderBy(x =>int.Parse(x.PlaceId)));
                }
                catch (Exception err)
                {
                    return NotFound(err.Message);
                }
            }
        }
        /// <summary>
        /// Post a place model.
        /// </summary>
        /// <param name="model">Model of a place.</param>
        /// <returns>ActionResult with status code and message.</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        [HttpPost]
        public IActionResult Post([FromBody] Place model)
        {
            if (Request.Headers["Authorization"].ToString() != authValue)
            {
                return Unauthorized("Access Denied!");
            }
            else
            {
                try
                {
                    placeRepository.Create(new PlaceEntity
                    {
                        PartitionKey = model.CityName,
                        RowKey = model.PlaceName,
                        PlaceId = model.PlaceId,
                        PlaceLat = model.PlaceLat,
                        PlaceLong = model.PlaceLong,
                        AdministrationUnit = model.AdministrationUnit,
                        CityId = model.CityId
                    });
                    return StatusCode(StatusCodes.Status201Created, "Object created!");
                }
                catch (Exception err)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, err.Message);
                }
            }
        }

    }
}
