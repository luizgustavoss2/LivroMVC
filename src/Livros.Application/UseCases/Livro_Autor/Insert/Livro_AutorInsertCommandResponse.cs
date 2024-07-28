using System;
using Livros.Application.Notifications;
namespace Livros.Application.UseCases
{
    public class Livro_AutorInsertCommandResponse : ResponseBase
    {
         public int CodL { get; set; }
    }
}
