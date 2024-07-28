using System;
using Livros.Application.Notifications;
namespace Livros.Application.UseCases
{
    public class PrecoInsertCommandResponse : ResponseBase
    {
         public int CodPr { get; set; }
    }
}
