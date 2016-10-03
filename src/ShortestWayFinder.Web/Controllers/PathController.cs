using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using ShortestWayFinder.Web.Contracts;
using ShortestWayFinder.Web.Exceptions;
using ShortestWayFinder.Web.Models;

namespace ShortestWayFinder.Web.Controllers
{
    /// <summary>
    /// Controller for managing paths
    /// </summary>
    [Route("api/[controller]")]
    public class PathController : Controller
    {

        private readonly IPathService _pathService;
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="pathService"></param>
        public PathController(IPathService pathService)
        {
            _pathService = pathService;
        }

        // GET api/path
        /// <summary>
        /// Returns all existed paths in database
        /// </summary>
        /// <response code="200">Return list of paths</response>
        /// <response code="500">Returns if server error has occurred</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PathDto>), 200)]
        [ProducesResponseType(typeof(InternalServerErrorResult), 500)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _pathService.GetAllExistedPathsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/path/points
        /// <summary>
        /// Returns all points of the city
        /// </summary>
        /// <response code="200">Return list of paths</response>
        /// <response code="500">Returns if server error has occurred</response>
        [Route("points")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PointDto>), 200)]
        [ProducesResponseType(typeof(InternalServerErrorResult), 500)]
        public async Task<IActionResult> GetAllPoints()
        {
            try
            {
                var result = await _pathService.GetPointsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Adds path to the application
        /// </summary>
        /// <param name="pathDto">Path data</param>
        /// <response code="201">Returns if path created successfully</response>
        /// <response code="400">Returns if path already exist or on invalid request data</response>
        /// <response code="500">Returns if server error has occurred</response>
        [Route("add")]
        [HttpPost]
        [ProducesResponseType(typeof(StatusCodeResult), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(InternalServerErrorResult), 500)]
        public async Task<IActionResult> AddPath([FromBody]PathDto pathDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _pathService.CreatePathAsync(pathDto);

                if (!result)
                    return BadRequest("This path already exist!");
                return StatusCode(201);
            }
            catch (TimeIsNotPositiveException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/path/update
        /// <summary>
        /// Updates path in application
        /// </summary>
        /// <param name="pathDto">Path data</param>
        /// <response code="204">Returns if path updated successfully</response>
        /// <response code="400">Returns on invalid request data</response>
        /// <response code="404">Returns if requested path not found</response>
        /// <response code="500">Returns if server error has occurred</response>
        [Route("update")]
        [HttpPut]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(InternalServerErrorResult), 500)]
        public async Task<IActionResult> UpdatePath([FromBody] PathDto pathDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _pathService.UpdatePathAsync(pathDto);

                if (!result)
                    return BadRequest("This path already exist!");
          
                return StatusCode(204);
            }
            catch (TimeIsNotPositiveException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        // POST api/path/shortestpath
        /// <summary>
        /// Shortest path beetween two points
        /// </summary>
        /// <remarks>
        /// 
        /// Returns array of path objects on sequence from first point to second point through other points in the shortest path (by time)
        /// 
        /// </remarks>
        /// <param name="model">Object that describe first and second points</param>
        /// <response code="200">Returns array of path objects in shortest path</response>
        /// <response code="400">Returns on invalid request data or connecting beetween points are not exist</response>
        /// <response code="404">Returns if requested points not found</response>
        /// <response code="500">Returns if server error has occurred</response>
        [Route("shortestpath")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<PathDto>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(InternalServerErrorResult), 500)]
        public async Task<IActionResult> GetShortestPath([FromBody] ShortestPathRequestDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _pathService.GetShortestPathAsync(model);

                return Ok(result);
            }
            catch (PointsNotExistException ex)
            {
                return NotFound(ex.Message);
            }
            catch (PointsNotConnectedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        // DELETE api/path/2

        /// <summary>
        /// Removes path in application
        /// </summary>
        /// <param name="id">Id of path</param>
        /// <response code="200">Returns if path deleted successfully</response>
        /// <response code="404">Returns if requested path not found</response>
        /// <response code="500">Returns if server error has occurred</response>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(StatusCodeResult), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(InternalServerErrorResult), 500)]
        public async Task<IActionResult> RemovePath(int id)
        {
            try
            {
                var result = await _pathService.RemovePathAsync(id);

                if (!result)
                    return NotFound();

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
