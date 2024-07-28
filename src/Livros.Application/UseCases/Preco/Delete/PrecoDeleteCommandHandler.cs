using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Application.Notifications;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class PrecoDeleteCommandHandler : IRequestHandler<PrecoDeleteCommandRequest, PrecoDeleteCommandResponse>
    {
        private readonly IRepositoryPreco _precoRepository;
        public PrecoDeleteCommandHandler(IRepositoryPreco precoRepository)
        {
            _precoRepository = precoRepository;
        }

        public async Task<PrecoDeleteCommandResponse> Handle(PrecoDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new PrecoDeleteCommandResponse();
            var valid = RequestBase<PrecoDeleteCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            _ = await _precoRepository.DeleteAsync(request.CodL);

            return response;
        }
    }
}
