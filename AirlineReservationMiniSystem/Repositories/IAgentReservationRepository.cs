using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservationMiniSystem.Areas.Identity.Data;
using AirlineReservationMiniSystem.Models;

namespace AirlineReservationMiniSystem.Repositories
{
    public interface IAgentReservationRepository
    {
        Task<List<AgentReservation>> AllAgentReservations { get; }
        Task<AgentReservation> GetAgentReservationByUserId(string id);
        Task<int> Add(AgentReservation agentReservation);

        Task<int> Update(AgentReservation agentReservation);
        Task<int> Delete(AgentReservation agentReservation);

        Task<int> SaveChangesAsync();
    }
}