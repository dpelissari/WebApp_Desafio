using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using WebApp_Desafio_FrontEnd.ApiClients.Desafio_API;
using WebApp_Desafio_FrontEnd.ViewModels;
using WebApp_Desafio_FrontEnd.ViewModels.Enums;
using AspNetCore.Reporting;

namespace WebApp_Desafio_FrontEnd.Controllers
{
    public class ChamadosController : Controller
    {
        private readonly IHostingEnvironment _hostEnvironment;

        public ChamadosController(IHostingEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Datatable()
        {
            try
            {
                var chamadosApiClient = new ChamadosApiClient();
                var lstChamados = chamadosApiClient.Listar();

                var dataTableVM = new DataTableAjaxViewModel()
                {
                    length = lstChamados.Count,
                    data = lstChamados
                };

                return Ok(dataTableVM);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var chamadoVM = new ChamadoViewModel()
            {
                DataAbertura = DateTime.Now
            };
            ViewData["Title"] = "Cadastrar Novo Chamado";

            try
            {
                var departamentosApiClient = new DepartamentosApiClient();

                ViewData["ListaDepartamentos"] = departamentosApiClient.Listar();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
            }

            return View("Cadastrar", chamadoVM);
        }

        [HttpPost]
        public IActionResult Cadastrar(ChamadoViewModel chamadoVM)
        {
            try
            {
                var chamadosApiClient = new ChamadosApiClient();
                var realizadoComSucesso = chamadosApiClient.Gravar(chamadoVM);

                if (realizadoComSucesso)
                    return Ok(new ResponseViewModel($"Chamado gravado com sucesso!", AlertTypes.success, this.RouteData.Values["controller"].ToString(), nameof(this.Listar)));
                else
                    throw new ApplicationException($"Falha ao gravar o Chamado.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpGet]
        public IActionResult Editar([FromRoute] int id)
        {
            ViewData["Title"] = "Editar Chamado";

            try
            {
                var chamadosApiClient = new ChamadosApiClient();
                var chamadoVM = chamadosApiClient.Obter(id);

                var departamentosApiClient = new DepartamentosApiClient();
                ViewData["ListaDepartamentos"] = departamentosApiClient.Listar();

                return View("Cadastrar", chamadoVM);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpDelete]
        public IActionResult Excluir([FromRoute] int id)
        {
            try
            {
                var chamadosApiClient = new ChamadosApiClient();
                var realizadoComSucesso = chamadosApiClient.Excluir(id);

                if (realizadoComSucesso)
                    return Ok(new ResponseViewModel($"Chamado {id} excluído com sucesso!", AlertTypes.success, "Chamados", nameof(Listar)));
                else
                    throw new ApplicationException($"Falha ao excluir o Chamado {id}.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpGet]
        public IActionResult Report()
        {
            string contentRootPath = _hostEnvironment.ContentRootPath;
            string path = Path.Combine(contentRootPath, "wwwroot", "reports", "rptChamados.rdlc");

            LocalReport localReport = new LocalReport(path);

            var chamadosApiClient = new ChamadosApiClient();
            var lstChamados = chamadosApiClient.Listar();

            localReport.AddDataSource("dsChamados", lstChamados);

            ReportResult reportResult = localReport.Execute(RenderType.Pdf);

            return File(reportResult.MainStream, "application/octet-stream", "rptChamados.pdf");
        }
    }
}
