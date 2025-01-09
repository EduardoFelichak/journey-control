using journey_control.Infra.Context;
using journey_control.Models;
using Microsoft.EntityFrameworkCore;

namespace journey_control.Repositories
{
    public class ProjectRepo
    {
        private readonly ApplicationDBContext _context = new ApplicationDBContext();

        public async Task<List<Project>> GetAll()
        {
            return await _context.Projects.ToListAsync();
        }
    }
}
