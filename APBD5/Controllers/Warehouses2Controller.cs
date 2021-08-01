using APBD5.Models;
using APBD5.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD5.Controllers
{
    [ApiController]
    [Route("api/warehouses")]
    public class WarehousesController2 : ControllerBase
    {


        private readonly IDbService _dbService;
        public WarehousesController2(IDbService idbService)
        {
            _dbService = idbService;
        }
        [HttpPost("2")]
        public async Task<IActionResult> PostItems([FromBody] Item item)
        {
            return Ok(await _dbService.AddItem2(item));
        }
    }
}
