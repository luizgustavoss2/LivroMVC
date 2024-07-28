using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Application.Notifications;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class Livro_AutorDeleteCommandHandler : IRequestHandler<Livro_AutorDeleteCommandRequest, Livro_AutorDeleteCommandResponse>
    {
        private readonly IRepository<Livro_Autor> _livro_AutorRepository;
        private readonly IRepository<Livro_Autor> _genericRepository;
        public Livro_AutorDeleteCommandHandler(IRepository<Livro_Autor> livro_AutorRepository, IRepository<Livro_Autor> genericRepository)
        {
            _livro_AutorRepository = livro_AutorRepository;
            _genericRepository = genericRepository;
        }

        public async Task<Livro_AutorDeleteCommandResponse> Handle(Livro_AutorDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new Livro_AutorDeleteCommandResponse();
            var valid = RequestBase<Livro_AutorDeleteCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var livro_Autor = _genericRepository.GetAsync<Livro_Autor>(request.CodL);

            if(livro_Autor.Result is null)
            {
                response.AddNotification("Id", "Livro_Autor not found!", ErrorCode.NotFound);
                return response;
            }

            _ = await _livro_AutorRepository.DeleteAsync(request.CodL);
            return response;
        }
    }
}
