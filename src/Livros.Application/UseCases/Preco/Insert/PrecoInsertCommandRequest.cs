using System;
using FluentValidation;
using MediatR;

namespace Livros.Application.UseCases
{
    public class PrecoInsertCommandRequest : RequestBase<PrecoInsertCommandRequest>, IRequest<PrecoInsertCommandResponse>
    {
        public PrecoInsertCommandRequest(int? livro_CodL, decimal? valor, int? tipo)
        {
            Livro_CodL = livro_CodL;
            Valor = valor;
            Tipo = tipo;
            ValidateModel();
        }
        public int? Livro_CodL { get; set; }
        public decimal? Valor { get; set; }
        public int? Tipo { get; set; }

        private void ValidateModel()
        {
            RuleFor(x => x.Livro_CodL).NotEmpty().WithMessage("Livro_CodL is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.Valor).NotEmpty().WithMessage("Valor is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.Tipo).NotEmpty().WithMessage("Tipo is required.").WithState(x => x.Code = NotificationCode.FieldMissing);

            ValidateModel(this);
        }
    }
}
