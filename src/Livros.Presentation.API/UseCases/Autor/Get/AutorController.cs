using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Autor.Get
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GetAutorPresenter _getAutorPresenter;
        public AutorController(IMediator mediator, GetAutorPresenter getAutorPresenter)
        {
            _mediator = mediator;
            _getAutorPresenter = getAutorPresenter; 
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new AutorGetCommandRequest();
            var result = await _mediator.Send(request); 
            return _getAutorPresenter.GetActionResult(result, result.Autor, HttpStatusCode.OK); 
        }
    }
}
