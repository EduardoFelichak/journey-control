using journey_control.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace journey_control.Helpers.Update
{
    public static class UpdateHelper
    {
        private static readonly string InstallerUrl = "https://github.com/EduardoFelichak/journey-control/releases/latest/download/setup.exe";
        private static readonly string LocalInstallerPath = Path.Combine(Path.GetTempPath(), "journey_control_installer.exe");
        private static readonly ApplicationDBContext _context = new ApplicationDBContext();

        public static async Task CheckForUpdatesAsync()
        {
            var appVersion = await _context.AppVersions.FirstOrDefaultAsync();
            string currentVersion = Application.ProductVersion.Split('+')[0];

            if (currentVersion != appVersion.Name)
            {
                var result = MessageBox.Show(
                        $"Uma nova versão do aplicativo está disponível ({appVersion.Name}). Deseja atualizar agora?",
                        "Atualização Disponível",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information
                );

                if (result == DialogResult.Yes)
                    await DownloadAndInstallUpdateAsync();
            }
        }

        private static async Task DownloadAndInstallUpdateAsync()
        {
            try
            {
                using (var client = new HttpClient()) 
                { 
                    var response = await client.GetAsync(InstallerUrl);
                    response.EnsureSuccessStatusCode();

                    using (var fileStream = new FileStream(LocalInstallerPath, FileMode.Create, FileAccess.Write))
                    {
                        await response.Content.CopyToAsync(fileStream);
                    }
                }

                Process.Start(new ProcessStartInfo
                {
                    FileName = LocalInstallerPath,
                    UseShellExecute = true,
                });

                Application.Exit();
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Erro ao baixar e instalar a atualização: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
