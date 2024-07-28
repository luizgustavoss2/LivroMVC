using System;
using Livros.Application.Notifications;
namespace Livros.Application.UseCases
{
    public class Livro_AssuntoInsertCommandResponse : ResponseBase
    {
         public int CodL { get; set; }
    }
}
