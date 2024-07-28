using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Livros.Infra.Data.Entities; 

namespace Livros.Application.UseCases
{
    public class AutorGetCommandHandler : IRequestHandler<AutorGetCommandRequest, AutorGetCommandResponse>
    {
        private readonly IRepository<Autor> _autorRepository;
        public AutorGetCommandHandler(IRepository<Autor> autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<AutorGetCommandResponse> Handle(AutorGetCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new AutorGetCommandResponse();

            var autorPersistence = await _autorRepository.GetAsync<AutorPersistence>();

            var autor = autorPersistence.Select<AutorPersistence, Autor>(x => x);
            response.Autor = autor;
            return response; 
        }
    }
}
