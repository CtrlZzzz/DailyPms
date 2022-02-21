using Azure;
using DailyPmsAPI.Data;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController: ControllerBase
    {
        readonly IAgentRepository agentRepository;
        readonly IPmsCenterRepository pmsCenterRepository;
        

        public AgentsController(IAgentRepository agentRepo, IPmsCenterRepository centerRepo)
        {
            agentRepository = agentRepo;
            pmsCenterRepository = centerRepo;
        }

        /// <summary>
        /// Get a list of all agents from one pms center
        /// </summary>
        /// <param name="centerId"> The ID from the center from wich you want to get all agents</param>
        /// <returns></returns>
        ///<response code = "200" > A list of agents is returned</response>
        /// <response code="404">The pms center from which you want to get the agents does not exist in the Database</response>
        [HttpGet("ByCenter/{centerId:length(24)}")]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAllAgentsByCenterAsync(string centerId)
        {
            var center = pmsCenterRepository.GetCenterByIdAsync(centerId);
            if (center ==  null)
            {
                return NotFound($"Could not find the center with id = {centerId}");
            }

            var agents = await agentRepository.GetAllAgentByCenterAsync(centerId);

            return Ok(agents);
        }

        /// <summary>
        /// Get an agent by its ID
        /// </summary>
        /// <param name="agentId">The ID from the agent to get</param>
        /// <returns></returns>
        /// <response code="200">The agent with the specified ID is returned</response>
        /// <response code="404">The agent with the specified ID does not exist in the Database</response>
        [HttpGet("{agentId:length(24)}", Name = "GetAgentByIdAsync")]
        public async Task<ActionResult<Agent>> GetAgentByIdAsync(string agentId)
        {
            var agent = await agentRepository.GetAgentByIdAsync(agentId);
            if (agent == null)
            {
                return NotFound($"Could not find agent with id = {agentId} in the Database");
            }

            return Ok(agent);   
        }

        /// <summary>
        /// Get an agent by its name
        /// </summary>
        /// <param name="name">The name of the agent to get</param>
        /// <param name="centerId">The PMS center ID from the agent to get</param>
        /// <returns></returns>
        /// <response code="200">The agent with the specified Name is returned</response>
        /// <response code="404">The agent with the specified Name does not exist in the Database</response>
        [HttpGet("ByName/{name}/{centerId:length(24)}", Name = "GetAgentByNameAsync")]
        public async Task<ActionResult<Agent>> GetAgentByNameAsync( string name, string centerId)
        {
            var agent = await agentRepository.GetAgentByNameAsync(name, centerId);
            if (agent == null)
            {
                return NotFound($"Could not find agent with name = {name} in the Database");
            }

            return Ok(agent);
        }    
    }
}
