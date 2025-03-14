using Microsoft.Maui.Controls;
using SolidariedadeMaui.Services; // Necessário para encontrar ApiService e Mensagem
using System;

namespace SolidariedadeMaui.Views
{
    public partial class MensagemPage : ContentPage
    {
        private ApiService _apiService = new ApiService();

        public MensagemPage()
        {
            InitializeComponent();
        }

        private async void EnviarMensagem_Clicked(object sender, EventArgs e)
        {
            // Cria um objeto Mensagem com base nos dados informados
            var mensagem = new Mensagem
            {
                Nome = NomeEntry.Text,
                Estado = EstadoEntry.Text,
                Conteudo = MensagemEditor.Text,
                DataEnvio = DateTime.Now
            };

            // Use o método correto, por exemplo, AddMensagemAsync
            var sucesso = await _apiService.AddMensagemAsync(mensagem);
            if (sucesso)
            {
                ResultadoLabel.Text = "Mensagem enviada com sucesso!";
                ResultadoLabel.IsVisible = true;
            }
            else
            {
                ResultadoLabel.Text = "Falha ao enviar mensagem.";
                ResultadoLabel.IsVisible = true;
            }
        }
    }
}
