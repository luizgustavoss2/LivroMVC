using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Livro.Presentation.Web.Models
{
    public class AutorViewModel
    {
        public int CodAu { get; set; }

        [Required(ErrorMessage = "O 'Nome' deve ser preenchido.")]
        [MinLength(3, ErrorMessage = "O 'Nome' deve ter no mínimo 3 caracteres.")]
        [MaxLength(40, ErrorMessage = "O 'Nome' deve ter no máximo 40 caracteres.")]
        [DisplayName("Nome")]
        public string Nome { get; set; }
    }
}
