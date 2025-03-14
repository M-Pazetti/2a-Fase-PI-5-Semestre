using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using SolidariedadeMaui.Services; // Adicione esse using para acessar ApiService e Emergencia

namespace SolidariedadeMaui.Views
{
    public partial class EmergenciaPage : ContentPage
    {
        private ApiService _apiService = new ApiService();
        public ObservableCollection<Emergencia> Emergencias { get; set; }

        public EmergenciaPage()
        {
            InitializeComponent();
            Emergencias = new ObservableCollection<Emergencia>();
            BindingContext = this;
            LoadEmergencias();
        }

        private async void LoadEmergencias()
        {
            var lista = await _apiService.GetEmergenciasAsync();
            foreach (var item in lista)
            {
                Emergencias.Add(item);
            }
        }
    }
}
