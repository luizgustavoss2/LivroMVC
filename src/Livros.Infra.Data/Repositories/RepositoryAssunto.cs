using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Infra.Data.Repositories
{
    public class RepositoryAssunto : IRepositoryAssunto
    {
        private readonly IConfiguration _configuration;

	    private SqlConnection OpenConnection() => new SqlConnection(_configuration.GetConnectionString("Sql"));

        private static readonly string INSERT = @"
	        Insert into [dbo].[Assunto](
	            Descricao 
            ) 
            values(
	            @Descricao 
            );
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        private static readonly string UPDATE = @"
	        Update [dbo].[Assunto] set
	           Descricao = @Descricao 
            Where CodAs = @CodAs;";


        private static readonly string DELETE = @"
	        Delete from [dbo].[Assunto] 
            Where CodAs = @CodAs;";

        private static readonly string GETBYID = @"
	        Select 
                CodAs, 
                Descricao 
            from [dbo].[Assunto] 
            Where CodAs = @CodAs;";

        public RepositoryAssunto(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> CreateAsync(Domain.Entities.Assunto assunto)
        {
            using var conn = OpenConnection();

            assunto.CodAs = await conn.QuerySingleAsync<int>(INSERT, new
            {
	            assunto.Descricao 
            });

            return assunto.CodAs.Value;
        }

        public async Task<int> UpdateAsync(Domain.Entities.Assunto assunto)
        {
            using var conn = OpenConnection();
            await conn.ExecuteAsync(UPDATE, new
            {
	           assunto.Descricao,
                assunto.CodAs
            });

            return  assunto.CodAs.Value;
        }

        public async Task<int> DeleteAsync(int codAs)
        {
            using var conn = OpenConnection();
            await conn.ExecuteAsync(DELETE, new
            {
                codAs
            });

            return codAs;
        }

        public async Task<Domain.Entities.Assunto> GetByIdAsync(int codAs)
        {
            using var conn = OpenConnection();
            return await conn.QueryFirstOrDefaultAsync<Domain.Entities.Assunto>(GETBYID, new
            {
                codAs
            });
        }

    }
}
