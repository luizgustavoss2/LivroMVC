using System;
using Livros.Application.Notifications;
namespace Livros.Application.UseCases
{
    public class AssuntoInsertCommandResponse : ResponseBase
    {
         public int CodAs { get; set; }
    }
}
