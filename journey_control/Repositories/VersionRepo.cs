using journey_control.Helpers.AppData;
using journey_control.Infra.Context;
using journey_control.Models;
using Microsoft.EntityFrameworkCore;

namespace journey_control.Repositories
{
    public class VersionRepo
    {
        private readonly ApplicationDBContext _context = new ApplicationDBContext();

        public async void Add(Models.Version version)
        {
            _context.Versions.Add(version);
            await _context.SaveChangesAsync();
        }

        public async Task<Boolean> Exists(int versionId)
        {
            var user = UserDataManager.LoadUserData();

            return await _context.Versions
                            .Where(v => v.Id == versionId && v.ProjectId == user.ProjectId)
                            .AnyAsync();
        }

        public async Task<Models.Version> GetVersionPerDate(DateOnly date)
        {
            var user = UserDataManager.LoadUserData();

            return await _context.Versions
                            .Where(v => v.StartDate <= date && v.DueDate >= date && v.ProjectId == user.ProjectId)
                            .FirstAsync();
        }

        public async Task<Models.Version?> GetById(int id)
        {
            var user = UserDataManager.LoadUserData();

            return await _context.Versions
                            .Where(v => v.Id == id && v.ProjectId == user.ProjectId)
                            .FirstOrDefaultAsync();
        }

        public async System.Threading.Tasks.Task Update(Models.Version version)
        {
            _context.Versions.Update(version);
            await _context.SaveChangesAsync();
        }
    }
}
