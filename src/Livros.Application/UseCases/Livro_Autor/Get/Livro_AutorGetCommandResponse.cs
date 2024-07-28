using System.Collections.Generic;
using Livros.Application.Notifications;
using Livros.Domain.Entities;
namespace Livros.Application.UseCases
{
    public class Livro_AutorGetCommandResponse : ResponseBase
    {
        public IEnumerable<Livro_Autor> Livro_Autor { get; set; }
    }
}
