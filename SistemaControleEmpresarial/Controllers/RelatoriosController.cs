using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaControleEmpresarial.Data;
using SistemaControleEmpresarial.Models;

namespace SistemaControleEmpresarial.Controllers
{
    [Authorize(Roles = "Administrador, Gerente")]
    public class RelatoriosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RelatoriosController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> _userManager)
        {
            _context = context;
            this.roleManager = roleManager;
            userManager = _userManager;
        }
        public IActionResult ListaRelatorios(string relatorios, string anoInicio, string mesInicio, string anoFim, string mesFim)
        {
            SetarParametrosDeConsulta(anoInicio, anoFim, mesInicio, mesFim);
            if (relatorios == "Usuarios")
            {
                return RedirectToAction(nameof(RelatorioUsuarios));
            }
            else if (relatorios == "Chamados")
            {
                return RedirectToAction(nameof(RelatorioChamados));
            }
            else if (relatorios == "AjustesPonto")
            {
                return RedirectToAction(nameof(RelatorioAjustesPonto));
            }
            else if (relatorios == "Treinamentos")
            {
                return RedirectToAction(nameof(RelatorioTreinamentos));
            }
            else if (relatorios == "FeriadosVSJornadas")
            {
                return RedirectToAction(nameof(RelatorioFeriadosVSJornadas));
            }
            
            return View();
        }

        public IActionResult RelatorioUsuarios()
        {
            return View();
        }

        public IActionResult RelatorioChamados()
        {
            return View();
        }

        public IActionResult RelatorioAjustesPonto()
        {
            return View();
        }

        public IActionResult RelatorioTreinamentos()
        {
            return View();
        }

        public IActionResult RelatorioFeriadosVSJornadas()
        {
            return View();
        }
        
        [HttpGet]
        public JsonResult UsuariosGrafico()
        {
            DateTime hoje = DateTime.Now;
            var usuarios = userManager.Users;

            var roleAdmin = roleManager.Roles.Where(r1 => r1.Name == "Administrador").FirstOrDefault();
            var roleGerente = roleManager.Roles.Where(r2 => r2.Name == "Gerente").FirstOrDefault();
            var roleSupervisor = roleManager.Roles.Where(r3 => r3.Name == "Supervisor").FirstOrDefault();
            var roleAnalista = roleManager.Roles.Where(r4 => r4.Name == "Analista").FirstOrDefault();

            var userRoleAdmin = _context.UserRoles.Where(u => u.RoleId == roleAdmin.Id);
            var userRoleGerente = _context.UserRoles.Where(u => u.RoleId == roleGerente.Id);
            var userRoleSupervisor = _context.UserRoles.Where(u => u.RoleId == roleSupervisor.Id);
            var userRoleAnalista = _context.UserRoles.Where(u => u.RoleId == roleAnalista.Id);

            int contaAdmin = (userRoleAdmin != null) ? userRoleAdmin.Count() : 0;
            int contaGerente = (userRoleGerente != null) ? userRoleGerente.Count() : 0; ;
            int contaSupervisor = (userRoleSupervisor != null) ? userRoleSupervisor.Count() : 0; ;
            int contaAnalista = (userRoleAnalista != null) ? userRoleAnalista.Count() : 0; ;
            int contaSemPerfil = usuarios.Count() - (contaAdmin + contaGerente + contaSupervisor + contaAnalista);

            /*
            foreach (ApplicationUser user in usuarios)
            {
                var userRole = _context.UserRoles.Where(u => u.UserId == user.Id).FirstOrDefault();
                if (userRole != null)
                {
                    if (userRole.RoleId == roleAdmin.Id)
                    {
                        contaAdmin++;
                    }
                    if (userRole.RoleId == roleGerente.Id)
                    {
                        contaGerente++;
                    }
                    if (userRole.RoleId == roleSupervisor.Id)
                    {
                        contaSupervisor++;
                    }
                    if (userRole.RoleId == roleAnalista.Id)
                    {
                        contaAnalista++;
                    }
                }                
                else
                {
                    contaSemPerfil++;
                }
            }
            */
            var lista = new List<UsuariosRolesGrafico>();
            lista.Add(new UsuariosRolesGrafico { Perfil = "Administrador", Total = contaAdmin });
            lista.Add(new UsuariosRolesGrafico { Perfil = "Gerente", Total = contaGerente });
            lista.Add(new UsuariosRolesGrafico { Perfil = "Supervisor", Total = contaSupervisor });
            lista.Add(new UsuariosRolesGrafico { Perfil = "Analista", Total = contaAnalista });
            lista.Add(new UsuariosRolesGrafico { Perfil = "Sem Perfil", Total = contaSemPerfil });
            return Json(lista);
        }

        [HttpGet]
        public JsonResult ChamadosGrafico()
        {
            string sAnoInicio, sAnoFim, sMesInicio, sMesFim;
            RecuperarParametrosConsulta(out sAnoInicio, out sAnoFim, out sMesInicio, out sMesFim);

            int anoInicio;
            int mesInicio;
            int anoFim;
            int mesFim;
            try
            {
                anoInicio = int.Parse(sAnoInicio);
                mesInicio = int.Parse(sMesInicio);
                anoFim = int.Parse(sAnoFim);
                mesFim = int.Parse(sMesFim);
            }
            catch(Exception)
            {
                TempData["msgErroRelatorios"] = "<script>alert('Operação Inválida!');</script>";
                return Json(string.Empty);
            }

            var chamados = from p in _context.Chamado
                           select p;
            chamados = chamados.Where(u => u.DataCriacao.Year >= anoInicio);
            chamados = chamados.Where(u => u.DataCriacao.Year <= anoFim);
            chamados = chamados.Where(u => u.DataCriacao.Month >= mesInicio);
            chamados = chamados.Where(u => u.DataCriacao.Month <= mesFim);

            int qtdeAnos = (anoInicio == anoFim) ? 1 : anoFim - anoInicio;
            int qtdeMeses = (mesInicio == mesFim) ? 1 : mesFim - mesInicio;

            var lista = new List<ChamadosGrafico>();
            for (int i = anoInicio; i <= anoFim; i++)
            {
                for (int j = mesInicio; j <= mesFim; j++)
                {
                    string anoMes = i.ToString() + "/" + DescricaoMes(j);
                    var totalChamadosAbertos = chamados.Where(u => u.DataCriacao.Year == i && u.DataCriacao.Month == j && u.SituacaoChamado == Dominio.SituacaoChamado.Aberto).Count();
                    var totalChamadosFechados = chamados.Where(u => u.DataCriacao.Year == i && u.DataCriacao.Month == j && u.SituacaoChamado == Dominio.SituacaoChamado.Fechado).Count();
                    lista.Add(new ChamadosGrafico { AnoMes = anoMes, TotalAberto = totalChamadosAbertos, TotalFechado = totalChamadosFechados });
                }
            }
            
            return Json(lista);
        }

        [HttpGet]
        public JsonResult AjustesPontoGrafico()
        {
            string sAnoInicio, sAnoFim, sMesInicio, sMesFim;
            RecuperarParametrosConsulta(out sAnoInicio, out sAnoFim, out sMesInicio, out sMesFim);

            int anoInicio;
            int mesInicio;
            int anoFim;
            int mesFim;
            try
            {
                anoInicio = int.Parse(sAnoInicio);
                mesInicio = int.Parse(sMesInicio);
                anoFim = int.Parse(sAnoFim);
                mesFim = int.Parse(sMesFim);
            }
            catch (Exception)
            {
                TempData["msgErroRelatorios"] = "<script>alert('Operação Inválida!');</script>";
                return Json(string.Empty);
            }

            var ajustesPonto = from p in _context.AjustePontoEletronico
                           select p;
            
            var lista = new List<AjustePontoEletronicoGrafico>();
            for (int i = anoInicio; i <= anoFim; i++)
            {
                for (int j = mesInicio; j <= mesFim; j++)
                {
                    string anoMes = i.ToString() + "/" + DescricaoMes(j);
                    var totalAjustesPontoSolicitados = ajustesPonto.Where(u => u.DataAjuste.Year == i && u.DataAjuste.Month == j).Count();
                    var totalAjustesPontoAprovados = ajustesPonto.Where(u => u.DataAjuste.Year == i && u.DataAjuste.Month == j && u.SituacaoAjuste == Dominio.SituacaoAjustePonto.Aprovada).Count();
                    var totalAjustesPontoReprovados = ajustesPonto.Where(u => u.DataAjuste.Year == i && u.DataAjuste.Month == j && u.SituacaoAjuste == Dominio.SituacaoAjustePonto.Reprovada).Count();
                    var totalAjustesPendentes = ajustesPonto.Where(u => u.DataAjuste.Year == i && u.DataAjuste.Month == j && u.SituacaoAjuste == Dominio.SituacaoAjustePonto.Pendente).Count();
                    lista.Add(new AjustePontoEletronicoGrafico { AnoMes = anoMes, TotalSolicitado = totalAjustesPontoSolicitados, TotalAprovado = totalAjustesPontoAprovados, TotalReprovado = totalAjustesPontoReprovados, TotalPendente = totalAjustesPendentes });
                }
            }

            return Json(lista);
        }

        [HttpGet]
        public JsonResult TreinamentosGrafico()
        {
            string sAnoInicio, sAnoFim, sMesInicio, sMesFim;
            RecuperarParametrosConsulta(out sAnoInicio, out sAnoFim, out sMesInicio, out sMesFim);

            int anoInicio;
            int mesInicio;
            int anoFim;
            int mesFim;
            try
            {
                anoInicio = int.Parse(sAnoInicio);
                mesInicio = int.Parse(sMesInicio);
                anoFim = int.Parse(sAnoFim);
                mesFim = int.Parse(sMesFim);
            }
            catch (Exception)
            {
                TempData["msgErroRelatorios"] = "<script>alert('Operação Inválida!');</script>";
                return Json(string.Empty);
            }

            var treinamentos = from p in _context.Treinamento
                           select p;
            
            var lista = new List<TreinamentosGrafico>();
            for (int i = anoInicio; i <= anoFim; i++)
            {
                for (int j = mesInicio; j <= mesFim; j++)
                {
                    string anoMes = i.ToString() + "/" + DescricaoMes(j);
                    var totalTreinamentos = treinamentos.Where(u => u.DataCriacao.Year == i && u.DataCriacao.Month == j).Count();
                    lista.Add(new TreinamentosGrafico { AnoMes = anoMes, Total = totalTreinamentos });
                }
            }

            return Json(lista);
        }

        [HttpGet]
        public JsonResult FeriadoVSJornadaGrafico()
        {
            string sAnoInicio, sAnoFim, sMesInicio, sMesFim;
            RecuperarParametrosConsulta(out sAnoInicio, out sAnoFim, out sMesInicio, out sMesFim);

            int anoInicio;
            int mesInicio;
            int anoFim;
            int mesFim;
            try
            {
                anoInicio = int.Parse(sAnoInicio);
                mesInicio = int.Parse(sMesInicio);
                anoFim = int.Parse(sAnoFim);
                mesFim = int.Parse(sMesFim);
            }
            catch (Exception)
            {
                TempData["msgErroRelatorios"] = "<script>alert('Operação Inválida!');</script>";
                return Json(string.Empty);
            }

            var feriados = from p in _context.Feriado
                               select p;

            var solicitacoesJornada = from s in _context.SolicitacaoJornada
                           select s;

            var lista = new List<FeriadosGrafico>();
            for (int i = anoInicio; i <= anoFim; i++)
            {
                for (int j = mesInicio; j <= mesFim; j++)
                {
                    string anoMes = i.ToString() + "/" + DescricaoMes(j);
                    var totalFeriados = feriados.Where(u => u.Data.Year == i && u.Data.Month == j).Count();

                    var listaFeriados = feriados.Where(u => u.Data.Year == i && u.Data.Month == j);
                    var totalJornadas = 0;
                    foreach (Feriado feriado in listaFeriados)
                    {
                        int totalJornadasNesteFeriado = solicitacoesJornada.Where(u => u.CodigoFeriado == feriado.CodigoFeriado && u.SituacaoSolicitacao == Dominio.SituacaoSolicitacaoJornada.Aprovada).Count();
                            totalJornadas = totalJornadas + totalJornadasNesteFeriado;
                    }

                    lista.Add(new FeriadosGrafico { AnoMes = anoMes, TotalFeriados = totalFeriados, TotalJornadas = totalJornadas });
                }
            }

            return Json(lista);
        }

        private void SetarParametrosDeConsulta(string anoInicio, string anoFim, string mesInicio, string mesFim)
        {
            HttpContext.Session.SetString("paramAnoInicio", (anoInicio != null) ? anoInicio : string.Empty);
            HttpContext.Session.SetString("paramMesInicio", (mesInicio != null) ? mesInicio : string.Empty);
            HttpContext.Session.SetString("paramAnoFim", (anoFim != null) ? anoFim : string.Empty);
            HttpContext.Session.SetString("paramMesFim", (mesFim != null) ? mesFim : string.Empty);
        }

        private void RecuperarParametrosConsulta(out string anoInicio, out string anoFim, out string mesInicio, out string mesFim)
        {
            anoInicio = HttpContext.Session.GetString("paramAnoInicio");
            mesInicio = HttpContext.Session.GetString("paramMesInicio");
            anoFim = HttpContext.Session.GetString("paramAnoFim");
            mesFim = HttpContext.Session.GetString("paramMesFim");
        }

        public string DescricaoMes (int mes)
        {
            string descricaoMes = string.Empty;
            switch(mes)
            {
                case 1:
                    descricaoMes = "JAN";
                    break;
                case 2:
                    descricaoMes = "FEV";
                    break;
                case 3:
                    descricaoMes = "MAR";
                    break;
                case 4:
                    descricaoMes = "ABR";
                    break;
                case 5:
                    descricaoMes = "MAI";
                    break;
                case 6:
                    descricaoMes = "JUN";
                    break;
                case 7:
                    descricaoMes = "JUL";
                    break;
                case 8:
                    descricaoMes = "AGO";
                    break;
                case 9:
                    descricaoMes = "SET";
                    break;
                case 10:
                    descricaoMes = "OUT";
                    break;
                case 11:
                    descricaoMes = "NOV";
                    break;
                case 12:
                    descricaoMes = "DEZ";
                    break;
                default:
                    descricaoMes = string.Empty;
                    break;
            }
            return descricaoMes;
        }
    }
}