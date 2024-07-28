using System;
using Livros.Application.Notifications;
namespace Livros.Application.UseCases
{
    public class Livro_AutorUpdateCommandResponse : ResponseBase
    {
         public int CodL { get; set; }
    }
}
