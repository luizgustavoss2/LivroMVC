using MediatR;
using System; 
using System.Threading;
using System.Threading.Tasks;
using Livros.Application.Notifications;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;

namespace Livros.Application.UseCases
{
    public class AutorUpdateCommandHandler : IRequestHandler<AutorUpdateCommandRequest, AutorUpdateCommandResponse>
    {
        private readonly IRepositoryAutor _autorRepository;
        public AutorUpdateCommandHandler(IRepositoryAutor autorRepository, IRepository<Autor> genericRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<AutorUpdateCommandResponse> Handle(AutorUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new AutorUpdateCommandResponse();
            var valid = RequestBase<AutorUpdateCommandRequest>.ValidateRequest(request, response);
            if (!valid)
                return response;

            var autor = _autorRepository.GetByIdAsync(request.CodAu);

            if(autor.Result is null)
            {
                response.AddNotification("CodAu", "Autor not found!", ErrorCode.NotFound);
                return response;
            }

            response.CodAu = await _autorRepository.UpdateAsync((Autor)request);

            return response;
        }
    }
}
