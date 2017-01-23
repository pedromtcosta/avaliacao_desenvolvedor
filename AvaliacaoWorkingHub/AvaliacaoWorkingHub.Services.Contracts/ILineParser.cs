namespace AvaliacaoWorkingHub.Services.Contracts
{
    public interface ILineParser<T>
    {
        T ParseLine(string line);
    }
}
