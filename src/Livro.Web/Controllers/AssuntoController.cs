using Livro.Presentation.Web.Models;
using Livros.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livro.Presentation.Web.Controllers
{
    public class AssuntoController : Controller
    {
        private readonly IMediator _mediator;

        public AssuntoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Index()
        {
            var lstAssuntoViewModel = new List<AssuntoViewModel>();

            var request = new AssuntoGetCommandRequest();
            var result = _mediator.Send(request).Result;

            if (result.Assunto != null && result.Assunto.Any())
            {
                foreach (var dto in result.Assunto)
                {
                    lstAssuntoViewModel.Add(new AssuntoViewModel() { CodAs = dto.CodAs.Value, Descricao = dto.Descricao });
                }
            }

            return View(lstAssuntoViewModel);
        }

        [HttpPost]
        public IActionResult Create(AssuntoViewModel contatoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var request = new AssuntoInsertCommandRequest(
                    descricao: contatoViewModel.Descricao);

                var result = _mediator.Send(request).Result;

                TempData[Constants.Message.SUCCESS] = "Assunto criado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData[Constants.Message.ERROR] = ex.Message;
            }
            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            var request = new AssuntoGetByIdCommandRequest(id);
            var result = _mediator.Send(request).Result;
            var assuntoViewModel = new AssuntoViewModel();
            if (result.Assunto != null)
            {
                assuntoViewModel.CodAs = result.Assunto.CodAs.Value;
                assuntoViewModel.Descricao = result.Assunto.Descricao;
            }
            return View(assuntoViewModel);
        }

        [HttpPost]
        public IActionResult Update(AssuntoViewModel assuntoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var request = new AssuntoUpdateCommandRequest(
                    codAs: assuntoViewModel.CodAs,
                    descricao: assuntoViewModel.Descricao
                );

                var result = _mediator.Send(request).Result;

                TempData[Constants.Message.SUCCESS] = "Assunto atualizado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData[Constants.Message.ERROR] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var request = new AssuntoDeleteCommandRequest(codAs: id);
                var result = _mediator.Send(request).Result;

                if (result.Error.HasValue)
                {
                    TempData[Constants.Message.ERROR] = result.Notifications[0].Message;
                    return RedirectToAction("Index");
                }

                TempData[Constants.Message.SUCCESS] = "Assunto excluído com sucesso.";
            }
            catch (Exception ex)
            {
                TempData[Constants.Message.ERROR] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
