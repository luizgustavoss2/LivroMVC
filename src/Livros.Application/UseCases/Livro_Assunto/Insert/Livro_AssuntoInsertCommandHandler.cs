using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class Livro_AssuntoInsertCommandHandler : IRequestHandler<Livro_AssuntoInsertCommandRequest, Livro_AssuntoInsertCommandResponse>
    {
        private readonly IRepositoryLivro_Assunto _livro_AssuntoRepository;
        public Livro_AssuntoInsertCommandHandler(IRepositoryLivro_Assunto livro_AssuntoRepository)
        {
            _livro_AssuntoRepository = livro_AssuntoRepository;
        }

        public async Task<Livro_AssuntoInsertCommandResponse> Handle(Livro_AssuntoInsertCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new Livro_AssuntoInsertCommandResponse();
            var valid = RequestBase<Livro_AssuntoInsertCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var objLivro_Assunto = PrepareObject(request);

            response.CodL = await _livro_AssuntoRepository.CreateAsync(objLivro_Assunto);
            return response;
        }

        private Livro_Assunto PrepareObject(Livro_AssuntoInsertCommandRequest request) => new Livro_Assunto(request.Livro_CodL, request.Assunto_CodAs);
    }
}
