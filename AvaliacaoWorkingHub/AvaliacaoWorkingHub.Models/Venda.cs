namespace AvaliacaoWorkingHub.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public string Comprador { get; set; }
        public string Descricao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
        public string Endereco { get; set; }
        public string Fornecedor { get; set; }
    }
}
