using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class AssuntoInsertCommandHandler : IRequestHandler<AssuntoInsertCommandRequest, AssuntoInsertCommandResponse>
    {
        private readonly IRepositoryAssunto _assuntoRepository;
        public AssuntoInsertCommandHandler(IRepositoryAssunto assuntoRepository)
        {
            _assuntoRepository = assuntoRepository;
        }

        public async Task<AssuntoInsertCommandResponse> Handle(AssuntoInsertCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new AssuntoInsertCommandResponse();
            var valid = RequestBase<AssuntoInsertCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var objAssunto = PrepareObject(request);

            response.CodAs = await _assuntoRepository.CreateAsync(objAssunto);
            return response;
        }

        private Assunto PrepareObject(AssuntoInsertCommandRequest request) => new Assunto(null, request.Descricao);
    }
}
