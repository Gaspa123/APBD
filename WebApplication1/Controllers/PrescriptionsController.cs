using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTOs.Req;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/prescriptions")]
    public class PrescriptionsController : ControllerBase
    {

        private readonly IDbService _dbService;
        public static bool HasAccess = false;

        public PrescriptionsController(IDbService dbService)
        {
            _dbService = dbService;
        }
     
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescriptionController([FromRoute]int id)
        {
            var res = await _dbService.GetPrescription(id);
            if (res == null)
                return BadRequest();
            return Ok(res);
        }
    }
}
