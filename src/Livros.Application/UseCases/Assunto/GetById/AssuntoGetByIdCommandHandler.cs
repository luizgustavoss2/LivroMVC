using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Livros.Infra.Data.Entities;

namespace Livros.Application.UseCases
{
    public class AssuntoGetByIdCommandHandler : IRequestHandler<AssuntoGetByIdCommandRequest, AssuntoGetByIdCommandResponse>
    {
        private readonly IRepositoryAssunto _assuntoRepository;
        public AssuntoGetByIdCommandHandler(IRepositoryAssunto assuntoRepository)
        {
            _assuntoRepository = assuntoRepository;
        }

        public async Task<AssuntoGetByIdCommandResponse> Handle(AssuntoGetByIdCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new AssuntoGetByIdCommandResponse();
            var valid = RequestBase<AssuntoGetByIdCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var assunto = await _assuntoRepository.GetByIdAsync(request.Id);

            response.Assunto = assunto;
            return response; 
        }
    }
}
