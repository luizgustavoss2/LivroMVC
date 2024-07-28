using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Livros.Infra.Data.Entities; 

namespace Livros.Application.UseCases
{
    public class Livro_AutorGetCommandHandler : IRequestHandler<Livro_AutorGetCommandRequest, Livro_AutorGetCommandResponse>
    {
        private readonly IRepository<Livro_Autor> _livro_AutorRepository;
        public Livro_AutorGetCommandHandler(IRepository<Livro_Autor> livro_AutorRepository)
        {
            _livro_AutorRepository = livro_AutorRepository;
        }

        public async Task<Livro_AutorGetCommandResponse> Handle(Livro_AutorGetCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new Livro_AutorGetCommandResponse();

            var livro_AutorPersistence = await _livro_AutorRepository.GetAsync<Livro_AutorPersistence>();

            var livro_Autor = livro_AutorPersistence.Select<Livro_AutorPersistence, Livro_Autor>(x => x);
            response.Livro_Autor = livro_Autor;
            return response; 
        }
    }
}
