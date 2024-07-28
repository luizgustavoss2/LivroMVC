using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class Livro_AssuntoDeleteCommandHandler : IRequestHandler<Livro_AssuntoDeleteCommandRequest, Livro_AssuntoDeleteCommandResponse>
    {
        private readonly IRepositoryLivro_Assunto _livro_AssuntoRepository;
        public Livro_AssuntoDeleteCommandHandler(IRepositoryLivro_Assunto livro_AssuntoRepository)
        {
            _livro_AssuntoRepository = livro_AssuntoRepository;
        }

        public async Task<Livro_AssuntoDeleteCommandResponse> Handle(Livro_AssuntoDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new Livro_AssuntoDeleteCommandResponse();
            var valid = RequestBase<Livro_AssuntoDeleteCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            //var livro_Assunto = _livro_AssuntoRepository.GetAsync<Livro_Assunto>(request.Id);

            //if(livro_Assunto.Result is null)
            //{
            //    response.AddNotification("Id", "Livro_Assunto not found!", ErrorCode.NotFound);
            //    return response;
            //}

            _ = await _livro_AssuntoRepository.DeleteAsync(request.CodL);
            return response;
        }
    }
}
