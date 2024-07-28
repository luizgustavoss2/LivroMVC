using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Livros.Infra.Data.Entities; 

namespace Livros.Application.UseCases
{
    public class AssuntoGetCommandHandler : IRequestHandler<AssuntoGetCommandRequest, AssuntoGetCommandResponse>
    {
        private readonly IRepository<Assunto> _assuntoRepository;
        public AssuntoGetCommandHandler(IRepository<Assunto> assuntoRepository)
        {
            _assuntoRepository = assuntoRepository;
        }

        public async Task<AssuntoGetCommandResponse> Handle(AssuntoGetCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new AssuntoGetCommandResponse();

            var assuntoPersistence = await _assuntoRepository.GetAsync<AssuntoPersistence>();

            var assunto = assuntoPersistence.Select<AssuntoPersistence, Assunto>(x => x);
            response.Assunto = assunto;
            return response; 
        }
    }
}
