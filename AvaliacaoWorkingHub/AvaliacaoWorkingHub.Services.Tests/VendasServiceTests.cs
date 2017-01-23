using AvaliacaoWorkingHub.Data.Contracts;
using AvaliacaoWorkingHub.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace AvaliacaoWorkingHub.Services.Tests
{
    [TestClass]
    public class VendasServiceTests
    {
        private Mock<IVendasData> _vendasDataMock;
        private VendasService _svc;

        [TestInitialize]
        public void Setup()
        {
            _vendasDataMock = new Mock<IVendasData>();
            _svc = new VendasService(_vendasDataMock.Object);
        }

        [TestMethod]
        public void Chama_Insert_10_Vezes_Ao_Inserir_10_Vendas()
        {
            _vendasDataMock.Setup(m => m.Insert(It.IsAny<Venda>()));

            var vendas = Enumerable.Range(1, 10).Select(i => new Venda());

            _svc.SalvarDadosVendas(vendas);
            _vendasDataMock.Verify(m => m.Insert(It.IsAny<Venda>()), Times.Exactly(10));
        }

        [TestMethod]
        public void Chama_Commit_Ao_Inserir_Vendas()
        {
            _vendasDataMock.Setup(m => m.Insert(It.IsAny<Venda>()));
            _vendasDataMock.Setup(m => m.Commit());

            var vendas = Enumerable.Range(1, 10).Select(i => new Venda());

            _svc.SalvarDadosVendas(vendas);
            _vendasDataMock.Verify(m => m.Commit(), Times.Once);
        }
    }
}
