using System;
using System.Threading.Tasks;

namespace Livros.Domain.Interfaces.Repository
{
    public interface IRepositoryLivro_Assunto
    {
        Task<int> CreateAsync(Domain.Entities.Livro_Assunto livro_Assunto);

        Task<int> UpdateAsync(Domain.Entities.Livro_Assunto livro_Assunto);

        Task<int> DeleteAsync(int codL);

        Task<Domain.Entities.Livro_Assunto> GetByIdAsync(int codL);
    }
}
