using Livros.Application.Notifications;
using Livros.Domain.Entities;
namespace Livros.Application.UseCases
{
    public class AutorGetByIdCommandResponse : ResponseBase
    {
         public Autor Autor { get; set; }
    }
}
