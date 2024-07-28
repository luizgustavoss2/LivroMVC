using System;
using FluentValidation;
using MediatR;

namespace Livros.Application.UseCases
{
    public class AssuntoInsertCommandRequest : RequestBase<AssuntoInsertCommandRequest>, IRequest<AssuntoInsertCommandResponse>
    {
        public AssuntoInsertCommandRequest(string descricao)
        {
            Descricao = descricao;
            ValidateModel();
        }
        public string Descricao { get; set; }

        private void ValidateModel()
        {
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("Descricao is required.").WithState(x => x.Code = NotificationCode.FieldMissing);

            ValidateModel(this);
        }
    }
}
