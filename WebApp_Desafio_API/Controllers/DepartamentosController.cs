using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp_Desafio_API.ViewModels;
using WebApp_Desafio_BackEnd.Business;

namespace WebApp_Desafio_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentosController : Controller
    {
        private readonly DepartamentosBLL _bll = new DepartamentosBLL();

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DepartamentoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Route("Listar")]
        public IActionResult Listar()
        {
            try
            {
                var departamentos = _bll.Listar();

                var lst = from departamento in departamentos
                          select new DepartamentoResponse()
                          {
                              Id = departamento.ID,
                              Descricao = departamento.Descricao,
                          };

                return Ok(lst);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(422, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(DepartamentoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Route("Obter")]
        public IActionResult Obter([FromQuery] int idDepartamento)
        {
            try
            {
                var departamento = _bll.Obter(idDepartamento);

                var resposta = new DepartamentoResponse()
                {
                    Id = departamento.ID,
                    Descricao = departamento.Descricao,
                };

                return Ok(resposta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(422, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Route("Gravar")]
        public IActionResult Gravar([FromBody] DepartamentoRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException("Request não informado.");

                var resultado = _bll.Gravar(request.Id, request.Descricao);
                return Ok(resultado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(422, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Route("Excluir")]
        public IActionResult Excluir([FromQuery] int idDepartamento)
        {
            try
            {
                var resultado = _bll.Excluir(idDepartamento);
                return Ok(resultado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(422, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
