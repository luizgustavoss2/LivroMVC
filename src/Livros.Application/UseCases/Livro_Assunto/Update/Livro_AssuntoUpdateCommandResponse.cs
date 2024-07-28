using System;
using Livros.Application.Notifications;
namespace Livros.Application.UseCases
{
    public class Livro_AssuntoUpdateCommandResponse : ResponseBase
    {
         public int CodL { get; set; }
    }
}
