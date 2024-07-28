using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Application.Notifications;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class AutorDeleteCommandHandler : IRequestHandler<AutorDeleteCommandRequest, AutorDeleteCommandResponse>
    {
        private readonly IRepositoryAutor _autorRepository;
        public AutorDeleteCommandHandler(IRepositoryAutor autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<AutorDeleteCommandResponse> Handle(AutorDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new AutorDeleteCommandResponse();
            var valid = RequestBase<AutorDeleteCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var autor = _autorRepository.GetByIdAsync(request.Id);

            if(autor.Result is null)
            {
                response.AddNotification("Code", "Autor not found!", ErrorCode.NotFound);
                return response;
            }

            _ = await _autorRepository.DeleteAsync(request.Id);
            return response;
        }
    }
}
