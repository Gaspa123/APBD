using APBD3.Models;
using APBD3.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        static bool start = true;
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
            if (start)
            {
                _dbService.Db();
                start = false;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStudent()
        {
            if (_dbService.GetStudent() == null)
                return NoContent();
            return Ok(_dbService.GetStudent());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent([FromRoute] string id)
        {
            if (_dbService.GetStudent(id) == null)
                return NoContent();
            return Ok(_dbService.GetStudent(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent([FromRoute] string id, [FromBody] Student s)
        {

            if (s != null)
            {
                if((_dbService.PutStudent(id, s) == null))
                    return NotFound("Student doesnt exist");
                else
                    return Ok($"{s} update complete");
            }
            else
                return NotFound("Empty request");
        }

        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] Student s)
        {
            if (s != null)
            {
                if (_dbService.AddStudent(s) == true)
                {
                    return Ok();
                }
            }
            return BadRequest("Wrong input");
        }

        [HttpDelete("{s}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] string s)
        { 
            if (s != null)
            {
                if (!_dbService.DeleteStudent(s) == false)
                {
                    return Ok("resource deleted successfully");
                }
            }
            return NotFound("Wrong input");
        }
    }
}
