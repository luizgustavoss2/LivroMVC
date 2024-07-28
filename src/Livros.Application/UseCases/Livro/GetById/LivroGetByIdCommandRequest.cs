using FluentValidation;
using MediatR;
using System;

namespace Livros.Application.UseCases
{
    public class LivroGetByIdCommandRequest : RequestBase<LivroGetByIdCommandRequest>, IRequest<LivroGetByIdCommandResponse>
    {
        public LivroGetByIdCommandRequest(int codL)
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
