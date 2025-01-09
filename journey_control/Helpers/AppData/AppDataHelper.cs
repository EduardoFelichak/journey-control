namespace journey_control.Helpers.AppData
{
    public static class AppDataHelper
    {
        private static readonly string AppDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "JourneyControl"
        );

        public static string GetAppDataFolder()
        {
            if (!Directory.Exists(AppDataPath))
                Directory.CreateDirectory(AppDataPath);

            return AppDataPath;
        }

        public static string GetUserDataFilePath()
        {
            return Path.Combine(GetAppDataFolder(), "user.json");
        }
    }
}
