using AvaliacaoWorkingHub.Models;
using AvaliacaoWorkingHub.Services;
using AvaliacaoWorkingHub.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvaliacaoWorkingHub.Web.Controllers
{
    public static class Messages
    {
        public static string UploadWithoutFileMessage = "Nenhum arquivo selecionado para o upload.";
        public static string NumericFieldsErrorMessage = "Arquivo inválido. Verifique os campos numéricos.";
        public static string InvalidFileErrorMessage = "Arquivo inválido. Verifique a quantidade de colunas.";
        public static string GenericError = "Erro ao realizar a operação. Favor tentar novamente.";
    }

    public class VendasController : Controller
    {
        private IFileParser<Venda> _fileParser;
        private IVendasService _vendasService;

        public VendasController(IVendasService vendasService, IFileParser<Venda> fileParser)
        {
            _vendasService = vendasService;
            _fileParser = fileParser;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload(VendasUploadModel uploadModel)
        {
            if (uploadModel.File?.InputStream == null)
            {
                uploadModel.ErrorMessage = Messages.UploadWithoutFileMessage;
            }
            else
            {
                try
                {
                    var vendasData = _fileParser.ParseFile(uploadModel.File.InputStream).ToList();
                    _vendasService.SalvarDadosVendas(vendasData);
                    uploadModel.UploadedData = vendasData;
                    uploadModel.ReceitaBruta = vendasData.Sum(v => v.PrecoUnitario * v.Quantidade);
                }
                catch (NumericFieldParseException)
                {
                    uploadModel.ErrorMessage = Messages.NumericFieldsErrorMessage;
                }
                catch (FormatException)
                {
                    uploadModel.ErrorMessage = Messages.InvalidFileErrorMessage;
                }
                catch
                {
                    uploadModel.ErrorMessage = Messages.GenericError;
                }
            }

            return View("Index", uploadModel);
        }
    }

    public class VendasUploadModel
    {
        public HttpPostedFileBase File { get; set; }
        public IEnumerable<Venda> UploadedData { get; set; }
        public decimal ReceitaBruta { get; set; }
        public string ErrorMessage { get; set; }
    }
}