using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Livro.Presentation.Web.Models
{
    public class LivroViewModel
    {
        public int CodL { get; set; }

        [Required(ErrorMessage = "O 'Título' deve ser preenchido.")]
        [MinLength(3, ErrorMessage = "O 'Título' deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O 'Título' deve ter no máximo 40 caracteres.")]
        [DisplayName("Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O 'Editora' deve ser preenchido.")]
        [MinLength(3, ErrorMessage = "O 'Editora' deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O 'Editora' deve ter no máximo 40 caracteres.")]
        [DisplayName("Editora")]
        public string Editora { get; set; }

        [Required(ErrorMessage = "O 'Edição' deve ser preenchido.")]       
        [DisplayName("Edição")]
        public int Edicao { get; set; }

        [Required(ErrorMessage = "O 'Ano Publicação' deve ser preenchido.")]
        //[MinLength(4, ErrorMessage = "O 'Ano Publicação' deve ter no mínimo 4 caracteres.")]
        //[MaxLength(4, ErrorMessage = "O 'Ano Publicação' deve ter no máximo 4 caracteres.")]
        
        [Range(1000, 2099, ErrorMessage = "Ano deve ser um número de 4 dígitos entre 1900 e 2099")]
        [DisplayName("Ano Publicação")]
        public int AnoPublicacao { get; set; }

        [DisplayName("Assunto")]
        public AssuntoViewModel Assunto { get; set; }

        public int CodAs { get; set; }

        [DisplayName("Autores")]
        public List<AutorViewModel> Autores { get; set; }

        [DisplayName("Autores")]
        public List<PrecoViewModel> Precos { get; set; }
        

        public LivroViewModel()
        {
            Autores = new List<AutorViewModel>();
            Precos = new List<PrecoViewModel>();
        }


    }
}
