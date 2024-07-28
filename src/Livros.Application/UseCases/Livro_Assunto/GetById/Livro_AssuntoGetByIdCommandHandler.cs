using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Livros.Infra.Data.Entities;

namespace Livros.Application.UseCases
{
    public class Livro_AssuntoGetByIdCommandHandler : IRequestHandler<Livro_AssuntoGetByIdCommandRequest, Livro_AssuntoGetByIdCommandResponse>
    {
        private readonly IRepositoryLivro_Assunto _livro_AssuntoRepository;
        public Livro_AssuntoGetByIdCommandHandler(IRepositoryLivro_Assunto livro_AssuntoRepository)
        {
            _livro_AssuntoRepository = livro_AssuntoRepository;
        }

        public async Task<Livro_AssuntoGetByIdCommandResponse> Handle(Livro_AssuntoGetByIdCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new Livro_AssuntoGetByIdCommandResponse();
            var valid = RequestBase<Livro_AssuntoGetByIdCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var livro_Assunto = await _livro_AssuntoRepository.GetByIdAsync(request.CodL);

            response.Livro_Assunto = livro_Assunto;
            return response; 
        }
    }
}
