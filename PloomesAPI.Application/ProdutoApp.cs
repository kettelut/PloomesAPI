using PloomesAPI.Application.Interfaces;
using PloomesAPI.Domain.Entities;
using PloomesAPI.Domain.Interfaces;

namespace PloomesAPI.Application
{
    public class ProdutoApp : IProdutoApp
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoApp(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public bool Existe(int id)
        {
            return _produtoRepository.Existe(id);
        }

        public Produto ObterPelaPK(int id)
        {
            return _produtoRepository.ObterPelaPK(id);
        }

        public List<Produto> ObterTodos(bool? ativo)
        {
            return _produtoRepository.ObterTodos(ativo);
        }

        public Produto Atualizar(Produto produto)
        {
            return _produtoRepository.Atualizar(produto);
        }

        public Produto Cadastrar(Produto produto)
        {
            return _produtoRepository.Cadastrar(produto);
        }

        public void Deletar(int id)
        {
            _produtoRepository.Deletar(id);
        }
    }
}
