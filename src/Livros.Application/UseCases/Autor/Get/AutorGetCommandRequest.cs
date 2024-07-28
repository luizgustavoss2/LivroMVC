using MediatR;

namespace Livros.Application.UseCases
{
    public class AutorGetCommandRequest : RequestBase<AutorGetCommandRequest>, IRequest<AutorGetCommandResponse>
    {
    }
}
