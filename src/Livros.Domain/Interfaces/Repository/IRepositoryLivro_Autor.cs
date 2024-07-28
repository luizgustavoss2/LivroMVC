using System;
using System.Threading.Tasks;

namespace Livros.Domain.Interfaces.Repository
{
    public interface IRepositoryLivro_Autor
    {
        Task<int> CreateAsync(Domain.Entities.Livro_Autor livro_Autor);

        Task<int> UpdateAsync(Domain.Entities.Livro_Autor livro_Autor);

        Task<int> DeleteAsync(int codL);

        Task<Domain.Entities.Livro_Assunto> GetByIdAsync(int codL);
    }
}
