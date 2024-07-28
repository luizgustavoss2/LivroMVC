using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Livro.Presentation.Web.Models
{
    public class PrecoViewModel
    {
        public int CodPr { get; set; }

        [Required(ErrorMessage = "O 'Preço' deve ser preenchido.")]
        [DisplayName("Valor")]
        public decimal Valor { get; set; }

        [DisplayName("Tipo")]
        public int Tipo { get; set; }
    }
}
