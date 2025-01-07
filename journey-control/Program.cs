using journey_control.Helpers.AppData;
using journey_control.Services;
using journey_control.Views;

namespace journey_control
{
    internal static class Program
    {
        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var user = UserDataManager.LoadUserData();

            if (user != null)
            {
                var redmineService = new RedmineService();
                user = await redmineService.GetUserAsync(user.ApiKey);
            }

            //TODO: - Implementar servi�o de atualiza��o
            //usando o seguinte comando: Application.ProductVersion.Split('+')[0] podemos pegar a vers�o guardada no bin�rio do projeto

            if (user != null)
            {
                Application.Run(new MainForm());
            }
            else
            {
                using (var apiKeyForm = new ApiKeyForm())
                {
                    if (apiKeyForm.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(new MainForm());
                    }
                }
            }
        }
    }
}
