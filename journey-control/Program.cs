using journey_control.Helpers.AppData;
using journey_control.Services;
using journey_control.Views;

namespace journey_control
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var user = UserDataManager.LoadUserData();

            //TODO: - Implementar serviço de atualização
            //usando o seguinte comando: Application.ProductVersion.Split('+')[0] podemos pegar a versão guardada no binário do projeto

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
