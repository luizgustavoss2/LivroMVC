using MediatR;
using System; 
using System.Threading;
using System.Threading.Tasks;
using Livros.Application.Notifications;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class Livro_AutorUpdateCommandHandler : IRequestHandler<Livro_AutorUpdateCommandRequest, Livro_AutorUpdateCommandResponse>
    {
        private readonly IRepositoryLivro_Autor _livro_AutorRepository;
        public Livro_AutorUpdateCommandHandler(IRepositoryLivro_Autor livro_AutorRepository)
        {
            _livro_AutorRepository = livro_AutorRepository;
        }

        public async Task<Livro_AutorUpdateCommandResponse> Handle(Livro_AutorUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new Livro_AutorUpdateCommandResponse();
            var valid = RequestBase<Livro_AutorUpdateCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var livro_Autor = _livro_AutorRepository.GetByIdAsync(request.Livro_CodL.Value);

            if(livro_Autor.Result is null)
            {
                response.AddNotification("CodL", "Livro_Autor not found!", ErrorCode.NotFound);
                return response;
            }

            response.CodL = await _livro_AutorRepository.UpdateAsync((Livro_Autor)request);

            return response;
        }
    }
}
