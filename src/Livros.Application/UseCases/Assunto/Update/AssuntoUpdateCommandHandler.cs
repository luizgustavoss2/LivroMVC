using MediatR;
using System; 
using System.Threading;
using System.Threading.Tasks;
using Livros.Application.Notifications;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class AssuntoUpdateCommandHandler : IRequestHandler<AssuntoUpdateCommandRequest, AssuntoUpdateCommandResponse>
    {
        private readonly IRepositoryAssunto _assuntoRepository;
        public AssuntoUpdateCommandHandler(IRepositoryAssunto assuntoRepository, IRepository<Assunto> genericRepository)
        {
            _assuntoRepository = assuntoRepository;
        }

        public async Task<AssuntoUpdateCommandResponse> Handle(AssuntoUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new AssuntoUpdateCommandResponse();
            var valid = RequestBase<AssuntoUpdateCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var assunto = _assuntoRepository.GetByIdAsync(request.CodAs);

            if(assunto.Result is null)
            {
                response.AddNotification("Code", "Assunto not found!", ErrorCode.NotFound);
                return response;
            }

            response.CodAs = await _assuntoRepository.UpdateAsync((Assunto)request);

            return response;
        }
    }
}
