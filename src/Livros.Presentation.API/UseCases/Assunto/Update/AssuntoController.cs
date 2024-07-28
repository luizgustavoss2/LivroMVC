using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.ResponseError;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Assunto.Update
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class AssuntoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UpdateAssuntoPresenter _updateAssuntoPresenter;
        public AssuntoController(IMediator mediator, UpdateAssuntoPresenter updateAssuntoPresenter)
        {
            _mediator = mediator;
            _updateAssuntoPresenter = updateAssuntoPresenter; 
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Patch(int id,[FromBody]UpdateAssuntoRequest updateRequest)
        {
            var request = new AssuntoUpdateCommandRequest(
              codAs: id,
              descricao: updateRequest.Descricao
            );
            var result = await _mediator.Send(request);
            return _updateAssuntoPresenter.GetActionResult(result, result.CodAs, HttpStatusCode.NoContent);
        }
    }
}
