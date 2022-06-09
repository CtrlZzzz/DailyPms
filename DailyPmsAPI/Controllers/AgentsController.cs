using DailyPmsAPI.Repositories;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        readonly AgentRepository agentRepository;
        readonly PmsCenterRepository pmsCenterRepository;


        public AgentsController(IRepository<Agent> agentRepo, IRepository<PmsCenter> centerRepo)
        {
            agentRepository = (AgentRepository)agentRepo;
            pmsCenterRepository = (PmsCenterRepository)centerRepo;
        }


        /// <summary>
        /// Get a list of all pms agents
        /// </summary>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code="200">A list of pms agents is returned</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetALlSchoolsAsync()
        {
            var schools = await agentRepository.GetAllAsync();

            return Ok(schools);
        }

        /// <summary>
        /// Get a list of all agents from one pms center
        /// </summary>
        /// <param name="centerId" example="400000000000000000000001"> The ID from the center from wich you want to get all agents</param>
        /// <returns></returns>
        ///<response code = "200" > A list of agents is returned</response>
        /// <response code="404">The pms center from which you want to get the agents does not exist in the Database</response>
        [HttpGet("ByCenter/{centerId:length(24)}")]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAllAgentsByCenterAsync(string centerId)
        {
            var center = pmsCenterRepository.GetByIdAsync(centerId);
            if (center == null)
            {
                return NotFound($"Could not find the center with id = {centerId}");
            }

            var agents = await agentRepository.GetAllFromIdAsync(centerId);

            return Ok(agents);
        }

        /// <summary>
        /// Get an agent by its ID
        /// </summary>
        /// <param name="agentId" example="500000000000000000000001">The ID from the agent to get</param>
        /// <returns></returns>
        /// <response code="200">The agent with the specified ID is returned</response>
        /// <response code="404">The agent with the specified ID does not exist in the Database</response>
        [HttpGet("{agentId:length(24)}", Name = "GetAgentByIdAsync")]
        public async Task<ActionResult<Agent>> GetAgentByIdAsync(string agentId)
        {
            var agent = await agentRepository.GetByIdAsync(agentId);
            if (agent == null)
            {
                return NotFound($"Could not find agent with id = {agentId} in the Database");
            }

            return Ok(agent);
        }

        /// <summary>
        /// Get agent(s) by its/their name
        /// </summary>
        /// <param name="name" example="De Groote">The name of the agent(s) to get</param>
        /// <returns></returns>
        /// <response code="200">The agent(s) with the specified Name is returned</response>
        /// <response code="404">The agent with the specified Name does not exist in the Database</response>
        [HttpGet("ByName/{name}/{centerId:length(24)}", Name = "GetAgentByNameAsync")]
        public async Task<ActionResult<Agent>> GetAgentByNameAsync(string name)
        {
            var agent = await agentRepository.GetByNameAsync(name);
            if (agent == null)
            {
                return NotFound($"Could not find agent with name = {name} in the Database");
            }

            return Ok(agent);
        }
    }
}
