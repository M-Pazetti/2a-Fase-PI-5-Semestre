namespace SolidariedadeMaui.Views
{
    public partial class MapaPage : ContentPage
    {
        public MapaPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Exemplo simples utilizando Google Maps via WebView
            string htmlString = @"
            <!DOCTYPE html>
            <html>
            <head>
              <meta name='viewport' content='initial-scale=1.0, user-scalable=no' />
              <style> 
                #map { height: 100%; width:100%; } 
                html, body { height: 100%; margin: 0; padding: 0;} 
              </style>
            </head>
            <body>
              <div id='map'></div>
              <script src='https://maps.googleapis.com/maps/api/js?key=SUA_CHAVE_AQUI'></script>
              <script>
                function initMap() {
                  var centerLatLng = { lat: -30.0, lng: -51.0 };
                  var map = new google.maps.Map(document.getElementById('map'), {
                    zoom: 10,
                    center: centerLatLng
                  });
                  // Exemplo de marcador fixo
                  new google.maps.Marker({
                    position: { lat: -30.034647, lng: -51.217658 },
                    map: map,
                    title: 'Posto Central'
                  });
                }
                window.onload = initMap;
              </script>
            </body>
            </html>
            ";

            MapaWebView.Source = new HtmlWebViewSource { Html = htmlString };
        }
    }
}
