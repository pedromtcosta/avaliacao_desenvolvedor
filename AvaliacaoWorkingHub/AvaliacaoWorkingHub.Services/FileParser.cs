using AvaliacaoWorkingHub.Services.Contracts;
using System.Collections.Generic;
using System.IO;

namespace AvaliacaoWorkingHub.Services
{
    public class FileParser<T> : IFileParser<T>
    {
        private ILineParser<T> _lineParser;

        public FileParser(ILineParser<T> lineParser)
        {
            _lineParser = lineParser;
        }

        public IEnumerable<T> ParseFile(Stream input, bool ignoreFirstLine = true)
        {
            using (var reader = new StreamReader(input))
            {
                if (ignoreFirstLine) reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    yield return _lineParser.ParseLine(line);
                }
            }
        }
    }
}
