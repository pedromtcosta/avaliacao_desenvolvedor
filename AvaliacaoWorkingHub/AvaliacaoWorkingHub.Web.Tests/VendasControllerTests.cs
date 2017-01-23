using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AvaliacaoWorkingHub.Web.Controllers;
using Moq;
using System.Web;
using FluentAssertions;
using System.Web.Mvc;
using AvaliacaoWorkingHub.Services.Contracts;
using AvaliacaoWorkingHub.Models;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using AvaliacaoWorkingHub.Services;

namespace AvaliacaoWorkingHub.Web.Tests
{
    [TestClass]
    public class VendasControllerTests
    {
        private VendasController _controller;
        private Mock<HttpPostedFileBase> _httpFileMock;
        private Mock<IVendasService> _vendasServiceMock;
        private Mock<IFileParser<Venda>> _fileParserMock;
        private Mock<Stream> _streamMock;

        [TestInitialize]
        public void Setup()
        {
            _streamMock = new Mock<Stream>();
            _httpFileMock = new Mock<HttpPostedFileBase>();
            _httpFileMock.Setup(m => m.InputStream).Returns(_streamMock.Object);
            _vendasServiceMock = new Mock<IVendasService>();
            _fileParserMock = new Mock<IFileParser<Venda>>();
            _controller = new VendasController(_vendasServiceMock.Object, _fileParserMock.Object);
        }

        [TestMethod]
        public void Retorna_Erro_Quando_Nenhum_Arquivo_Selecionado()
        {
            var uploadModel = GetValidUploadModel();
            uploadModel.File = null;
            var result = (ViewResult)_controller.Upload(uploadModel);

            ((VendasUploadModel)result.Model).ErrorMessage.Should().Be(Messages.UploadWithoutFileMessage);
        }

        [TestMethod]
        public void Adiciona_Mensagem_Erro_Quando_Ocorrer_FormatException()
        {
            _fileParserMock
                .Setup(m => m.ParseFile(It.IsAny<Stream>(), true))
                .Throws(new FormatException());

            var uploadModel = GetValidUploadModel();
            var result = (ViewResult)_controller.Upload(uploadModel);

            ((VendasUploadModel)result.Model).ErrorMessage.Should().Be(Messages.InvalidFileErrorMessage);
        }

        [TestMethod]
        public void Adiciona_Mensagem_Erro_Quando_Ocorrer_NumericFieldParseException()
        {
            _fileParserMock
                .Setup(m => m.ParseFile(It.IsAny<Stream>(), true))
                .Throws(new NumericFieldParseException());

            var uploadModel = GetValidUploadModel();
            var result = (ViewResult)_controller.Upload(uploadModel);

            ((VendasUploadModel)result.Model).ErrorMessage.Should().Be(Messages.NumericFieldsErrorMessage);
        }

        [TestMethod]
        public void Salva_Dados_de_Vendas_Com_Quantidade_de_Registros_Retornada_Pelo_Parser()
        {
            _fileParserMock
                .Setup(m => m.ParseFile(It.IsAny<Stream>(), true))
                .Returns(Enumerable.Range(1, 10).Select(x => new Venda()));

            _vendasServiceMock
                .Setup(m => m.SalvarDadosVendas(It.IsAny<IEnumerable<Venda>>()));

            var uploadModel = GetValidUploadModel();
            var result = (ViewResult)_controller.Upload(uploadModel);

            _vendasServiceMock
                .Verify(m => m.SalvarDadosVendas(It.Is<IEnumerable<Venda>>(en => en.Count() == 10)));
        }

        [TestMethod]
        public void Adiciona_Registros_Importados_Na_ViewModel()
        {
            _fileParserMock
                .Setup(m => m.ParseFile(It.IsAny<Stream>(), true))
                .Returns(Enumerable.Range(1, 10).Select(x => new Venda()));

            var uploadModel = GetValidUploadModel();
            var result = (ViewResult)_controller.Upload(uploadModel);

            ((VendasUploadModel)result.Model).UploadedData.Should().HaveCount(10);
        }

        [TestMethod]
        public void Adiciona_Receita_Bruta_Dos_Registros_Importados_Na_ViewModel()
        {
            _fileParserMock
                .Setup(m => m.ParseFile(It.IsAny<Stream>(), true))
                .Returns(new List<Venda>
                {
                    new Venda { PrecoUnitario = 10, Quantidade = 2 },
                    new Venda { PrecoUnitario = 10, Quantidade = 3 }
                });

            _vendasServiceMock
                .Setup(m => m.SalvarDadosVendas(It.IsAny<IEnumerable<Venda>>()));

            var uploadModel = GetValidUploadModel();
            var result = (ViewResult)_controller.Upload(uploadModel);

            ((VendasUploadModel)result.Model).ReceitaBruta.Should().Be(50);
        }

        private VendasUploadModel GetValidUploadModel()
        {
            var uploadModel = new VendasUploadModel();

            uploadModel.File = _httpFileMock.Object;

            return uploadModel;
        }
    }
}
