using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Livros.Domain.Interfaces.Repository;
using System.Linq;
using System.Collections.Generic;

namespace Livros.Infra.Data.Repositories
{
    public class RepositoryLivro : IRepositoryLivro
    {
        private readonly IConfiguration _configuration;

	    private SqlConnection OpenConnection() => new SqlConnection(_configuration.GetConnectionString("Sql"));

        private static readonly string INSERT = @"
	        Insert into [dbo].[Livro](
	            Titulo,
	            Editora,
	            Edicao,
	            AnoPublicacao 
            ) 
            values(
	            @Titulo,
	            @Editora,
	            @Edicao,
	            @AnoPublicacao 
            );
             SELECT CAST(SCOPE_IDENTITY() AS INT);";

        private static readonly string UPDATE = @"
	        Update [dbo].[Livro] set
	           Titulo = @Titulo,
	           Editora = @Editora,
	           Edicao = @Edicao,
	           AnoPublicacao = @AnoPublicacao 
            Where CodL = @CodL;";

        private static readonly string GETBYID = @"
	        Select 
               l.CodL,
               l.Titulo,
               l.Editora,
               l.Edicao,
               l.AnoPublicacao,
               a.CodAs,
               a.Descricao
            From [dbo].[Livro] l
	        Left join Livro_Assunto la on l.CodL = la.Livro_CodL
            Left join Assunto a on la.Assunto_CodAs = a.CodAs
            Where l.CodL = @CodL;";

        private static readonly string DELETE = @"
	        Delete from [dbo].[Livro] 
            Where CodL = @CodL;";

        private static readonly string GETALL = @"
	        Select 
               l.CodL,
               l.Titulo,
               l.Editora,
               l.Edicao,
               l.AnoPublicacao,
               a.CodAs,
               a.Descricao
            From [dbo].[Livro] l
	        Left join Livro_Assunto la on l.CodL = la.Livro_CodL
            Left join Assunto a on la.Assunto_CodAs = a.CodAs
            Order by l.Titulo";

        public RepositoryLivro(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> CreateAsync(Domain.Entities.Livro livro)
        {
            using var conn = OpenConnection();

            livro.CodL = await conn.QuerySingleAsync<int>(INSERT, new
            {
	            livro.Titulo,
	            livro.Editora,
	            livro.Edicao,
	            livro.AnoPublicacao 
            });

            return livro.CodL.Value;
        }

        public async Task<int> UpdateAsync(Domain.Entities.Livro livro)
        {
            using var conn = OpenConnection();
 
            await conn.ExecuteAsync(UPDATE, new
            {
	           livro.Titulo,
	           livro.Editora,
	           livro.Edicao,
	           livro.AnoPublicacao,
               livro.CodL
            });

            return  livro.CodL.Value;
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

        public async Task<Domain.Entities.Livro> GetByIdAsync(int codL)
        {
            using var conn = OpenConnection();

            var result = await conn.QueryAsync<Domain.Entities.Livro, Domain.Entities.Assunto, Domain.Entities.Livro>(GETBYID,
            map: (livro, assunto) =>
            {
                livro.Assunto = assunto;

                return livro;
            },
            param: new
            {
                CodL = codL
            },
               splitOn: "CodAs");

            return result?.FirstOrDefault();
        }

        public async Task<IEnumerable<Domain.Entities.Livro>> GetAllAsync()
        {
            using var conn = OpenConnection();

            var result = await conn.QueryAsync<Domain.Entities.Livro, Domain.Entities.Assunto, Domain.Entities.Livro>(GETALL,
            map: (livro, assunto) =>
            {
                livro.Assunto = assunto;

                return livro;
            },
               splitOn: "CodAs");


            return result?.ToList();
        }

    }
}
