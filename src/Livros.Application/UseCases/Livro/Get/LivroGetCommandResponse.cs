using System.Collections.Generic;
using Livros.Application.Notifications;
using Livros.Domain.Entities;
namespace Livros.Application.UseCases
{
    public class LivroGetCommandResponse : ResponseBase
    {
        public IList<Livro> Livro { get; set; }
    }
}
