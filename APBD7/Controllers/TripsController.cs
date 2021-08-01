using APBD7.DTOs;
using APBD7.Models;
using APBD7.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD7.Controllers
{

    [ApiController]
    [Route("api/trips")]
    public class TripsController : ControllerBase
    {

        private readonly IRepository _dbService;
        public TripsController(IRepository idbService)
        {
            _dbService = idbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var res = await _dbService.GetTripsAsync();
            return Ok(res);
        }

        [HttpPost("{id}/clients")]
        public async Task<IActionResult> PostTrips([FromRoute] int id,[FromBody] CreateClientAndTrip client)
        {
            await _dbService.AddClientsAsync(id, client);
            return Ok("Succeessful add");
        }
    }
}
