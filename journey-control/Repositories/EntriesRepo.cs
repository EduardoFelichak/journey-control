using journey_control.Helpers.AppData;
using journey_control.Infra.Context;
using journey_control.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;

namespace journey_control.Repositories
{
    public class EntriesRepo
    {
        private readonly ApplicationDBContext _context = new ApplicationDBContext();

        public async System.Threading.Tasks.Task AddOrUpdateFromRedmine(List<Entrie> redmineEntries)
        {
            foreach (var entry in redmineEntries)
            {
                var existingEntry = await _context.Entries.FirstOrDefaultAsync(e => e.Id == entry.Id);

                if (existingEntry == null)
                    await _context.Entries.AddAsync(entry);
                else
                {
                    existingEntry.Duration = entry.Duration;
                    existingEntry.DateEntrie = entry.DateEntrie;
                    existingEntry.TaskId = entry.TaskId;
                    _context.Entries.Update(existingEntry);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Entrie>> GetEntriesByUserAndDate(DateOnly date)
        {
            var user = UserDataManager.LoadUserData();

            return await _context.Entries
                .Include(e => e.Task)
                .Where(e => e.TaskUserId == user.Id && e.DateEntrie == date)
                .ToListAsync();
        }

        public async Task<int> GetTotalTimeByTaskAndDate(string taskId, DateOnly date)
        {
            var user = UserDataManager.LoadUserData();

            return await _context.Entries
                .Include(e => e.Task)
                .Where(e => e.TaskUserId == user.Id
                        && e.TaskId == taskId
                        && e.DateEntrie == date)
                .SumAsync(e => e.Duration);
        }

        public async System.Threading.Tasks.Task Add(Entrie entry)
        {
            var existingEntry = await _context.Entries.FindAsync(entry);

            if (existingEntry != null)
            {
                existingEntry.Duration = entry.Duration;
                _context.Entries.Update(existingEntry);
            }
            else
                _context.Entries.Add(entry);

            await _context.SaveChangesAsync();
        }

        public async Task<int> GetRealesedTime(DateOnly date)
        {
            var user = UserDataManager.LoadUserData();

            return await _context.Entries
                .Where(e => e.DateEntrie == date && e.TaskUserId == user.Id)
                .SumAsync(e => e.Duration);
        }

        public async Task<int> GetTotalSpentTimePerDate(DateOnly date)
        {
            var user = UserDataManager.LoadUserData();

            return await _context.Entries
                .Where(e => e.DateEntrie == date && e.TaskUserId == user.Id)
                .SumAsync(e => e.Duration) +
                await _context.LocalEntries
                .Where(e => e.DateEntrie == date && e.TaskUserId == user.Id)
                .SumAsync(e => e.Duration);
        }

        public async Task<int> GetWorkTimeSpentPerDate(DateOnly date)
        {
            var user = UserDataManager.LoadUserData();

            return await _context.Entries
                .Where(e => e.DateEntrie == date && e.TaskUserId == user.Id
                        && e.TaskId != "Estudo")
                .SumAsync(e => e.Duration) +
                await _context.LocalEntries
                .Where(e => e.DateEntrie == date && e.TaskUserId == user.Id
                        && e.TaskId != "Estudo")
                .SumAsync(e => e.Duration);
        }

        public async Task<int> GetStudyTimeSpentPerDate(DateOnly date)
        {
            var user = UserDataManager.LoadUserData();

            return await _context.Entries
                .Where(e => e.DateEntrie == date && e.TaskUserId == user.Id
                        && e.TaskId == "Estudo")
                .SumAsync(e => e.Duration) +
                await _context.LocalEntries
                .Where(e => e.DateEntrie == date && e.TaskUserId == user.Id
                        && e.TaskId == "Estudo")
                .SumAsync(e => e.Duration);
        }
    }
}
