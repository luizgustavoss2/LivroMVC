﻿using Livro.Presentation.Web.Models;
using Livro.Web.Models;
using Livros.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Livro.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            LoadViewBags();

            ViewBag.DadosJson = JsonConvert.SerializeObject(GetLivros());

            return View();
        }

        private void LoadViewBags()
        {
            var requestAssunto = new AssuntoGetCommandRequest();
            var resultAssunto = _mediator.Send(requestAssunto).Result;
            ViewBag.TotalAssuntos = resultAssunto.Assunto.Count();

            var requestAutor = new AutorGetCommandRequest();
            var resultAutor = _mediator.Send(requestAutor).Result;
            ViewBag.TotalAutores = resultAutor.Autor.Count();

            var requestLivros = new LivroGetCommandRequest();
            var resultLivros = _mediator.Send(requestLivros).Result;

            ViewBag.TotalLivros = resultLivros.Livro.Count();
        }

        private List<LivroPorAssuntoViewModel> GetLivros()
        {
            var livrosPorAssunto = new List<LivroPorAssuntoViewModel>();
            var request = new LivroGetCommandRequest();
            var result = _mediator.Send(request).Result;

            if (result.Livro != null && result.Livro.Any())
            {
                foreach (var livro in result.Livro.GroupBy(x => x.Assunto.Descricao).Select(group => new
                {
                    Assunto = group.Key,
                    Count = group.Count()
                }).OrderBy(x => x.Assunto))
                {
                    livrosPorAssunto.Add(new LivroPorAssuntoViewModel { Assunto = livro.Assunto, Quantidade = livro.Count });
                }
            }


            return livrosPorAssunto;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

