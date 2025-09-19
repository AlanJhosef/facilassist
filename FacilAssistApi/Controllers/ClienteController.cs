using FacilAssistApi.Command;
using FacilAssistApi.Enums;
using FacilAssistApi.Ports;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FacilAssistApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Retorna a lista de todos os clientes
        /// </summary>
        /// <returns>Uma lista de Clientes.</returns>
        /// <response code="200">Retorna a lista de Clientes com sucesso.</response>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var clientes = await _clienteService.ObterClientes();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Retorna a lista de todos os clientes
        /// </summary>
        /// <returns>Uma lista de Clientes.</returns>
        /// <response code="200">Retorna a lista de Clientes com sucesso.</response>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> DetalhesCliente(int id)
        {
            try
            {
                var cliente = await _clienteService.ObterDetalhesCliente(id);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        /// <summary>
        /// Criar Cliente
        /// </summary>
        /// <returns>Nao retornada nada, apenas executa</returns>
        /// <response code="200">OK</response>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Criar([FromBody] ClienteCommand command, [FromServices] IValidator<ClienteCommand> validator)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(command);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                await _clienteService.Criar(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex .Message});
            }
        }


        /// <summary>
        /// ExAlterarcluir Cliente
        /// </summary>
        /// <returns>Nao retornada nada, apenas executa</returns>
        /// <response code="200">OK</response>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Alterar([FromBody] ClienteCommand command, [FromServices] IValidator<ClienteCommand> validator)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(command);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                await _clienteService.Alterar(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <returns>Nao retornada nada, apenas executa</returns>
        /// <response code="200">OK</response>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Excluir([FromRoute] int id)
        {
            try
            {                
                await _clienteService.Excluir(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>
        /// Aprovar/Reprovar Cliente
        /// </summary>
        /// <returns>Nao retornada nada, apenas executa</returns>
        /// <response code="200">OK</response>
        [HttpPut("aprovar-reprovar/{id}/{status}")]
        [Authorize]
        public async Task<IActionResult> AprovarReprovar([FromRoute] int id, [FromRoute] ESituacaoCliente status)
        {
            try
            {
                await _clienteService.AprovarReprovarCliente(id, status);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
