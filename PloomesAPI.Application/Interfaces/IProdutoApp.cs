using PloomesAPI.Domain.Entities;

namespace PloomesAPI.Application.Interfaces
{
    public interface IProdutoApp
    {
        public List<Produto> ObterTodos(bool? ativo);

        public bool Existe(int id);

        public Produto ObterPelaPK(int id);

        public Produto Atualizar(Produto produto);

        public Produto Cadastrar(Produto produto);

        public void Deletar(int id);
    }
}
