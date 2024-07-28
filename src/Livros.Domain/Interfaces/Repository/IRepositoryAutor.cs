using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livros.Domain.Interfaces.Repository
{
    public interface IRepositoryAutor
    {
        Task<int> CreateAsync(Domain.Entities.Autor autor);

        Task<int> UpdateAsync(Domain.Entities.Autor autor);

        Task<int> DeleteAsync(int codAu);

        Task<Domain.Entities.Autor> GetByIdAsync(int codAu);

        Task<IEnumerable<Domain.Entities.Autor>> GetByLivroIdAsync(int codL);
    }
}
