using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservationMiniSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservationMiniSystem.Repositories.Impl
{
    public class AgentReservationRepository: IAgentReservationRepository
    {
        private readonly AppDbContext _appDbContext;

        public AgentReservationRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<List<AgentReservation>> AllAgentReservations => _appDbContext.AgentReservations.ToListAsync();
        
        public Task<AgentReservation> GetAgentReservationByUserId(string id)
        {
            return _appDbContext.AgentReservations.FirstOrDefaultAsync(ar => ar.UserId == id);
        }

        public Task<int> Add(AgentReservation agentReservation)
        {
            _appDbContext.AgentReservations.Add(agentReservation);
            return _appDbContext.SaveChangesAsync();
        }

        public Task<int> Update(AgentReservation agentReservation)
        {
            _appDbContext.AgentReservations.Update(agentReservation);
            return _appDbContext.SaveChangesAsync();
        }

        public Task<int> Delete(AgentReservation agentReservation)
        {
            _appDbContext.Remove(agentReservation);
            return _appDbContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _appDbContext.SaveChangesAsync();
        }
    }
}