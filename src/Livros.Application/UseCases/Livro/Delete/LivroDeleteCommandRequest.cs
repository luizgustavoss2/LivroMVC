using FluentValidation;
using MediatR;
using System;

namespace Livros.Application.UseCases
{
    public class LivroDeleteCommandRequest : RequestBase<LivroDeleteCommandRequest>, IRequest<LivroDeleteCommandResponse>
    {
        public LivroDeleteCommandRequest(int codL)
        {
            CodL = codL;
            ValidateModel();
        }

        public int CodL { get; set; }

        private void ValidateModel()
        {
            RuleFor(x => x.CodL).NotEmpty().WithMessage("Code is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            ValidateModel(this);
        }
    }
}
