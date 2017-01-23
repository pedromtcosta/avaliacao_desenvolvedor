using AvaliacaoWorkingHub.Models;

namespace AvaliacaoWorkingHub.Data.Contracts
{
    public interface IVendasData
    {
        void Insert(Venda venda);
        void Commit();
    }
}
