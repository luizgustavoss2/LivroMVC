using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Livros.Domain.Interfaces.Repository;
using Livros.Domain.Entities;
using System.Collections.Generic;

namespace Livros.Infra.Data.Repositories
{
    public class RepositoryPreco : IRepositoryPreco
    {
        private readonly IConfiguration _configuration;

	    private SqlConnection OpenConnection() => new SqlConnection(_configuration.GetConnectionString("Sql"));

        private static readonly string INSERT = @"
	        Insert into [dbo].[Preco](
	            Livro_CodL,
	            Valor,
	            Tipo 
            ) 
            values(
	            @Livro_CodL,
	            @Valor,
	            @Tipo 
            );
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        private static readonly string UPDATE = @"
	        Update [dbo].[Preco] set	           
	           Livro_CodL = @Livro_CodL,
	           Valor = @Valor,
	           Tipo = @Tipo 
            Where CodPr = @CodPr;";

        private static readonly string DELETE = @"
	        Delete from [dbo].[Preco] 
            Where Livro_CodL = @codL;";

        private static readonly string GETBYLIVROID = @"
	        Select 
                CodPr,
                Livro_CodL,
	            Valor,
	            Tipo 
            from [dbo].[Preco] 
            Where Livro_CodL = @CodL;";

        public RepositoryPreco(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> CreateAsync(Domain.Entities.Preco preco)
        {
            using var conn = OpenConnection();

            preco.CodPr = await conn.QuerySingleAsync<int>(INSERT, new
            {
	            preco.CodPr,
	            preco.Livro_CodL,
	            preco.Valor,
	            preco.Tipo 
            });

            return preco.CodPr.Value;
        }

        public async Task<int> UpdateAsync(Domain.Entities.Preco preco)
        {
            using var conn = OpenConnection();

            await conn.ExecuteAsync(UPDATE, new
            {
	           preco.CodPr,
	           preco.Livro_CodL,
	           preco.Valor,
	           preco.Tipo 
            });

            return  preco.CodPr.Value;
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

        public async Task<IEnumerable<Preco>> GetByLivroIdAsync(int codL)
        {
            using var conn = OpenConnection();
            return await conn.QueryAsync<Domain.Entities.Preco>(GETBYLIVROID, new
            {
                codL
            });
        }
    }
}
