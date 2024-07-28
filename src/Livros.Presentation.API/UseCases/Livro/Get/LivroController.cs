using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Livro.Get
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GetLivroPresenter _getLivroPresenter;
        public LivroController(IMediator mediator, GetLivroPresenter getLivroPresenter)
        {
            _mediator = mediator;
            _getLivroPresenter = getLivroPresenter; 
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new LivroGetCommandRequest();
            var result = await _mediator.Send(request); 
            return _getLivroPresenter.GetActionResult(result, result.Livro, HttpStatusCode.OK); 
        }
    }
}
