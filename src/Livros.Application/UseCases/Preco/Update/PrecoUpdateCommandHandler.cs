using MediatR;
using System; 
using System.Threading;
using System.Threading.Tasks;
using Livros.Application.Notifications;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class PrecoUpdateCommandHandler : IRequestHandler<PrecoUpdateCommandRequest, PrecoUpdateCommandResponse>
    {
        private readonly IRepositoryPreco _precoRepository;
        public PrecoUpdateCommandHandler(IRepositoryPreco precoRepository)
        {
            _precoRepository = precoRepository;
        }

        public async Task<PrecoUpdateCommandResponse> Handle(PrecoUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new PrecoUpdateCommandResponse();
            var valid = RequestBase<PrecoUpdateCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var preco = _precoRepository.GetByLivroIdAsync(request.CodPr.Value);

            if(preco.Result is null)
            {
                response.AddNotification("Code", "Preco not found!", ErrorCode.NotFound);
                return response;
            }

            response.CodPr = await _precoRepository.UpdateAsync((Preco)request);

            return response;
        }
    }
}
