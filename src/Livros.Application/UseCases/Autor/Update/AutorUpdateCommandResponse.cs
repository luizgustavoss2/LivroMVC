using System;
using Livros.Application.Notifications;
namespace Livros.Application.UseCases
{
    public class AutorUpdateCommandResponse : ResponseBase
    {
         public int CodAu { get; set; }
    }
}
