using AvaliacaoWorkingHub.Services.Contracts;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Linq;

namespace AvaliacaoWorkingHub.Services.Tests
{
    [TestClass]
    public class FileParserTests
    {
        [TestMethod]
        public void Retorna_Quantidade_Registros_Correta()
        {
            var mock = new Mock<ILineParser<object>>();
            mock
                .Setup(m => m.ParseLine(It.IsAny<string>()))
                .Returns(new object());

            var fileParser = new FileParser<object>(mock.Object);

            var fakeLines = Enumerable.Range(1, 11).Select(x => x.ToString());
            var fakeFile = String.Join(Environment.NewLine, fakeLines);

            using (var inputStream = GenerateStreamFromString(fakeFile))
            {
                var parsedData = fileParser.ParseFile(inputStream);
                parsedData.Should().HaveCount(10);
            }
        }

        [TestMethod]
        public void Retorna_Quantidade_Registros_Correta_Com_Linha_Vazia_No_Final()
        {
            var mock = new Mock<ILineParser<object>>();
            mock
                .Setup(m => m.ParseLine(It.IsAny<string>()))
                .Returns(new object());

            var fileParser = new FileParser<object>(mock.Object);

            var fakeLines = Enumerable.Range(1, 11).Select(x => x.ToString()).ToList();
            fakeLines.Add(String.Empty);
            var fakeFile = String.Join(Environment.NewLine, fakeLines);

            using (var inputStream = GenerateStreamFromString(fakeFile))
            {
                var parsedData = fileParser.ParseFile(inputStream);
                parsedData.Should().HaveCount(10);
            }
        }

        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
