using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SolidariedadeMaui.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        // Construtor que permite injetar o HttpClient (bom para testes e reutilização)
        public ApiService(HttpClient httpClient = null)
        {
            // Se não for injetado, cria uma nova instância e define o BaseAddress.
            _httpClient = httpClient ?? new HttpClient { BaseAddress = new Uri("http://localhost:3000") };
            // Se estiver testando em um emulador Android, troque para "http://10.0.2.2:3000"

            // Configura opções de serialização (por exemplo, para ignorar diferenças em case-sensitive)
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        #region Métodos para Postos de Doação

        // Método para obter a lista de postos de doação
        public async Task<List<PostoDoacao>> GetPostosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/postos");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var postos = JsonSerializer.Deserialize<List<PostoDoacao>>(json, _jsonOptions);
                return postos ?? new List<PostoDoacao>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao obter postos: {ex.Message}");
                return new List<PostoDoacao>();
            }
        }

        // Método para adicionar um novo posto de doação
        public async Task<bool> AddPostoAsync(PostoDoacao posto)
        {
            try
            {
                var json = JsonSerializer.Serialize(posto, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/postos", content);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao adicionar posto: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Métodos para Emergências

        // Método para obter a lista de números de emergência
        public async Task<List<Emergencia>> GetEmergenciasAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/emergencias");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var emergencias = JsonSerializer.Deserialize<List<Emergencia>>(json, _jsonOptions);
                return emergencias ?? new List<Emergencia>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao obter emergências: {ex.Message}");
                return new List<Emergencia>();
            }
        }

        // Método para inserir uma nova emergência
        public async Task<bool> AddEmergenciaAsync(Emergencia emergencia)
        {
            try
            {
                var json = JsonSerializer.Serialize(emergencia, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/emergencias", content);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao adicionar emergência: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Métodos para Mensagens de Solidariedade

        // Método para obter a lista de mensagens
        public async Task<List<Mensagem>> GetMensagensAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/mensagens");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var mensagens = JsonSerializer.Deserialize<List<Mensagem>>(json, _jsonOptions);
                return mensagens ?? new List<Mensagem>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao obter mensagens: {ex.Message}");
                return new List<Mensagem>();
            }
        }

        // Método para inserir uma nova mensagem
        public async Task<bool> AddMensagemAsync(Mensagem mensagem)
        {
            try
            {
                var json = JsonSerializer.Serialize(mensagem, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/mensagens", content);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao adicionar mensagem: {ex.Message}");
                return false;
            }
        }

        #endregion
    }

    #region Modelos (idealmente, esses modelos devem estar em arquivos separados na pasta Models)

    public class PostoDoacao
    {
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public Localizacao Localizacao { get; set; } = new Localizacao();
    }

    public class Localizacao
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Emergencia
    {
        public string Nome { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
    }

    public class Mensagem
    {
        public string Nome { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Conteudo { get; set; } = string.Empty;
        public DateTime DataEnvio { get; set; } = DateTime.Now;
    }

    #endregion
}
