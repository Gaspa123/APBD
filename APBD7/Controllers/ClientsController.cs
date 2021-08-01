using APBD7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD7
{
    [ApiController]
    [Route("api/clients")]
    
    public class ClientsController : ControllerBase
    {
        private readonly IRepository _dbService;
        public ClientsController(IRepository idbService)
        {
            _dbService = idbService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClients([FromRoute] int id)
        {
            
            await _dbService.DeleteClientsAsync(id);
            return Ok("Successful delete");
        }
    }
}
