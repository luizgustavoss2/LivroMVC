using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livros.Domain.Interfaces.Repository
{
    public interface IRepositoryLivro
    {
        Task<int> CreateAsync(Domain.Entities.Livro livro);

        Task<int> UpdateAsync(Domain.Entities.Livro livro);

        Task<int> DeleteAsync(int codL);

        Task<Domain.Entities.Livro> GetByIdAsync(int codL);

        Task<IEnumerable<Domain.Entities.Livro>> GetAllAsync();
    }
}
