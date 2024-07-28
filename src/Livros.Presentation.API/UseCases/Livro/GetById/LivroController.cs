using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Livro.GetById
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GetByIdLivroPresenter _getByIdLivroPresenter;
        public LivroController(IMediator mediator, GetByIdLivroPresenter getByIdLivroPresenter)
        {
            _mediator = mediator;
            _getByIdLivroPresenter = getByIdLivroPresenter; 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request = new LivroGetByIdCommandRequest(id);
            var result = await _mediator.Send(request);
            return _getByIdLivroPresenter.GetActionResult(result, result.Livro, HttpStatusCode.OK);
        }
    }
}
