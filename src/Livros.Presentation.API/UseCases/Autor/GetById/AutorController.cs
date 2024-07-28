using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Autor.GetById
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GetByIdAutorPresenter _getByIdAutorPresenter;
        public AutorController(IMediator mediator, GetByIdAutorPresenter getByIdAutorPresenter)
        {
            _mediator = mediator;
            _getByIdAutorPresenter = getByIdAutorPresenter; 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request = new AutorGetByIdCommandRequest(id);
            var result = await _mediator.Send(request);
            return _getByIdAutorPresenter.GetActionResult(result, result.Autor, HttpStatusCode.OK);
        }
    }
}
