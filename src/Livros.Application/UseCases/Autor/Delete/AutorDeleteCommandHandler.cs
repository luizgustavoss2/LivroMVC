using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Livros.Domain.Entities;
using Livros.Application.Notifications;
using Livros.Domain.Interfaces.Repository;
using System.Data.SqlClient;

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

            if (autor.Result is null)
            {
                response.AddNotification("Code", "Autor not found!", ErrorCode.NotFound);
                return response;
            }
            try
            {
                _ = await _autorRepository.DeleteAsync(request.Id);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("conflicted with the REFERENCE"))
                {
                    response.AddNotification("Code", "Autor não pode ser excluído. Vinculo com livros!", ErrorCode.Conflict);
                    return response;
                }

                response.AddNotification("Code", ex.Message);
            }
            return response;
        }
    }
}
