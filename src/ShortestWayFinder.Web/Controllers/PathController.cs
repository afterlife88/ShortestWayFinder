using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using ShortestWayFinder.Web.Contracts;
using ShortestWayFinder.Web.Models;

namespace ShortestWayFinder.Web.Controllers
{
    [Route("api/[controller]")]
    public class PathController : Controller
    {
        private readonly IPathService _pathService;
        public PathController(IPathService pathService)
        {
            _pathService = pathService;
        }
        [Route("add")]
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
