using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livros.Domain.Interfaces.Repository
{
    public interface IRepositoryPreco
    {
        Task<int> CreateAsync(Domain.Entities.Preco preco);

        Task<int> UpdateAsync(Domain.Entities.Preco preco);

        Task<int> DeleteAsync(int codL);

        Task<IEnumerable<Domain.Entities.Preco>> GetByLivroIdAsync(int codL);
    }
}
