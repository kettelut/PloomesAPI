namespace PloomesAPI.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataAlteracao {get; set; }
    }
}
