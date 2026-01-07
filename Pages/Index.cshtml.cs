using Calculadora_teste.Models;
using Calculadora_teste.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calculadora_teste.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICalcApiService _service;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ICalcApiService service, ILogger<IndexModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        [BindProperty]
        public decimal? ValorA { get; set; }

        [BindProperty]
        public decimal? ValorB { get; set; }

        [BindProperty]
        public string Operacao { get; set; }
        [BindProperty]
        public bool MostrarModal { get; set; } = false;



        public List<CalculationResponse> Calculos { get; set; } = new();
        public CalculationResponse UltimoCalculo { get; private set; }
        public string MensagemSucesso { get; set; }

        public async Task OnGetAsync()
        {
            Calculos = await _service.GetAllAsync();
        }

        public async Task<IActionResult> OnPostCalculateAsync()
        {
            _logger.LogInformation("POST Calculate chamado! A={ValorA}, B={ValorB}, Op={Operacao}", ValorA, ValorB, Operacao);

            if (!ValorA.HasValue || !ValorB.HasValue)
                ModelState.AddModelError("", "Os valores A e B são obrigatórios.");

            if (string.IsNullOrWhiteSpace(Operacao) || !new[] { "+", "-", "*", "/" }.Contains(Operacao))
                ModelState.AddModelError("", "Operação inválida.");

            if (Operacao == "/" && ValorB == 0)
                ModelState.AddModelError("", "Divisão por zero não é permitida.");

            if (!ModelState.IsValid)
            {
                Calculos = await _service.GetAllAsync();
                return Page();
            }

            decimal resultado = Operacao switch
            {
                "+" => ValorA.Value + ValorB.Value,
                "-" => ValorA.Value - ValorB.Value,
                "*" => ValorA.Value * ValorB.Value,
                "/" => ValorA.Value / ValorB.Value,
                _ => 0m
            };
            var brasil = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var agoraBrasil = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brasil);
            var request = new CalculationRequest

             
              {
                ValorA = ValorA.Value,
                ValorB = ValorB.Value,
                Operacao = Operacao,
                Resultado = resultado,
                DataCalculo = agoraBrasil
            };

            var id = await _service.CreateAsync(request);
         
            MensagemSucesso = $"Dados armazenados com sucesso! ID {id}";

            // Recarrega a lista de cálculos
            Calculos = await _service.GetAllAsync();

            // Define que o modal deve aparecer
            MostrarModal = true;

            return Page(); // <-- não redireciona, apenas renderiza a página atual

        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}
