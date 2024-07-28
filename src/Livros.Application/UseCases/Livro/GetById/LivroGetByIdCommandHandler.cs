using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Livros.Infra.Data.Entities;

namespace Livros.Application.UseCases
{
    public class LivroGetByIdCommandHandler : IRequestHandler<LivroGetByIdCommandRequest, LivroGetByIdCommandResponse>
    {
        private readonly IRepositoryLivro _livroRepository;

        private readonly IRepositoryAutor _autorRepository;

        private readonly IRepositoryPreco _precoRepository;
        public LivroGetByIdCommandHandler(IRepositoryLivro livroRepository, IRepositoryAutor autorRepository, IRepositoryPreco precoRepository)
        {
            _livroRepository = livroRepository;
            _autorRepository = autorRepository;
            _precoRepository = precoRepository;
        }

        public async Task<LivroGetByIdCommandResponse> Handle(LivroGetByIdCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new LivroGetByIdCommandResponse();
            var valid = RequestBase<LivroGetByIdCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var livro = await _livroRepository.GetByIdAsync(request.CodL);
            if( livro.CodL > 0)
            {
                livro.Autor = await _autorRepository.GetByLivroIdAsync(request.CodL);

                livro.Preco = await _precoRepository.GetByLivroIdAsync(request.CodL);
            }

            response.Livro = livro;
            return response; 
        }
    }
}
