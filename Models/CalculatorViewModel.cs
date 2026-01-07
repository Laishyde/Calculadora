using System;
using System.Collections.Generic;

namespace Calculadora_teste.Models
{
    public class CalculatorViewModel
    {
        public decimal? ValorA { get; set; }
        public decimal? ValorB { get; set; }
        public string Operacao { get; set; }
        public List<CalculationResponse> Calculos { get; set; } = new();
    }

    public class CalculationResponse
    {
        public int Id { get; set; }
        public decimal ValorA { get; set; }
        public decimal ValorB { get; set; }
        public string Operacao { get; set; }
        public decimal Resultado { get; set; }
        public DateTime DataCalculo { get; set; }
    }

    public class CalculationRequest
    {
        public decimal ValorA { get; set; }
        public decimal ValorB { get; set; }
        public string Operacao { get; set; }
        public decimal Resultado { get; set; }
        public DateTime DataCalculo { get; set; }
    }
}
