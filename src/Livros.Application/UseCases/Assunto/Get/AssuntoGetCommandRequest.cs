using MediatR;

namespace Livros.Application.UseCases
{
    public class AssuntoGetCommandRequest : RequestBase<AssuntoGetCommandRequest>, IRequest<AssuntoGetCommandResponse>
    {
    }
}
