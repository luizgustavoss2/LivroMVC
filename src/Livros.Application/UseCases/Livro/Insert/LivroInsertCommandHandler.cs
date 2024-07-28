using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using System.Linq;

namespace Livros.Application.UseCases
{
    public class LivroInsertCommandHandler : IRequestHandler<LivroInsertCommandRequest, LivroInsertCommandResponse>
    {
        private readonly IRepositoryLivro _livroRepository;
        private readonly IRepositoryLivro_Assunto _livroAssuntoRepository;
        private readonly IRepositoryLivro_Autor _livroAutorRepository;
        private readonly IRepositoryPreco _precoRepository;
        public LivroInsertCommandHandler(IRepositoryLivro livroRepository, IRepositoryLivro_Assunto livroAssuntoRepository, IRepositoryLivro_Autor livroAutorRepository, IRepositoryPreco precoRepository)
        {
            _livroRepository = livroRepository;
            _livroAssuntoRepository = livroAssuntoRepository;
            _livroAutorRepository = livroAutorRepository;
            _precoRepository = precoRepository;
        }

        public async Task<LivroInsertCommandResponse> Handle(LivroInsertCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new LivroInsertCommandResponse();
            var valid = RequestBase<LivroInsertCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var objLivro = PrepareObject(request);

            response.CodL = await _livroRepository.CreateAsync(objLivro);

            if (request.CodAs > 0)
            {
                await _livroAssuntoRepository.CreateAsync(new Livro_Assunto(response.CodL, request.CodAs));
            }
            if (request.ListCodAu != null && request.ListCodAu.Any())
            {
                foreach (var autor in request.ListCodAu)
                {
                    await _livroAutorRepository.CreateAsync(new Livro_Autor(response.CodL, autor));
                }

                if (request.Precos != null && request.Any())
                {
                    foreach (var preco in request.Precos)
                    {
                        await _precoRepository.CreateAsync(new Preco() { Livro_CodL = response.CodL, Valor = preco.Valor, Tipo = preco.Tipo });
                    }
                }
            }

            return response;
        }

        private Livro PrepareObject(LivroInsertCommandRequest request) => new Livro(null, request.Titulo, request.Editora, request.Edicao, request.AnoPublicacao);
    }
}
