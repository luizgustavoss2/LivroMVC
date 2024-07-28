using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class PrecoInsertCommandHandler : IRequestHandler<PrecoInsertCommandRequest, PrecoInsertCommandResponse>
    {
        private readonly IRepositoryPreco _precoRepository;
        public PrecoInsertCommandHandler(IRepositoryPreco precoRepository)
        {
            _precoRepository = precoRepository;
        }

        public async Task<PrecoInsertCommandResponse> Handle(PrecoInsertCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new PrecoInsertCommandResponse();
            var valid = RequestBase<PrecoInsertCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var objPreco = PrepareObject(request);

            response.CodPr = await _precoRepository.CreateAsync(objPreco);

            return response;
        }

        private Preco PrepareObject(PrecoInsertCommandRequest request) => new Preco(0, request.Livro_CodL, request.Valor, request.Tipo);
    }
}
