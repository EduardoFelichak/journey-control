using journey_control.Helpers.AppData;
using journey_control.Services;

namespace journey_control
{
    public partial class ApiKeyForm : Form
    {
        public ApiKeyForm()
        {
            InitializeComponent();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string apiKey = txtApiCode.Text.Trim();

                if (string.IsNullOrEmpty(apiKey))
                    throw new Exception("Por favor, insira o código da API.");

                var redmineService = new RedmineService();
                var user = await redmineService.GetUserAsync(apiKey);

                if (user == null)
                    throw new Exception("Código da API inválido. Tente novamente.");

                UserDataManager.SaveUserData(user);
                MessageBox.Show("Código da API validado e salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
