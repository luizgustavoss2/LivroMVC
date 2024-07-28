using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Livros.Infra.Data.Entities; 

namespace Livros.Application.UseCases
{
    public class PrecoGetCommandHandler : IRequestHandler<PrecoGetCommandRequest, PrecoGetCommandResponse>
    {
        private readonly IRepository<Preco> _precoRepository;
        public PrecoGetCommandHandler(IRepository<Preco> precoRepository)
        {
            _precoRepository = precoRepository;
        }

        public async Task<PrecoGetCommandResponse> Handle(PrecoGetCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new PrecoGetCommandResponse();

            var precoPersistence = await _precoRepository.GetAsync<PrecoPersistence>();

            var preco = precoPersistence.Select<PrecoPersistence, Preco>(x => x);
            response.Preco = preco;
            return response; 
        }
    }
}
