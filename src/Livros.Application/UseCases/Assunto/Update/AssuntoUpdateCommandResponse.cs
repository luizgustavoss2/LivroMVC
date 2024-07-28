using System;
using Livros.Application.Notifications;
namespace Livros.Application.UseCases
{
    public class AssuntoUpdateCommandResponse : ResponseBase
    {
         public int CodAs { get; set; }
    }
}
