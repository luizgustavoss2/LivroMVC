using System;
using Livros.Application.Notifications;
namespace Livros.Application.UseCases
{
    public class AutorInsertCommandResponse : ResponseBase
    {
         public int CodAu { get; set; }
    }
}
