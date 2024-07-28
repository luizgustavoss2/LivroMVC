using System.Collections.Generic;
using Livros.Application.Notifications;
using Livros.Domain.Entities;
namespace Livros.Application.UseCases
{
    public class AutorGetCommandResponse : ResponseBase
    {
        public IEnumerable<Autor> Autor { get; set; }
    }
}
