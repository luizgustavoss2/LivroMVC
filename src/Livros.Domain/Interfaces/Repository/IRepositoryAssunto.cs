using System;
using System.Threading.Tasks;

namespace Livros.Domain.Interfaces.Repository
{
    public interface IRepositoryAssunto
    {
        Task<int> CreateAsync(Domain.Entities.Assunto assunto);

        Task<int> UpdateAsync(Domain.Entities.Assunto assunto);

        Task<int> DeleteAsync(int codAs);


        Task<Domain.Entities.Assunto> GetByIdAsync(int codAs);
    }
}
