using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class Livro_AutorInsertCommandHandler : IRequestHandler<Livro_AutorInsertCommandRequest, Livro_AutorInsertCommandResponse>
    {
        private readonly IRepositoryLivro_Autor _livro_AutorRepository;
        public Livro_AutorInsertCommandHandler(IRepositoryLivro_Autor livro_AutorRepository)
        {
            _livro_AutorRepository = livro_AutorRepository;
        }

        public async Task<Livro_AutorInsertCommandResponse> Handle(Livro_AutorInsertCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new Livro_AutorInsertCommandResponse();
            var valid = RequestBase<Livro_AutorInsertCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var objLivro_Autor = PrepareObject(request);

            response.CodL = await _livro_AutorRepository.CreateAsync(objLivro_Autor);
            return response;
        }

        private Livro_Autor PrepareObject(Livro_AutorInsertCommandRequest request) => new Livro_Autor(request.Livro_CodL, request.Autor_CodAu);
    }
}
