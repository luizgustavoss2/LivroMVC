using System;
using Livros.Application.Notifications;
namespace Livros.Application.UseCases
{
    public class LivroInsertCommandResponse : ResponseBase
    {
         public int CodL { get; set; }
    }
}
