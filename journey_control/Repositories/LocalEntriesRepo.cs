using journey_control.Helpers.AppData;
using journey_control.Infra.Context;
using journey_control.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace journey_control.Repositories
{
    public class LocalEntriesRepo
    {
        private readonly ApplicationDBContext _context = new ApplicationDBContext();

        public async System.Threading.Tasks.Task Add(LocalEntrie localEntrie)
        {
            var existingEntry = await _context.LocalEntries
                    .Where(e => e.DateEntrie == DateOnly.FromDateTime(DateTime.Now)
                            && e.TaskId == localEntrie.TaskId
                            && e.TaskUserId == localEntrie.TaskUserId)
                    .FirstOrDefaultAsync();

            if (existingEntry != null)
            {
                existingEntry.Duration = localEntrie.Duration;
                _context.LocalEntries.Update(existingEntry);
            }
            else
                _context.LocalEntries.Add(localEntrie);

            await _context.SaveChangesAsync();
        }

        public async Task<List<LocalEntrie>> GetAllPerDate(DateOnly date)
        {
            var user = UserDataManager.LoadUserData();

            return await _context.LocalEntries
                .Include(e => e.Task)
                .Where(e => e.DateEntrie == date && e.TaskUserId == user.Id)
                .ToListAsync(); 
        }


        public async Task<int> GetTotalTimeByTaskAndDate(string taskId, DateOnly date)
        {
            var user = UserDataManager.LoadUserData();

            return await _context.LocalEntries
                .Include(e => e.Task)
                .Where(e => e.TaskUserId == user.Id
                        && e.TaskId == taskId
                        && e.DateEntrie == date)
                .SumAsync(e => e.Duration);
        }

        public async Task<int> GetRealesedTime(DateOnly date)
        {
            var user = UserDataManager.LoadUserData();

            return await _context.LocalEntries
                .Where(e => e.DateEntrie == date && e.TaskUserId == user.Id)
                .SumAsync(e => e.Duration);
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var localEntrie = await _context.LocalEntries.FirstOrDefaultAsync(e => e.Id == id);    

            if (localEntrie != null)
            {
                _context.LocalEntries.Remove(localEntrie);  
                await _context.SaveChangesAsync();
            }
        }
    }
}
