using System.Collections.Generic;
using MetricsManager.Models;

namespace MetricsManager.DAL.Interfaces
{
    public interface IAgentRepository
    {
        void RegisterAgent(AgentInfo item);
        
        void EnableById(int agentId);
        
        void DisableById(int agentId);

        IList<AgentInfo> GetRegisteredList();
    }
}