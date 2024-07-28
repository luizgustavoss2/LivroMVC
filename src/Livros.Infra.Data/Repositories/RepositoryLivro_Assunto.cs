using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Infra.Data.Repositories
{
    public class RepositoryLivro_Assunto : IRepositoryLivro_Assunto
    {
        private readonly IConfiguration _configuration;

	    private SqlConnection OpenConnection() => new SqlConnection(_configuration.GetConnectionString("Sql"));

        private static readonly string INSERT = @"
	        Insert into [dbo].[Livro_Assunto](
	            Livro_CodL,
	            Assunto_CodAs 
            ) 
            values(
	            @Livro_CodL,
	            @Assunto_CodAs 
            );";

        private static readonly string UPDATE = @"
	        Update [dbo].[Livro_Assunto] set
	           Assunto_CodAs = @Assunto_CodAs 
            Where Livro_CodL = @Livro_CodL;";

        private static readonly string DELETE = @"
	        Delete from [dbo].[Livro_Assunto] 
            Where Livro_CodL = @CodL;";

        private static readonly string GETBYID = @"
	        Select 
                Livro_CodL, 
                Assunto_CodAs 
            from [dbo].[Livro_Assunto] 
            Where Livro_CodL = @CodL;";

        public RepositoryLivro_Assunto(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> CreateAsync(Domain.Entities.Livro_Assunto livro_Assunto)
        {
            using var conn = OpenConnection();

            await conn.ExecuteAsync(INSERT, new
            {
	            livro_Assunto.Livro_CodL,
	            livro_Assunto.Assunto_CodAs 
            });

            return livro_Assunto.Livro_CodL.Value;
        }

        public async Task<int> UpdateAsync(Domain.Entities.Livro_Assunto livro_Assunto)
        {
            using var conn = OpenConnection();

            await conn.ExecuteAsync(UPDATE, new
            {
	           livro_Assunto.Livro_CodL,
	           livro_Assunto.Assunto_CodAs 
            });

            return  livro_Assunto.Livro_CodL.Value;
        }

        public async Task<int> DeleteAsync(int codL)
        {
            using var conn = OpenConnection();

            await conn.ExecuteAsync(DELETE, new
            {
                codL
            });

            return codL;
        }

        
        public async Task<Domain.Entities.Livro_Assunto> GetByIdAsync(int codL)
        {
            using var conn = OpenConnection();
            return await conn.QueryFirstOrDefaultAsync<Domain.Entities.Livro_Assunto>(GETBYID, new
            {
                codL
            });
        }
    }
}
