using MediatR;
using System; 
using System.Threading;
using System.Threading.Tasks;
using Livros.Application.Notifications;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using System.Linq;

namespace Livros.Application.UseCases
{
    public class LivroUpdateCommandHandler : IRequestHandler<LivroUpdateCommandRequest, LivroUpdateCommandResponse>
    {
        private readonly IRepositoryLivro _livroRepository;
        private readonly IRepositoryLivro_Assunto _livroAssuntoRepository;
        private readonly IRepositoryLivro_Autor _livroAutorRepository;
        private readonly IRepositoryPreco _precoRepository;
        public LivroUpdateCommandHandler(IRepositoryLivro livroRepository, IRepositoryLivro_Assunto livroAssuntoRepository, IRepositoryLivro_Autor livroAutorRepository, IRepositoryPreco precoRepository)
        {
            _livroRepository = livroRepository;
            _livroAssuntoRepository = livroAssuntoRepository;
            _livroAutorRepository = livroAutorRepository;
            _precoRepository = precoRepository;
        }

        public async Task<LivroUpdateCommandResponse> Handle(LivroUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new LivroUpdateCommandResponse();
            var valid = RequestBase<LivroUpdateCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var livro = _livroRepository.GetByIdAsync(request.CodL);

            if(livro.Result is null)
            {
                response.AddNotification("Id", "Livro not found!", ErrorCode.NotFound);
                return response;
            }

            response.CodL = await _livroRepository.UpdateAsync((Livro)request);

            if (request.CodAs > 0)
            {
                await _livroAssuntoRepository.UpdateAsync(new Livro_Assunto(request.CodL, request.CodAs));
            }
            else
            {
                await _livroAssuntoRepository.DeleteAsync(request.CodL);
            }

            await _livroAutorRepository.DeleteAsync(request.CodL);

            if (request.ListCodAu != null && request.ListCodAu.Any())
            {
                foreach(var codAu in request.ListCodAu)
                {
                    await _livroAutorRepository.CreateAsync(new Livro_Autor(request.CodL, codAu));
                }
            }


            if (request.Precos != null && request.Precos.Any())
            {
                await _precoRepository.DeleteAsync(request.CodL);

                foreach (var preco in request.Precos)
                {
                    await _precoRepository.CreateAsync(new Preco() { Livro_CodL = request.CodL, Valor = preco.Valor, Tipo = preco.Tipo });
                }
            }
            

            return response;
        }
    }
}
