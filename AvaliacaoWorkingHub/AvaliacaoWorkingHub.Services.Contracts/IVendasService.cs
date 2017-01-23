using System.Collections.Generic;
using AvaliacaoWorkingHub.Models;

namespace AvaliacaoWorkingHub.Services.Contracts
{
    public interface IVendasService
    {
        void SalvarDadosVendas(IEnumerable<Venda> vendas);
    }
}