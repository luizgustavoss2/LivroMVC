using System;
using FluentValidation;
using MediatR;

namespace Livros.Application.UseCases
{
    public class PrecoUpdateCommandRequest : RequestBase<PrecoUpdateCommandRequest>, IRequest<PrecoUpdateCommandResponse>
    {
        public PrecoUpdateCommandRequest(int? codPr, int? livro_CodL, decimal? valor, int? tipo)
        {
            CodPr = codPr;
            Livro_CodL = livro_CodL;
            Valor = valor;
            Tipo = tipo;
            ValidateModel();
        }
        public int? CodPr { get; set; }
        public int? Livro_CodL { get; set; }
        public decimal? Valor { get; set; }
        public int? Tipo { get; set; }

        private void ValidateModel()
        {
            RuleFor(x => x.CodPr).NotEmpty().WithMessage("CodPr is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.Livro_CodL).NotEmpty().WithMessage("Livro_CodL is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.Valor).NotEmpty().WithMessage("Valor is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.Tipo).NotEmpty().WithMessage("Tipo is required.").WithState(x => x.Code = NotificationCode.FieldMissing);

            ValidateModel(this);
        }

        public static explicit operator Domain.Entities.Preco(PrecoUpdateCommandRequest request)
        {
            if (request is null)
                return null;

            return new Domain.Entities.Preco()
            {
               CodPr = request.CodPr,
               Livro_CodL = request.Livro_CodL,
               Valor = request.Valor,
               Tipo = request.Tipo
            };
        }
    }
}
