using System;
namespace Livros.Domain.Entities
{
    public class Autor 
    {
        public Autor()
        {
        }

        public Autor(int? codAu, string nome)
        {
            CodAu = codAu;
            Nome = nome;
          
        }
        public int? CodAu { get; set; }
        public string Nome { get; set; }

    }
}
