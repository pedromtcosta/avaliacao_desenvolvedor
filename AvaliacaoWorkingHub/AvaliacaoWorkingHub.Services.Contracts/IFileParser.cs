using System.Collections.Generic;
using System.IO;

namespace AvaliacaoWorkingHub.Services.Contracts
{
    public interface IFileParser<T>
    {
        IEnumerable<T> ParseFile(Stream input, bool ignoreFirstLine = true);
    }
}