using Livros.Application.Notifications;
using Livros.Domain.Entities;
namespace Livros.Application.UseCases
{
    public class LivroGetByIdCommandResponse : ResponseBase
    {
         public Livro Livro { get; set; }
    }
}
