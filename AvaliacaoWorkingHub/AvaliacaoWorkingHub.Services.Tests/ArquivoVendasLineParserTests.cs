using AvaliacaoWorkingHub.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaliacaoWorkingHub.Services.Tests
{
    [TestClass]
    public class ArquivoVendasLineParserTests
    {
        private ArquivoVendasLineParser _parser = new ArquivoVendasLineParser();

        [TestMethod]
        public void Cria_Retorno_Com_Dados_Das_Vendas()
        {
            var input = "João Silva	R$10 off R$20 of food	10.0	2	987 Fake St	Bob's Pizza";
            var venda = _parser.ParseLine(input);

            venda.ShouldBeEquivalentTo(new Venda
            {
                Comprador = "João Silva",
                Descricao = "R$10 off R$20 of food",
                PrecoUnitario = 10,
                Quantidade = 2,
                Endereco = "987 Fake St",
                Fornecedor = "Bob's Pizza"
            });
        }

        [TestMethod]
        public void Cria_Retorno_Com_Dados_Das_Vendas_Ultimo_Campo_Faltando()
        {
            var input = "João Silva	R$10 off R$20 of food	10.0	2	987 Fake St	";
            var venda = _parser.ParseLine(input);

            venda.ShouldBeEquivalentTo(new Venda
            {
                Comprador = "João Silva",
                Descricao = "R$10 off R$20 of food",
                PrecoUnitario = 10,
                Quantidade = 2,
                Endereco = "987 Fake St",
                Fornecedor = null
            });
        }

        [TestMethod]
        public void Cria_Retorno_Com_Dados_Das_Vendas_Segundo_Campo_Faltando()
        {
            var input = "João Silva		10.0	2	987 Fake St	Bob's Pizza";
            var venda = _parser.ParseLine(input);

            venda.ShouldBeEquivalentTo(new Venda
            {
                Comprador = "João Silva",
                Descricao = null,
                PrecoUnitario = 10,
                Quantidade = 2,
                Endereco = "987 Fake St",
                Fornecedor = "Bob's Pizza"
            });
        }

        [TestMethod]
        public void Cria_Retorno_Com_PrecoUnitario_0_Caso_Nao_Esteja_Presente_No_Arquivo()
        {
            var input = "João Silva	R$10 off R$20 of food		2	987 Fake St	Bob's Pizza";
            var venda = _parser.ParseLine(input);

            venda.ShouldBeEquivalentTo(new Venda
            {
                Comprador = "João Silva",
                Descricao = "R$10 off R$20 of food",
                PrecoUnitario = 0,
                Quantidade = 2,
                Endereco = "987 Fake St",
                Fornecedor = "Bob's Pizza"
            });
        }

        [TestMethod]
        public void Cria_Retorno_Com_Quantidade_0_Caso_Nao_Esteja_Presente_No_Arquivo()
        {
            var input = "João Silva	R$10 off R$20 of food	10.0		987 Fake St	Bob's Pizza";
            var venda = _parser.ParseLine(input);

            venda.ShouldBeEquivalentTo(new Venda
            {
                Comprador = "João Silva",
                Descricao = "R$10 off R$20 of food",
                PrecoUnitario = 10.0m,
                Quantidade = 0,
                Endereco = "987 Fake St",
                Fornecedor = "Bob's Pizza"
            });
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Lanca_FormatException_Caso_Arquivo_Nao_Possua_6_Tabs()
        {
            var input = "João Silva	R$10 off R$20 of food	10.0	2	987 Fake St";
            var venda = _parser.ParseLine(input);
        }

        [TestMethod]
        [ExpectedException(typeof(NumericFieldParseException))]
        public void Lanca_NumericFieldParseException_Caso_PrecoUnitario_Esteja_Com_Formato_Incorreto()
        {
            var input = "João Silva	R$10 off R$20 of food	a	2	987 Fake St	Bob's Pizza";
            var venda = _parser.ParseLine(input);
        }

        [TestMethod]
        [ExpectedException(typeof(NumericFieldParseException))]
        public void Lanca_NumericFieldParseException_Caso_Quantidade_Esteja_Com_Formato_Incorreto()
        {
            var input = "João Silva	R$10 off R$20 of food	10.0	b	987 Fake St	Bob's Pizza";
            var venda = _parser.ParseLine(input);
        }
    }
}
