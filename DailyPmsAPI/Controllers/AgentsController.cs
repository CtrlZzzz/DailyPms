using DailyPmsAPI.Repositories;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<IEnumerable<Agent>>> GetALlAgentsAsync()
        {
            var Agents = await agentRepository.GetAllAsync();

            return Ok(Agents);
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
            var center = await pmsCenterRepository.GetByIdAsync(centerId);
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
        /// <response code="200">The agent(s) with the specified Name is/are returned</response>
        /// <response code="404">The agent with the specified Name does not exist in the Database</response>
        [HttpGet("ByName/{name}", Name = "GetAgentByNameAsync")]
        public async Task<ActionResult<Agent>> GetAgentByNameAsync(string name)
        {
            var agents = await agentRepository.GetByNameAsync(name);
            if (agents == null)
            {
                return NotFound($"Could not find agent with name = {name} in the Database");
            }

            return Ok(agents);
        }

        /// <summary>
        /// Update an agent
        /// </summary>
        /// <param name="agentId" example="500000000000000000000001">The ID from the school to update</param>
        /// <param name="updatedAgent">The updated agent object (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="204">The agent with the specified ID is updated - No content is returned</response>
        /// <response code="404">The agent with the specified ID does not exist in the Database</response>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateAgentByIdAsync(string agentId, Agent updatedAgent)
        {
            var original = await agentRepository.GetByIdAsync(agentId);
            if (original == null)
            {
                return NotFound($"Could not find agent with id = {agentId}");
            }

            await agentRepository.UpdateAsync(agentId, updatedAgent);

            return NoContent();
        }

        /// <summary>
        /// Create a new agent
        /// </summary>
        /// <param name="newAgent">The new agent to create (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="201">The new agent is created</response>
        /// <response code="400">An agent with the same email as the new agent's email already exists in the Database</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateAgentAsync(Agent newAgent)
        {
            var alreadyExistingAgents = await agentRepository.GetAllAsync();
            foreach (var agent in alreadyExistingAgents)
            {
                if (agent.Email == newAgent.Email)
                {
                    return BadRequest(new ApiUserFlowResponse("ValidationError", $"An agent with email address {newAgent.Email} "
                        + $"already exist in the database !"));
                }
            }

            //Retreive center Id and store it in newAgent before creation
            var completeAgent = newAgent;
            var centers = await pmsCenterRepository.GetByNameAsync(newAgent.CenterName);
            completeAgent.CenterID = centers.ToList()[0]._id;

            await agentRepository.CreateAsync(completeAgent);
            //await agentRepository.CreateAsync(newAgent);

            return Ok(new ApiUserFlowResponse());
        }

        /// <summary>
        /// Remove an agent
        /// </summary>
        /// <param name="agentId" example="500000000000000000000001">The ID from the agent to remove from the Database</param>
        /// <returns></returns>
        /// <response code="204">The agent with the specified ID is removed from the Database - No content is returned</response>
        /// <response code="404">The agent with the specified ID does not exist in the Database</response>
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteAgentByIdAsync(string agentId)
        {
            var student = await agentRepository.GetByIdAsync(agentId);
            if (student == null)
            {
                return NotFound($"Could not found agent with id = {agentId}");
            }

            await agentRepository.DeleteAsync(agentId);

            return NoContent();
        }
    }
}
