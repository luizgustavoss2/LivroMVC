using System;
using System.Collections.Generic;
using FluentValidation;
using MediatR;

namespace Livros.Application.UseCases
{
    public class LivroUpdatePrecoRequest : RequestBase<LivroUpdatePrecoRequest>, IRequest<LivroUpdateCommandResponse>
    {
        public LivroUpdatePrecoRequest(decimal valor, int tipo)
        {
            Valor = valor;
            Tipo = tipo;
            ValidateModel();
        }
        public decimal Valor { get; set; }
        public int Tipo { get; set; }


        private void ValidateModel()
        {
            ValidateModel(this);
        }
    }
}
