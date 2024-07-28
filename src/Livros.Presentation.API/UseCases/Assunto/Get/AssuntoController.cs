using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Assunto.Get
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class AssuntoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GetAssuntoPresenter _getAssuntoPresenter;
        public AssuntoController(IMediator mediator, GetAssuntoPresenter getAssuntoPresenter)
        {
            _mediator = mediator;
            _getAssuntoPresenter = getAssuntoPresenter; 
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new AssuntoGetCommandRequest();
            var result = await _mediator.Send(request); 
            return _getAssuntoPresenter.GetActionResult(result, result.Assunto, HttpStatusCode.OK); 
        }
    }
}
