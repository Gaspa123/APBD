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
    [Route ("api/warehouses")]
    public class WarehousesController : ControllerBase
    {
    

        private readonly IDbService _dbService;
        public WarehousesController(IDbService idbService)
        {
            _dbService = idbService;
        }
        [HttpPost("1")]
        public async Task<IActionResult> PostItems([FromBody] Item item) 
        {
            string res = (await _dbService.AddItem(item));
            if (res == "This item id doesn't exist")
            {
                return NotFound("This item id doesn't exist");
            }
            if (res == "This warehouse id doesn't exist")
            {
                return NotFound("This item id doesn't exist");
            }
            if (res == "Invalid amount")
            {
                return NotFound("Invalid amount");
            }
            if (res == "Order not found")
            {
                return NotFound("Order not found");
            }
            if (res == "Already done")
            {
                return NotFound("Already done");
            }

            return Ok(res);
        }
    }
}
