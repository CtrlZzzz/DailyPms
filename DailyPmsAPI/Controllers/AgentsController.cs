using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Data;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc;

namespace DailyPmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController: ControllerBase
    {
        readonly IAgentRepository agentRepository;
        

        public AgentsController(IAgentRepository agentRepo)
        {
            agentRepository = agentRepo;
        }

        //[HttpGet("ByCenter/{centerId:length(24)}")]
        //public async Task<ActionResult<IEnumerable<Agent>>> GetAllAgentsByCenterAsync(string centerId)
        //{

        //} 
    }
}
