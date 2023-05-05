using System.Data;

namespace PloomesAPI.Infra.Data.Context
{
    public interface IDapperContext
    {
        IDbConnection GetConnection();
    }
}
