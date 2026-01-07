using Calculadora_teste.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calculadora_teste.Services.Interfaces
{
    public interface ICalcApiService
    {
        Task<List<CalculationResponse>> GetAllAsync();
        Task<int> CreateAsync(CalculationRequest request);
        Task DeleteAsync(int id);
    }
}
