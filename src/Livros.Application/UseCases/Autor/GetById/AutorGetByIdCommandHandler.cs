using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Livros.Infra.Data.Entities;

namespace Livros.Application.UseCases
{
    public class AutorGetByIdCommandHandler : IRequestHandler<AutorGetByIdCommandRequest, AutorGetByIdCommandResponse>
    {
        private readonly IRepositoryAutor _autorRepository;
        public AutorGetByIdCommandHandler(IRepositoryAutor autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<AutorGetByIdCommandResponse> Handle(AutorGetByIdCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new AutorGetByIdCommandResponse();
            var valid = RequestBase<AutorGetByIdCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var autor = await _autorRepository.GetByIdAsync(request.Id);

            response.Autor = autor;
            return response; 
        }
    }
}
