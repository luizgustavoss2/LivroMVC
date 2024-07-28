using System;
using Livros.Application.Notifications;
namespace Livros.Application.UseCases
{
    public class LivroUpdateCommandResponse : ResponseBase
    {
         public int CodL { get; set; }
    }
}
