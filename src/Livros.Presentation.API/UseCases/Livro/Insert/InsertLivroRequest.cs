using System;
using System.Collections.Generic;

namespace Livros.Presentation.API.UseCases
{
    public class InsertLivroRequest
    {
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int? Edicao { get; set; }
        public int AnoPublicacao { get; set; }
        public int CodAs { get; set; }
        public List<int> ListCodAu { get; set; }
    }
}
