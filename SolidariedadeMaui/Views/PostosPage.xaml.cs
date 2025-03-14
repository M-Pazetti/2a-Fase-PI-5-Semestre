using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using SolidariedadeMaui.Services; // Certifique-se de que este é o namespace onde ApiService e PostoDoacao estão definidos

namespace Solidariedade_Maui.Views
{
    public partial class PostosPage : ContentPage
    {
        private ApiService _apiService = new ApiService();

        // ObservableCollection para vinculação (binding) na CollectionView
        public ObservableCollection<PostoDoacao> Postos { get; set; } = new ObservableCollection<PostoDoacao>();

        public PostosPage()
        {
            InitializeComponent();
            BindingContext = this;
            LoadPostos();
        }

        private async void LoadPostos()
        {
            var lista = await _apiService.GetPostosAsync();
            foreach (var item in lista)
            {
                Postos.Add(item);
            }
        }
    }
}
