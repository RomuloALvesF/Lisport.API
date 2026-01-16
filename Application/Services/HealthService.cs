using Lisport.API.Domain.Interfaces;

namespace Lisport.API.Application.Services
{
    public class HealthService : IHealthService
    {
        public string GetStatus()
        {
            return "ok";
        }
    }
}
