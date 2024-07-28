using MediatR;

namespace Livros.Application.UseCases
{
    public class Livro_AssuntoGetCommandRequest : RequestBase<Livro_AssuntoGetCommandRequest>, IRequest<Livro_AssuntoGetCommandResponse>
    {
    }
}
