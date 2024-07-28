using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Infra.Data.Repositories
{
    public class RepositoryLivro_Autor : IRepositoryLivro_Autor
    {
        private readonly IConfiguration _configuration;

	    private SqlConnection OpenConnection() => new SqlConnection(_configuration.GetConnectionString("Sql"));

        private static readonly string INSERT = @"
	        Insert into [dbo].[Livro_Autor](
	            Livro_CodL,
	            Autor_CodAu 
            ) 
            values(
	            @Livro_CodL,
	            @Autor_CodAu 
            );";

        private static readonly string UPDATE = @"
	        Update [dbo].[Livro_Autor] set
	           Livro_CodL = @Livro_CodL,
	           Autor_CodAu = @Autor_CodAu 
            Where Id = @Id;";

        private static readonly string DELETE = @"
	        Delete from [dbo].[Livro_Autor] 
            Where Livro_CodL = @codL;";

        private static readonly string GETBYID = @"
	        Select 
                Livro_CodL, 
                Autor_CodAu 
            from [dbo].[Livro_Autor] 
            Where Livro_CodL = @CodL;";


        public RepositoryLivro_Autor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> CreateAsync(Domain.Entities.Livro_Autor livro_Autor)
        {
            using var conn = OpenConnection();

            await conn.ExecuteAsync(INSERT, new
            {
	            livro_Autor.Livro_CodL,
	            livro_Autor.Autor_CodAu 
            });

            return livro_Autor.Livro_CodL.Value;
        }

        public async Task<int> UpdateAsync(Domain.Entities.Livro_Autor livro_Autor)
        {
            using var conn = OpenConnection();

            livro_Autor.UpdatedOn = DateTime.Now; 
            await conn.ExecuteAsync(UPDATE, new
            {
	           livro_Autor.Livro_CodL,
	           livro_Autor.Autor_CodAu 
            });

            return  livro_Autor.Livro_CodL.Value;
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
