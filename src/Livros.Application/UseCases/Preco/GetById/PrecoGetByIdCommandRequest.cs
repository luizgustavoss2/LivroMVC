using FluentValidation;
using MediatR;
using System;

namespace Livros.Application.UseCases
{
    public class PrecoGetByIdCommandRequest : RequestBase<PrecoGetByIdCommandRequest>, IRequest<PrecoGetByIdCommandResponse>
    {
        public PrecoGetByIdCommandRequest(int codL)
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
