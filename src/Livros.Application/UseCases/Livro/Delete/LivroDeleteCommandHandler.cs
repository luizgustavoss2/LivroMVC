using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Application.Notifications;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class LivroDeleteCommandHandler : IRequestHandler<LivroDeleteCommandRequest, LivroDeleteCommandResponse>
    {
        private readonly IRepositoryLivro _livroRepository;
        private readonly IRepositoryLivro_Assunto _assuntoRepository;
        private readonly IRepositoryLivro_Autor _autorRepository;
        private readonly IRepositoryPreco _precoRepository;
        public LivroDeleteCommandHandler(IRepositoryLivro livroRepository, IRepositoryLivro_Assunto assuntoRepository, IRepositoryLivro_Autor autorRepository, IRepositoryPreco precoRepository)
        {
            _livroRepository = livroRepository;
            _assuntoRepository = assuntoRepository;
            _autorRepository = autorRepository;
            _precoRepository = precoRepository;
        }

        public async Task<LivroDeleteCommandResponse> Handle(LivroDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new LivroDeleteCommandResponse();
            var valid = RequestBase<LivroDeleteCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var livro = _livroRepository.GetByIdAsync(request.CodL);

            if(livro.Result is null)
            {
                response.AddNotification("Code", "Livro not found!", ErrorCode.NotFound);
                return response;
            }

            _ = await _assuntoRepository.DeleteAsync(request.CodL);
            _ = await _autorRepository.DeleteAsync(request.CodL);
            _ = await _precoRepository.DeleteAsync(request.CodL);
            _ = await _livroRepository.DeleteAsync(request.CodL);

            return response;
        }
    }
}
