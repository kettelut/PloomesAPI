using PloomesAPI.Domain.Entities;

namespace PloomesAPI.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        public List<Produto> ObterTodos(bool? ativo);

        public Produto ObterPelaPK(int id);

        public bool Existe(int id);

        public Produto Cadastrar(Produto produto);

        public void Deletar(int id);

        public Produto Atualizar(Produto produto);
    }
}
