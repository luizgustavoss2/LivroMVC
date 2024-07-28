using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Livros.Infra.Data.Entities; 

namespace Livros.Application.UseCases
{
    public class Livro_AssuntoGetCommandHandler : IRequestHandler<Livro_AssuntoGetCommandRequest, Livro_AssuntoGetCommandResponse>
    {
        private readonly IRepository<Livro_Assunto> _livro_AssuntoRepository;
        public Livro_AssuntoGetCommandHandler(IRepository<Livro_Assunto> livro_AssuntoRepository)
        {
            _livro_AssuntoRepository = livro_AssuntoRepository;
        }

        public async Task<Livro_AssuntoGetCommandResponse> Handle(Livro_AssuntoGetCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new Livro_AssuntoGetCommandResponse();

            var livro_AssuntoPersistence = await _livro_AssuntoRepository.GetAsync<Livro_AssuntoPersistence>();

            var livro_Assunto = livro_AssuntoPersistence.Select<Livro_AssuntoPersistence, Livro_Assunto>(x => x);
            response.Livro_Assunto = livro_Assunto;
            return response; 
        }
    }
}
