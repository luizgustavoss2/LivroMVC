using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Livro.Presentation.Web.Models
{
    public class AssuntoViewModel
    {
        public int CodAs { get; set; }

        [Required(ErrorMessage = "O 'Descrição' deve ser preenchido.")]
        [MinLength(3, ErrorMessage = "O 'Descrição' deve ter no mínimo 3 caracteres.")]
        [MaxLength(20, ErrorMessage = "O 'Descrição' deve ter no máximo 20 caracteres.")]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
    }
}
