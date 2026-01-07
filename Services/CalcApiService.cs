using Calculadora_teste.Models;
using Calculadora_teste.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Calculadora_teste.Services
{
    public class CalcApiService : ICalcApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CalcApiService> _logger;

        public CalcApiService(HttpClient httpClient, ILogger<CalcApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<CalculationResponse>> GetAllAsync()
        {
            var url = "api/Calculadora?apikey=LAIS-0107";
            _logger.LogInformation("GET → {Url}", url);

            var response = await _httpClient.GetAsync(url);
            _logger.LogInformation("GET STATUS → {Status}", response.StatusCode);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError("GET ERROR → {Error}", error);
                return new List<CalculationResponse>();
            }

            return await response.Content.ReadFromJsonAsync<List<CalculationResponse>>()
                   ?? new List<CalculationResponse>();
        }

        public async Task<int> CreateAsync(CalculationRequest request)
        {
            var body = new
            {
                id = 0,
                valorA = request.ValorA,
                valorB = request.ValorB,
                operacao = request.Operacao,
                resultado = request.Resultado,
                dataCalculo = request.DataCalculo.ToString("o")
            };

            _logger.LogInformation("POST → api/Calculadora?apikey=LAIS-0107");
            _logger.LogInformation("POST BODY → {Body}", System.Text.Json.JsonSerializer.Serialize(body));

            var response = await _httpClient.PostAsJsonAsync("api/Calculadora?apikey=LAIS-0107", body);
            _logger.LogInformation("POST STATUS → {Status}", response.StatusCode);

            var id = await response.Content.ReadFromJsonAsync<int>();
            _logger.LogInformation("POST RESPONSE → {Id}", id);

            return id;
        }

        public async Task DeleteAsync(int id)
        {
            var url = $"api/Calculadora?id={id}&apikey=LAIS-0107";
            _logger.LogInformation("DELETE → {Url}", url);

            var response = await _httpClient.DeleteAsync(url);
            _logger.LogInformation("DELETE STATUS → {Status}", response.StatusCode);

            response.EnsureSuccessStatusCode();
        }
    }
}
