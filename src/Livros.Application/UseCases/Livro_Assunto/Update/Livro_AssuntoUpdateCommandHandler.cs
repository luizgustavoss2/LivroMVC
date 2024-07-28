using MediatR;
using System; 
using System.Threading;
using System.Threading.Tasks;
using Livros.Application.Notifications;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class Livro_AssuntoUpdateCommandHandler : IRequestHandler<Livro_AssuntoUpdateCommandRequest, Livro_AssuntoUpdateCommandResponse>
    {
        private readonly IRepositoryLivro_Assunto _livro_AssuntoRepository;
        public Livro_AssuntoUpdateCommandHandler(IRepositoryLivro_Assunto livro_AssuntoRepository)
        {
            _livro_AssuntoRepository = livro_AssuntoRepository;
        }

        public async Task<Livro_AssuntoUpdateCommandResponse> Handle(Livro_AssuntoUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new Livro_AssuntoUpdateCommandResponse();
            var valid = RequestBase<Livro_AssuntoUpdateCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var livro_Assunto = _livro_AssuntoRepository.GetByIdAsync(request.Livro_CodL.Value);

            if(livro_Assunto.Result is null)
            {
                response.AddNotification("CodL", "Livro_Assunto not found!", ErrorCode.NotFound);
                return response;
            }

            response.CodL = await _livro_AssuntoRepository.UpdateAsync((Livro_Assunto)request);

            return response;
        }
    }
}
