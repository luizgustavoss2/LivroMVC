using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Assunto.GetById
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class AssuntoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GetByIdAssuntoPresenter _getByIdAssuntoPresenter;
        public AssuntoController(IMediator mediator, GetByIdAssuntoPresenter getByIdAssuntoPresenter)
        {
            _mediator = mediator;
            _getByIdAssuntoPresenter = getByIdAssuntoPresenter; 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request = new AssuntoGetByIdCommandRequest(id);
            var result = await _mediator.Send(request);
            return _getByIdAssuntoPresenter.GetActionResult(result, result.Assunto, HttpStatusCode.OK);
        }
    }
}
