using journey_control.Helpers.AppData;
using journey_control.Infra.Context;
using journey_control.Models;
using Microsoft.EntityFrameworkCore;

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
                else if (!existingEntry.IsInProgress)
                {
                    existingEntry.Duration = entry.Duration;
                    existingEntry.DateEntrie = entry.DateEntrie;
                    existingEntry.TaskId = entry.TaskId;
                    _context.Entries.Update(existingEntry);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Entrie>> GetInProgressEntries()
        {
            var user = UserDataManager.LoadUserData();

            return await _context.Entries.Where(e => e.IsInProgress && e.TaskUserId == user.Id).ToListAsync();
        }

        public async Task<List<Entrie>> GetEntriesByUserAndDate(DateOnly date)
        {
            var user = UserDataManager.LoadUserData();

            return await _context.Entries
                .Include(e => e.Task)
                .Where(e => e.TaskUserId == user.Id && e.DateEntrie == date)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task Add(Entrie entry)
        {
            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();
        }
    }
}
