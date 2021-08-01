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
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public DoctorsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> ShowDoctorsController()
        {
            return Ok(await _dbService.GetDoctors());
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctorController([FromBody] AddDoctorReq doctor)
        {
            if (!await _dbService.AddDoctor(doctor))
                return Forbid();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyDoctorController([FromBody] ModifyDoctorReq doctor, [FromRoute] int id)
        {
            if (!await _dbService.ModifyDoctor(doctor, id))
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ShowDoctorsController([FromRoute] int id)
        {
            if (!await _dbService.DeleteDoctor(id))
                return BadRequest();
            return Ok();
        }

    }
}
