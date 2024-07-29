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
    public class AssuntoDeleteCommandHandler : IRequestHandler<AssuntoDeleteCommandRequest, AssuntoDeleteCommandResponse>
    {
        private readonly IRepositoryAssunto _assuntoRepository;
        public AssuntoDeleteCommandHandler(IRepositoryAssunto assuntoRepository)
        {
            _assuntoRepository = assuntoRepository;
        }

        public async Task<AssuntoDeleteCommandResponse> Handle(AssuntoDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new AssuntoDeleteCommandResponse();
            var valid = RequestBase<AssuntoDeleteCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var assunto = _assuntoRepository.GetByIdAsync(request.CodAs);

            if(assunto.Result is null)
            {
                response.AddNotification("Code", "Assunto not found!", ErrorCode.NotFound);
                return response;
            }
            try
            {
                _ = await _assuntoRepository.DeleteAsync(request.CodAs);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("conflicted with the REFERENCE"))
                {
                    response.AddNotification("Code", "Assunto não pode ser exluído. Vinculo com livros!", ErrorCode.Conflict);
                    return response;
                }

                response.AddNotification("Code", ex.Message);
            }
            return response;
        }
    }
}
