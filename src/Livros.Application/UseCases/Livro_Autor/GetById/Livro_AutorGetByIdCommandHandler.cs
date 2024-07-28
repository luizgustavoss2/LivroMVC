using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Livros.Infra.Data.Entities;

namespace Livros.Application.UseCases
{
    public class Livro_AutorGetByIdCommandHandler : IRequestHandler<Livro_AutorGetByIdCommandRequest, Livro_AutorGetByIdCommandResponse>
    {
        private readonly IRepository<Livro_Autor> _livro_AutorRepository;
        public Livro_AutorGetByIdCommandHandler(IRepository<Livro_Autor> livro_AutorRepository)
        {
            _livro_AutorRepository = livro_AutorRepository;
        }

        public async Task<Livro_AutorGetByIdCommandResponse> Handle(Livro_AutorGetByIdCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new Livro_AutorGetByIdCommandResponse();
            var valid = RequestBase<Livro_AutorGetByIdCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var livro_Autor = await _livro_AutorRepository.GetAsync<Livro_AutorPersistence>(request.CodL);

            response.Livro_Autor = livro_Autor;
            return response; 
        }
    }
}
