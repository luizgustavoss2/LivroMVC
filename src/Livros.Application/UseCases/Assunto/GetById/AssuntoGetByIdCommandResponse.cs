using Livros.Application.Notifications;
using Livros.Domain.Entities;
namespace Livros.Application.UseCases
{
    public class AssuntoGetByIdCommandResponse : ResponseBase
    {
         public Assunto Assunto { get; set; }
    }
}
