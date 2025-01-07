using journey_control.Repositories;
using journey_control.Services;

namespace journey_control.Helpers.Initializer
{
    public static class VersionSync
    {
        public static async Task Run(DateOnly date)
        {
            var redmineService = new RedmineService();

            Models.Version version = await redmineService.GetCurrentVersionAsync(date);
            VersionRepo versionRepo = new VersionRepo();

            Models.Version? existingVersion = await versionRepo.GetById(version.Id);

            if (existingVersion == null)
                versionRepo.Add(version);
            else
            {
                existingVersion.StartDate = version.StartDate;
                existingVersion.DueDate   = version.DueDate;

                await versionRepo.Update(existingVersion);
            }
        }
    }
}
