using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalsController : ControllerBase
    {
        static bool start = true;
        private readonly IDbService _dbService;

        public AnimalsController(IDbService dbService)
        {
            _dbService = dbService;
            if (start)
            {
                _dbService.Db();
                start = false;
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAnimals([FromQuery] string orderBy)
        {
            if (_dbService.GetAnimal().Count == 0)
                return NoContent();
            if (orderBy == null)
            {
                return Ok(_dbService.GetAnimal());
            }

            List<string> validationList = new List<string>();
            validationList.Add("name");
            validationList.Add("description");
            validationList.Add("category");
            validationList.Add("area");

            bool flag = false;
            foreach (var item in validationList)
            {
                if (item.Equals(orderBy))
                    flag = true;
            }
            if (flag)
            {
                return Ok(_dbService.GetAnimalQuery(orderBy));
            }
            else
                return BadRequest("Invalid querystring");
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostAnimals([FromBody] Animal animal)
        {
            if (_dbService.AddAnimal(animal))
                return Ok("Successful adding");
            return BadRequest("We have this animal already");
        }

        [HttpPut("{idAnimal}")]
        public async Task<IActionResult> PutAnimals([FromBody] Animal animal,[FromRoute] int idAnimal)
        {
            if (_dbService.ModifyAnimal(animal,idAnimal))
                return Ok("Successful modificaton");
            return BadRequest("I can not modify this animal");
        }

        [HttpDelete("{idAnimal}")]
        public async Task<IActionResult> DeleteAnimals([FromRoute] int idAnimal)
        {
            if (_dbService.DeleteAnimal(idAnimal))
                return Ok("Successful delete");
            return BadRequest("I can not delete this animal");
        }
    }
}
