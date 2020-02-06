using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using workout_app.Core.Domain;

namespace workout_app.Core.Interfaces
{
    public interface ISessionRepository
    {
        Task<List<Session>> GetAllAsync();
        Task<Session> GetByIdAsync(int id);
        Task<int> CreateAsync(Session session);
        Task<int> UpdateAsync(Session session);
        Task DeleteAsync(Session session);
    }
}
