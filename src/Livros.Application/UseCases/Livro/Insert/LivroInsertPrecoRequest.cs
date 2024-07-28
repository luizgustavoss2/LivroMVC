using System;
using System.Collections.Generic;
using FluentValidation;
using MediatR;

namespace Livros.Application.UseCases
{
    public class LivroInsertPrecoRequest : RequestBase<LivroInsertPrecoRequest>, IRequest<LivroInsertCommandResponse>
    {
        public LivroInsertPrecoRequest(decimal valor, int tipo)
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
