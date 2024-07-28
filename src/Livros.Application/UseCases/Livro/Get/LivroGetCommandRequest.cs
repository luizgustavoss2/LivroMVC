using MediatR;

namespace Livros.Application.UseCases
{
    public class LivroGetCommandRequest : RequestBase<LivroGetCommandRequest>, IRequest<LivroGetCommandResponse>
    {
    }
}
