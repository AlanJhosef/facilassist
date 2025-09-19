using FacilAssist.Front.Models;
using FacilAssist.Front.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FacilAssist.Front.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClienteService _clienteService;

        public HomeController()
        {
            _clienteService = new ClienteService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Listar()
        {
            var model = await _clienteService.ObterClientesAsync();
            return View(model);
        }

        public async Task<JsonResult> ObterDadosCliente(int id)
        {
            var model = await _clienteService.DetalhesClientesAsync(id);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _partialEditarClientes()
        {
            return View();
        }
        public async Task<JsonResult> Criar(ClienteCommand command)
        {
            var retorno = new { msg = "", tipo = "error" };
            try
            {
                await _clienteService.CriarClientesAsync(command);                
                retorno = new { msg = "Cliente cadastrado com sucesso", tipo = "success" };
                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno = new { msg = ex.Message, tipo = "error" };
                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
        }
        
        public async Task<ActionResult> Alterar(ClienteCommand command)
        {
            var retorno = new { msg = "", tipo = "error" };
            try
            {
                await _clienteService.AlterarClientesAsync(command);
                retorno = new { msg = "Cliente alterado com sucesso", tipo = "success" };
                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno = new { msg = ex.Message, tipo = "error" };
                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> Excluir(int id)
        {
            var retorno = new { msg = "", tipo = "error" };
            try
            {
                await _clienteService.ExcluirClientesAsync(id);

                retorno = new { msg = "Cliente excluído com sucesso", tipo = "success" };

                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno = new { msg = ex.Message, tipo = "error" };
                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> Aprovar(int id)
        {
            var retorno = new { msg = "", tipo = "error" };
            try
            {
                await _clienteService.AprovarReprovarClientesAsync(id, Enum.EStatusCliente.Aprovado);

                retorno = new { msg = "Cliente Aprovado com sucesso", tipo = "success" };

                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno = new { msg = ex.Message, tipo = "error" };
                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> Reprovar(int id)
        {
            var retorno = new { msg = "", tipo = "error" };
            try
            {
                await _clienteService.AprovarReprovarClientesAsync(id, Enum.EStatusCliente.Reprovado);

                retorno = new { msg = "Cliente Reprovado com sucesso", tipo = "success" };

                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno = new { msg = ex.Message, tipo = "error" };
                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
        }
    }
}