using journey_control.Models;
using Newtonsoft.Json;

namespace journey_control.Helpers.AppData
{
    public static class UserDataManager
    {
        public static void SaveUserData(User user)
        {
            string filePath = AppDataHelper.GetUserDataFilePath();
            string json     = JsonConvert.SerializeObject(user, Formatting.Indented);   
            File.WriteAllText(filePath, json);
        }

        public static User LoadUserData()
        {
            string filePath = AppDataHelper.GetUserDataFilePath();
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<User>(jsonContent);
            }
            return null;
        }
    }
}
