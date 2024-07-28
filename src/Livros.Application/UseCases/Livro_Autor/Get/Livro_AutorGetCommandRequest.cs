using MediatR;

namespace Livros.Application.UseCases
{
    public class Livro_AutorGetCommandRequest : RequestBase<Livro_AutorGetCommandRequest>, IRequest<Livro_AutorGetCommandResponse>
    {
    }
}
