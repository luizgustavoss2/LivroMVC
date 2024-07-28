using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class AutorInsertCommandHandler : IRequestHandler<AutorInsertCommandRequest, AutorInsertCommandResponse>
    {
        private readonly IRepositoryAutor _autorRepository;
        public AutorInsertCommandHandler(IRepositoryAutor autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<AutorInsertCommandResponse> Handle(AutorInsertCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new AutorInsertCommandResponse();
            var valid = RequestBase<AutorInsertCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var objAutor = PrepareObject(request);

            response.CodAu = await _autorRepository.CreateAsync(objAutor);
            return response;
        }

        private Autor PrepareObject(AutorInsertCommandRequest request) => new Autor(null,request.Nome);
    }
}
