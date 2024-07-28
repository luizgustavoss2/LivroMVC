using System;
using Livros.Application.Notifications;
namespace Livros.Application.UseCases
{
    public class PrecoUpdateCommandResponse : ResponseBase
    {
         public int CodPr { get; set; }
    }
}
