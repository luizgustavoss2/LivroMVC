using Livros.Application.Notifications;
using Livros.Domain.Entities;
namespace Livros.Application.UseCases
{
    public class Livro_AssuntoGetByIdCommandResponse : ResponseBase
    {
         public Livro_Assunto Livro_Assunto { get; set; }
    }
}
