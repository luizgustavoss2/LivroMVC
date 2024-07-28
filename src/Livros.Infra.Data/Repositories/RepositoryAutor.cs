using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Livros.Domain.Interfaces.Repository;
using System.Collections.Generic;
using Livros.Domain.Entities;

namespace Livros.Infra.Data.Repositories
{
    public class RepositoryAutor : IRepositoryAutor
    {
        private readonly IConfiguration _configuration;

	    private SqlConnection OpenConnection() => new SqlConnection(_configuration.GetConnectionString("Sql"));

        private static readonly string INSERT = @"
	        Insert into [dbo].[Autor](
	            Nome 
            ) 
            values(
	            @Nome 
            );
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        private static readonly string UPDATE = @"
	        Update [dbo].[Autor] set
	           Nome = @Nome 
            Where CodAu = @CodAu;";

        private static readonly string DELETE = @"
	        Delete from [dbo].[Autor] 
            Where CodAu = @CodAu;";

        private static readonly string GETBYID = @"
	        Select 
                CodAu, 
                Nome 
            from [dbo].[Autor] 
            Where CodAu = @CodAu;";
               

        private static readonly string GETBYLIVROID = @"
	        Select 
                a.CodAu, 
                a.Nome 
            from [dbo].[Autor] a
            Inner Join [dbo].[Livro_Autor] la on a.CodAu = la.Autor_CodAu
            Where la.Livro_CodL = @CodL;";

        public RepositoryAutor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> CreateAsync(Domain.Entities.Autor autor)
        {
            using var conn = OpenConnection();

            autor.CodAu = await conn.QuerySingleAsync<int>(INSERT, new
            {
	            autor.Nome 
            });

            return autor.CodAu.Value;
        }

        public async Task<int> UpdateAsync(Domain.Entities.Autor autor)
        {
            using var conn = OpenConnection();

            await conn.ExecuteAsync(UPDATE, new
            {	           
	           autor.Nome,
               autor.CodAu
            });

            return  autor.CodAu.Value;
        }

        public async Task<int> DeleteAsync(int codAu)
        {
            using var conn = OpenConnection();
            await conn.ExecuteAsync(DELETE, new
            {
                codAu
            });

            return codAu;
        }

        public async Task<Domain.Entities.Autor> GetByIdAsync(int codAu)
        {
            using var conn = OpenConnection();
            return await conn.QueryFirstOrDefaultAsync<Domain.Entities.Autor>(GETBYID, new
            {
                codAu
            });
        }

        public async Task<IEnumerable<Autor>> GetByLivroIdAsync(int codL)
        {
            using var conn = OpenConnection();
            return await conn.QueryAsync<Domain.Entities.Autor>(GETBYLIVROID, new
            {
                codL
            });
        }
    }
}
