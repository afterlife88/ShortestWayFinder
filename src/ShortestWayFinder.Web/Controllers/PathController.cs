﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using ShortestWayFinder.Web.Contracts;
using ShortestWayFinder.Web.Exceptions;
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

        // GET api/path
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
        // POST api/path/add
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
        // POST api/path/shortestpath

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
