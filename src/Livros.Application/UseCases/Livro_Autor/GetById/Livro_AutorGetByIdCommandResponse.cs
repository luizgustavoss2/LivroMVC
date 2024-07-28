using Livros.Application.Notifications;
using Livros.Domain.Entities;
namespace Livros.Application.UseCases
{
    public class Livro_AutorGetByIdCommandResponse : ResponseBase
    {
         public Livro_Autor Livro_Autor { get; set; }
    }
}
