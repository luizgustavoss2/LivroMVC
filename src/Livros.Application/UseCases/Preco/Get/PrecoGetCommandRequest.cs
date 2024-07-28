using MediatR;

namespace Livros.Application.UseCases
{
    public class PrecoGetCommandRequest : RequestBase<PrecoGetCommandRequest>, IRequest<PrecoGetCommandResponse>
    {
    }
}
