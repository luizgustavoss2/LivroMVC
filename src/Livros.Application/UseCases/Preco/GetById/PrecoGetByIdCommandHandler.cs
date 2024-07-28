using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Livros.Infra.Data.Entities;

namespace Livros.Application.UseCases
{
    public class PrecoGetByIdCommandHandler : IRequestHandler<PrecoGetByIdCommandRequest, PrecoGetByIdCommandResponse>
    {
        private readonly IRepositoryPreco _precoRepository;
        public PrecoGetByIdCommandHandler(IRepositoryPreco precoRepository)
        {
            _precoRepository = precoRepository;
        }

        public async Task<PrecoGetByIdCommandResponse> Handle(PrecoGetByIdCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new PrecoGetByIdCommandResponse();
            var valid = RequestBase<PrecoGetByIdCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var preco = await _precoRepository.GetByLivroIdAsync(request.CodL);

            response.Preco = preco;

            return response; 
        }
    }
}
