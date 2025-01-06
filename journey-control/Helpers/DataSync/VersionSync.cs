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
            if (!await versionRepo.Exists(version.Id))
                versionRepo.Add(version);
        }
    }
}
