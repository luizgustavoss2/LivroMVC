using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Livros.Infra.Data.Entities;
using System.Collections.Generic;

namespace Livros.Application.UseCases
{
    public class LivroGetCommandHandler : IRequestHandler<LivroGetCommandRequest, LivroGetCommandResponse>
    {
        private readonly IRepositoryLivro _livroRepository;
        private readonly IRepositoryAutor _autorRepository;
        private readonly IRepositoryPreco _precoRepository;
        public LivroGetCommandHandler(IRepositoryLivro livroRepository, IRepositoryAutor autorRepository, IRepositoryPreco precoRepository)
        {
            _livroRepository = livroRepository;
            _autorRepository = autorRepository;
            _precoRepository = precoRepository;
    }

        public async Task<LivroGetCommandResponse> Handle(LivroGetCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new LivroGetCommandResponse();

            var livros = await _livroRepository.GetAllAsync();

            if (livros != null && livros.Any())
            {
                response.Livro = new List<Livro>();

                foreach (var livro in livros)
                {
                    livro.Autor = await _autorRepository.GetByLivroIdAsync(livro.CodL.Value);

                    livro.Preco = await _precoRepository.GetByLivroIdAsync(livro.CodL.Value);

                    response.Livro.Add(livro);
                }
                
            }
           
            return response; 
        }
    }
}
