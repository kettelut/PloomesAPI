using Dapper;
using PloomesAPI.Domain.Entities;
using PloomesAPI.Domain.Interfaces;
using PloomesAPI.Infra.Data.Context;
using System.Data.SqlClient;

namespace PloomesAPI.Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        public readonly IDapperContext _dapperContext;

        public ProdutoRepository(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public bool Existe(int id)
        {
            var retorno = false;

            using (var con = _dapperContext.GetConnection())
            {
                try
                {
                    string sql = $@"
                        SELECT 
                            TOP 1 1
                        FROM 
                            Produto
                        WHERE 
                            ID = @Id
                    ";

                    retorno = con.ExecuteScalar<int>(sql, new { Id = id }) == 1;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 4060)
                        throw new Exception("Falha na comunicação com o banco de dados");
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return retorno;
        }

        public Produto ObterPelaPK(int id)
        {
            Produto produto = new Produto();

            using (var con = _dapperContext.GetConnection())
            {
                try
                {
                    string sql = @$"
                        SELECT
                            ID,
                            Nome,
                            Ativo,
                            DataAlteracao
                        FROM 
                            Produto
                        WHERE 
                            ID = @Id
                    ";

                    produto = con.QueryFirstOrDefault<Produto>(sql, new { Id = id });
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 4060)
                        throw new Exception("Falha na comunicação com o banco de dados");
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return produto;
        }

        public List<Produto> ObterTodos(bool? ativo)
        {
            List<Produto> listaProduto = new List<Produto>();

            using (var con = _dapperContext.GetConnection())
            {
                try
                {
                    string sql = @$"
                        SELECT
                            ID,
                            Nome,
                            Ativo,
                            DataAlteracao
                        FROM
                            Produto
                        WHERE 
                            0 = 0
                    ";

                    if (ativo != null)
                        sql += $@" AND Ativo = @Ativo";

                    listaProduto = con.Query<Produto>(sql, ativo).ToList();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 4060)
                        throw new Exception("Falha na comunicação com o banco de dados");
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return listaProduto;
        }

        public Produto Cadastrar(Produto produto)
        {
            Produto produtoResult = new Produto();
            using (var con = _dapperContext.GetConnection())
            {
                try
                {
                    string sql = @$"
                        INSERT INTO Produto
                        (
                            Nome,
                            Ativo,
                            DataAlteracao
                        )
                        OUTPUT
                            INSERTED.ID,
                            INSERTED.Nome,
                            INSERTED.Ativo,
                            INSERTED.DataAlteracao
                        VALUES
                        (
                            @Nome,
                            @Ativo,
                            GETDATE()
                        )
                    ";

                    produtoResult = con.QueryFirstOrDefault<Produto>(sql, produto);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 4060)
                        throw new Exception("Falha na comunicação com o banco de dados");
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return produtoResult;
        }

        public void Deletar(int id)
        {
            using (var con = _dapperContext.GetConnection())
            {
                try
                {
                    string sql = @$"
                        DELETE FROM 
                            Produto
                        OUTPUT
                            DELETED.ID,
                            DELETED.Nome,
                            DELETED.Ativo,
                            DELETED.DataAlteracao
                        WHERE 
                            ID = @ID
                    ";

                    con.Execute(sql, new { ID = id });
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 4060)
                        throw new Exception("Falha na comunicação com o banco de dados");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public Produto Atualizar(Produto produto)
        {
            Produto produtoResult = new Produto();

            if (!this.Existe(produto.Id))
                return produtoResult;

            using (var con = _dapperContext.GetConnection())
            {
                try
                {
                    string sql = @$"
                        UPDATE 
                            Produto
                        SET
                            Nome = @Nome,
                            Ativo = @Ativo,
                            DataAlteracao = GETDATE()
                        OUTPUT
                            INSERTED.ID,
                            INSERTED.Nome,
                            INSERTED.Ativo,
                            INSERTED.DataAlteracao
                        WHERE 
                            ID = @ID
                    ";

                    produtoResult = con.QueryFirstOrDefault<Produto>(sql, produto);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 4060)
                        throw new Exception("Falha na comunicação com o banco de dados");
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return produtoResult;
        }
    }
}
