using FluentValidation;
using MediatR;
using System;

namespace Livros.Application.UseCases
{
    public class PrecoDeleteCommandRequest : RequestBase<PrecoDeleteCommandRequest>, IRequest<PrecoDeleteCommandResponse>
    {
        public PrecoDeleteCommandRequest(int codL)
        {
            CodL = codL;
            ValidateModel();
        }

        public int CodL { get; set; }

        private void ValidateModel()
        {
            ValidateModel(this);
        }
    }
}
