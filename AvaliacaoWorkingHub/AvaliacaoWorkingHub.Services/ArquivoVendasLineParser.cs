using AvaliacaoWorkingHub.Models;
using AvaliacaoWorkingHub.Services.Contracts;
using System;
using System.Globalization;

namespace AvaliacaoWorkingHub.Services
{
    public class ArquivoVendasLineParser : IArquivoVendasLineParser
    {
        private NumberFormatInfo _numberFormatInfo;

        public ArquivoVendasLineParser()
        {
            _numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
        }

        public Venda ParseLine(string line)
        {
            var splitedInput = line.Split('\t');

            if (splitedInput.Length != 6)
            {
                throw new FormatException("Parâmetro input com quantidade incorreta de tabs. O input deve conter exatamente 6 tabs para que seja realizado o parse.");
            }

            var venda = new Venda();

            venda.Comprador = GetStringFromArrayIfPositionExists(splitedInput, 0);
            venda.Descricao = GetStringFromArrayIfPositionExists(splitedInput, 1);
            venda.PrecoUnitario = GetDecimal(GetStringFromArrayIfPositionExists(splitedInput, 2));
            venda.Quantidade = GetInt(GetStringFromArrayIfPositionExists(splitedInput, 3));
            venda.Endereco = GetStringFromArrayIfPositionExists(splitedInput, 4);
            venda.Fornecedor = GetStringFromArrayIfPositionExists(splitedInput, 5);

            return venda;
        }

        // Utils - Provavelmente deveria estar em um Assembly de utilitários em uma aplicação real
        #region Utils

        private decimal GetDecimal(string input)
        {
            try
            {
                var value = decimal.Parse(input, _numberFormatInfo);
                return value;
            }
            catch (ArgumentNullException)
            {
                return 0;
            }
            catch (FormatException formatException)
            {
                throw new NumericFieldParseException(formatException.Message, formatException);
            }
        }

        private int GetInt(string input)
        {
            try
            {
                var value = int.Parse(input, _numberFormatInfo);
                return value;
            }
            catch (ArgumentNullException)
            {
                return 0;
            }
            catch (FormatException formatException)
            {
                throw new NumericFieldParseException(formatException.Message, formatException);
            }
        }

        private string GetStringFromArrayIfPositionExists(string[] array, int position)
        {
            var value = array[position];
            if (value == String.Empty)
            {
                return null;
            }
            else
            {
                return value;
            }
        }

        #endregion
    }
}
