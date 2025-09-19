using FacilAssist.Front.Enum;
using FacilAssist.Front.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace FacilAssist.Front.Services
{
    public class ClienteService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly string _authEndpoint;
        private readonly string _clientesEndpoint;
        private readonly string _username;
        private readonly string _password;

        public string _authToken { get; set; }

        public ClienteService()
        {
            _apiBaseUrl = WebConfigurationManager.AppSettings["ApiBaseUrl"];
            _authEndpoint = WebConfigurationManager.AppSettings["AuthEndpoint"];
            _clientesEndpoint = WebConfigurationManager.AppSettings["ClientesEndpoint"];
            _username = WebConfigurationManager.AppSettings["ApiUsername"];
            _password = WebConfigurationManager.AppSettings["ApiPassword"];

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_apiBaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetAuthTokenAsync()
        {
            // Se já tivermos um token, o reutilizamos para evitar chamadas desnecessárias
            if (!string.IsNullOrEmpty(_authToken))
            {
                return _authToken;
            }

            try
            {
                var authRequestData = new AuthRequest
                {
                    Username = _username,
                    Password = _password
                };

                var jsonContent = JsonConvert.SerializeObject(authRequestData);

                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(_authEndpoint, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var tokenResult = JsonConvert.DeserializeObject<AuthToken>(jsonResponse);
                    _authToken = tokenResult.AccessToken;
                    return _authToken;
                }
                else
                {
                    Console.WriteLine($"Erro ao obter o token: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro de conexão ao obter o token: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Cliente>> ListarClientesAsync()
        {
            try
            {
                var token = await GetAuthTokenAsync();

                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _httpClient.GetAsync("/api/cliente");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var clientes = JsonConvert.DeserializeObject<List<Cliente>>(jsonResponse);
                    return clientes;
                }
                else
                {

                    Console.WriteLine($"Erro na requisição: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {

                Console.WriteLine($"Erro de conexão: {ex.Message}");
                return null;
            }
        }

        public async Task CriarClientesAsync(ClienteCommand command)
        {
            try
            {
                var token = await GetAuthTokenAsync();

                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    command.Criar();

                    var jsonContent = JsonConvert.SerializeObject(command);

                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await _httpClient.PostAsync("api/cliente", httpContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ClienteErroDto>(jsonResponse);

                        if (result.Nome.Count > 0)
                        {
                            throw new Exception(result.Nome[0]);
                        }
                        if (result.Cpf.Count > 0)
                        {
                            throw new Exception(result.Cpf[0]);
                        }
                        if (result.DataNascimento.Count > 0)
                        {
                            throw new Exception(result.DataNascimento[0]);
                        }
                        if (result.Sexo.Count > 0)
                        {
                            throw new Exception(result.Sexo[0]);
                        }
                        if (!string.IsNullOrEmpty(result.message))
                        {
                            throw new Exception(result.message);
                        }

                    }
                }


            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro na requisição: {ex.Message}");
            }
        }

        public async Task AlterarClientesAsync(ClienteCommand command)
        {
            try
            {
                var token = await GetAuthTokenAsync();

                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    command.Criar();

                    var jsonContent = JsonConvert.SerializeObject(command);

                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await _httpClient.PutAsync("api/cliente", httpContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ClienteErroDto>(jsonResponse);

                        if (result.Nome.Count > 0)
                        {
                            throw new Exception(result.Nome[0]);
                        }
                        if (result.Cpf.Count > 0)
                        {
                            throw new Exception(result.Cpf[0]);
                        }
                        if (result.DataNascimento.Count > 0)
                        {
                            throw new Exception(result.DataNascimento[0]);
                        }
                        if (result.Sexo.Count > 0)
                        {
                            throw new Exception(result.Sexo[0]);
                        }
                        if (!string.IsNullOrEmpty(result.message))
                        {
                            throw new Exception(result.message);
                        }

                    }
                }


            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro na requisição: {ex.Message}");
            }
        }

        public async Task<List<Cliente>> ObterClientesAsync()
        {
            var clientes = new List<Cliente>();
            try
            {
                var token = await GetAuthTokenAsync();

                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await _httpClient.GetAsync("api/cliente");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        clientes = JsonConvert.DeserializeObject<List<Cliente>>(jsonResponse);
                    }
                }

                return clientes;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro na requisição: {ex.Message}");
            }
        }

        public async Task<Cliente> DetalhesClientesAsync(int id)
        {
            var clientes = new Cliente();
            try
            {
                var token = await GetAuthTokenAsync();

                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var _url = string.Format("api/Cliente/{0}", id);

                    HttpResponseMessage response = await _httpClient.GetAsync(_url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        clientes = JsonConvert.DeserializeObject<Cliente>(jsonResponse);
                    }
                }

                return clientes;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro na requisição: {ex.Message}");
            }
        }

        public async Task AprovarReprovarClientesAsync(int id, EStatusCliente status)
        {
            try
            {
                var token = await GetAuthTokenAsync();

                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var _url = string.Format("api/Cliente/aprovar-reprovar/{0}/{1}", id, (int)status);
                    var httpContent = new StringContent("", Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await _httpClient.PutAsync(_url, httpContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ClienteErroDto>(jsonResponse);
                        throw new Exception(result.message);
                    }
                }


            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro na requisição: {ex.Message}");
            }
        }

        public async Task ExcluirClientesAsync(int id)
        {
            try
            {
                var token = await GetAuthTokenAsync();

                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var _url = string.Format("api/Cliente/{0}", id);                    

                    HttpResponseMessage response = await _httpClient.DeleteAsync(_url);

                    if (!response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ClienteErroDto>(jsonResponse);
                        throw new Exception(result.message);
                    }
                }


            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro na requisição: {ex.Message}");
            }
        }
    }
}