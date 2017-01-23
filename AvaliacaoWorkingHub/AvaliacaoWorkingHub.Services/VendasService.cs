using AvaliacaoWorkingHub.Data.Contracts;
using AvaliacaoWorkingHub.Models;
using AvaliacaoWorkingHub.Services.Contracts;
using System.Collections.Generic;

namespace AvaliacaoWorkingHub.Services
{
    public class VendasService : IVendasService
    {
        private IVendasData _vendasData;

        public VendasService(IVendasData vendasData)
        {
            _vendasData = vendasData;
        }

        public void SalvarDadosVendas(IEnumerable<Venda> vendas)
        {
            foreach(var venda in vendas)
            {
                _vendasData.Insert(venda);
            }
            _vendasData.Commit();
        }
    }
}
