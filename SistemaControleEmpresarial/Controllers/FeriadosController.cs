using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaControleEmpresarial.Data;
using Microsoft.AspNetCore.Identity;
using SistemaControleEmpresarial.Models;
using Microsoft.AspNetCore.Http;
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace SistemaControleEmpresarial.Controllers
{
    [Authorize(Roles = "Administrador, Gerente, Supervisor, Analista")]
    public class FeriadosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public FeriadosController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> _userManager)
        {
            _context = context;
            this.roleManager = roleManager;
            userManager = _userManager;
        }

        // GET: Feriados
        public async Task<IActionResult> ListaFeriados(DateTime? dataFeriadoInicio,
                                                       DateTime? dataFeriadoFim,
                                                       string descricaoFeriado)
        {
            SetarParametrosDeConsulta(dataFeriadoInicio, dataFeriadoFim, descricaoFeriado);

            var feriados = from f in _context.Feriado.Include(f => f.ApplicationUser)
                           select f;

            if (dataFeriadoInicio != null)
            {
                feriados = feriados.Where(u => u.Data >= dataFeriadoInicio);
            }
            if (dataFeriadoFim != null)
            {
                feriados = feriados.Where(u => u.Data <= dataFeriadoFim);
            }
            if (!string.IsNullOrEmpty(descricaoFeriado))
            {
                feriados = feriados.Where(u => u.DescricaoFeriado.Contains(descricaoFeriado));
            }

            if (dataFeriadoInicio == null && dataFeriadoFim == null && string.IsNullOrEmpty(descricaoFeriado))
            {
                feriados = feriados.Where(u => u.Data >= DateTime.Now.Date);
            }

            feriados = feriados.OrderBy(u => u.Data);

            return View(await feriados.AsNoTracking().ToListAsync());
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // GET: Feriados/CadastrarFeriado
        public IActionResult CadastrarFeriado()
        {
            return View();
        }

        // POST: Feriados/CadastrarFeriado
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador, Gerente")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastrarFeriado([Bind("CodigoFeriado,DescricaoFeriado,Data")] Feriado feriado)
        {
            string codigoUsuario = userManager.GetUserId(User);
            DateTime dataHoraCriacao = DateTime.Now;
            DateTime hoje = DateTime.Now.Date;

            if (feriado.Data <= hoje)
            {
                TempData["msgCriacaoFeriado"] = "<script>alert('O feriado deve ser cadastrado para uma data futura.');</script>";
            }
            else
            {
                var feriadoExistente = await _context.Feriado
                .FirstOrDefaultAsync(m => m.Data.Date == feriado.Data.Date);
                if (feriadoExistente == null)
                {
                    feriado.UserId = codigoUsuario;
                    feriado.DataCriacao = dataHoraCriacao;
                    _context.Add(feriado);
                    await _context.SaveChangesAsync();

                    int codigoLogAlteracao = LogarAlteracoes(feriado.CodigoFeriado, Dominio.TipoOperacaoLog.Inclusao, codigoUsuario);
                    CriarLogAlteracaoItem(codigoLogAlteracao, "DescricaoFeriado", string.Empty, feriado.DescricaoFeriado);
                    CriarLogAlteracaoItem(codigoLogAlteracao, "Data", string.Empty, feriado.Data.ToString());
                    CriarLogAlteracaoItem(codigoLogAlteracao, "UsuarioCriacao", string.Empty, codigoUsuario.ToString());
                    CriarLogAlteracaoItem(codigoLogAlteracao, "DataHoraCriacao", string.Empty, dataHoraCriacao.ToString());
                    _context.SaveChanges();

                    TempData["msgCriacaoFeriado"] = "<script>alert('Feriado Cadastrado Com Sucesso!');</script>";
                    return RedirectToAction(nameof(ListaFeriados));
                }
                else
                {
                    TempData["msgCriacaoFeriado"] = "<script>alert('Já existe um feriado cadastrado para esta data!');</script>";
                }
            }
            return View();
        }

        [Authorize(Roles = "Administrador, Gerente")]
        public async Task<IActionResult> EditarFeriado(int? id)
        {
            var feriado = await _context.Feriado.FindAsync(id);
            if (feriado == null)
            {
                return NotFound();
            }
            return View(feriado);
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // POST: Feriados/EditarFeriado
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarFeriado(int id, [Bind("CodigoFeriado,DescricaoFeriado,Data")] Feriado feriado)
        {
            string codigoUsuario = userManager.GetUserId(User);
            if (id != feriado.CodigoFeriado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DateTime dataHoraAtual = DateTime.Now;
                    var feriadoAtualizacao = await _context.Feriado
                    .FirstOrDefaultAsync(m => m.CodigoFeriado == id);

                    string descricaoAntiga = feriadoAtualizacao.DescricaoFeriado;

                    if (feriado.Data != feriadoAtualizacao.Data)
                    {
                        TempData["msgCriacaoFeriado"] = "<script>alert('A data deste feriado não pode ser alterada.');</script>";
                        return View(feriadoAtualizacao);
                    }

                    var usuarioAtualizacao = _context.Users.FirstOrDefault(u => u.Id == codigoUsuario);

                    feriadoAtualizacao.DescricaoFeriado = feriado.DescricaoFeriado;
                    feriadoAtualizacao.CodigoUsuarioUltimaAtualizacao = usuarioAtualizacao.CodigoExterno.ToString();
                    feriadoAtualizacao.DataHoraUltimaAlteracao = dataHoraAtual;
                    feriadoAtualizacao.NomeUsuarioUltimaAtualizacao = usuarioAtualizacao.NomeUsuario;

                    int codigoLogAlteracao = LogarAlteracoes(feriadoAtualizacao.CodigoFeriado, Dominio.TipoOperacaoLog.Edicao, codigoUsuario);
                    CriarLogAlteracaoItem(codigoLogAlteracao, "DescricaoFeriado", descricaoAntiga, feriado.DescricaoFeriado);
                    _context.Update(feriadoAtualizacao);

                    await _context.SaveChangesAsync();
                    TempData["msgCriacaoFeriado"] = "<script>alert('Feriado Alterado Com Sucesso!');</script>";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeriadoExists(feriado.CodigoFeriado))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListaFeriados));
            }
            return View(feriado);
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // GET: Feriados/ExcluirFeriado/5
        public async Task<IActionResult> ExcluirFeriado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feriado = await _context.Feriado
                .Include(f => f.ApplicationUser)
                .FirstOrDefaultAsync(m => m.CodigoFeriado == id);
            if (feriado == null)
            {
                return NotFound();
            }

            return View(feriado);
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // POST: Feriados/ExcluirFeriado/5
        [HttpPost, ActionName("ExcluirFeriado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string codigoUsuario = userManager.GetUserId(User);

            var feriado = await _context.Feriado.FindAsync(id);

            DateTime dataAtual = DateTime.Now.Date;
            if (feriado.Data <= dataAtual)
            {
                TempData["msgExclusaoFeriado"] = "<script>alert('Operação de exclusão disponível apenas para feriados FUTUROS.');</script>";
                return View(feriado);
            }

            _context.Feriado.Remove(feriado);
            await _context.SaveChangesAsync();

            int codigoLogAlteracao = LogarAlteracoes(id, Dominio.TipoOperacaoLog.Exclusao, codigoUsuario);

            TempData["msgExclusaoFeriado"] = "<script>alert('Feriado Excluído Com Sucesso!');</script>";
            return RedirectToAction(nameof(ListaFeriados));
        }

        private bool FeriadoExists(int id)
        {
            return _context.Feriado.Any(e => e.CodigoFeriado == id);
        }

        public int LogarAlteracoes(int codigoFeriado, string tipoOperacao, string codigoUsuario)
        {
            LogAlteracao logAlteracao = new LogAlteracao();
            logAlteracao.DataCriacao = DateTime.Now;
            logAlteracao.TipoOperacaoLog = tipoOperacao;
            logAlteracao.UserId = codigoUsuario;
            logAlteracao.Entidade = "Feriado";
            logAlteracao.CodigoEntidade = codigoFeriado.ToString();
            _context.Add(logAlteracao);
            _context.SaveChanges();
            return logAlteracao.CodigoLogAlteracao;
        }

        private void CriarLogAlteracaoItem(int codigoLogAlteracao, string objeto, string valorAntigo, string valorNovo)
        {
            LogAlteracaoItem logAlteracaoItem = new LogAlteracaoItem();
            logAlteracaoItem.CodigoLogAlteracao = codigoLogAlteracao;
            logAlteracaoItem.Objeto = objeto;
            logAlteracaoItem.ValorAntigo = valorAntigo;
            logAlteracaoItem.ValorNovo = valorNovo;
            _context.Add(logAlteracaoItem);
        }

        private void SetarParametrosDeConsulta(DateTime ? dataFeriadoInicio, DateTime ? dataFeriadoFim, string descricaoFeriado)
        {
            if (dataFeriadoInicio != null)
            {
                HttpContext.Session.SetString("paramDataFeriadoInicio", dataFeriadoInicio.ToString());
            }
            if (dataFeriadoFim != null)
            {
                HttpContext.Session.SetString("paramDataFeriadoFim", dataFeriadoFim.ToString());
            }
            if (!string.IsNullOrEmpty(descricaoFeriado))
            {
                HttpContext.Session.SetString("paramDescricaoFeriado", descricaoFeriado);
            }
        }

        private void RecuperarParametrosConsulta(out string dataFeriadoInicio, out string dataFeriadoFim, out string descricaoFeriado)
        {
            dataFeriadoInicio = HttpContext.Session.GetString("paramDataFeriadoInicio");
            dataFeriadoFim = HttpContext.Session.GetString("paramDataFeriadoFim");
            descricaoFeriado = HttpContext.Session.GetString("paramDescricaoFeriado");
        }

        public IActionResult ExportarExcel()
        {
            string sdataFeriadoInicio; 
            string sdataFeriadoFim;
            string descricaoFeriado;
            RecuperarParametrosConsulta(out sdataFeriadoInicio, out sdataFeriadoFim, out descricaoFeriado);

            var feriados = from f in _context.Feriado.Include(f => f.ApplicationUser)
                           select f;

            if (!string.IsNullOrEmpty(sdataFeriadoInicio))
            {
                feriados = feriados.Where(u => u.Data >= DateTime.Parse(sdataFeriadoInicio));
            }
            if (!string.IsNullOrEmpty(sdataFeriadoFim))
            {
                feriados = feriados.Where(u => u.Data <= DateTime.Parse(sdataFeriadoFim));
            }
            if (!string.IsNullOrEmpty(descricaoFeriado))
            {
                feriados = feriados.Where(u => u.DescricaoFeriado.Contains(descricaoFeriado));
            }

            if (string.IsNullOrEmpty(sdataFeriadoInicio) && string.IsNullOrEmpty(sdataFeriadoFim) && string.IsNullOrEmpty(descricaoFeriado))
            {
                feriados = feriados.Where(u => u.Data >= DateTime.Now.Date);
            }
            feriados = feriados.OrderBy(u => u.Data);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Feriados");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "CodigoFeriado";
                worksheet.Cell(currentRow, 2).Value = "Data";
                worksheet.Cell(currentRow, 3).Value = "Feriado";
                worksheet.Cell(currentRow, 4).Value = "UsuarioCriacao";
                worksheet.Cell(currentRow, 5).Value = "DataCriacao";

                foreach (var feriado in feriados)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = feriado.CodigoFeriado;
                    worksheet.Cell(currentRow, 2).Value = feriado.Data;
                    worksheet.Cell(currentRow, 3).Value = feriado.DescricaoFeriado;
                    worksheet.Cell(currentRow, 4).Value = feriado.ApplicationUser.CodigoExterno + " - " + feriado.ApplicationUser.NomeUsuario;
                    worksheet.Cell(currentRow, 5).Value = feriado.DataCriacao;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Feriados.xlsx");
                }
            }
        }

        public IActionResult ExportarExcelJornadas()
        {
            string codigoUsuarioLogado = userManager.GetUserId(User);

            var solicitacoesJornadas = from f in _context.SolicitacaoJornada.Include(f => f.ApplicationUser).Include(f => f.Feriado)
                                       select f;
            solicitacoesJornadas = solicitacoesJornadas.Where(s => s.UserId == codigoUsuarioLogado);
            
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("SolicitacoesJornadas");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Feriado";
                worksheet.Cell(currentRow, 2).Value = "UsuarioJornada";
                worksheet.Cell(currentRow, 3).Value = "UsuarioSolicitante";
                worksheet.Cell(currentRow, 4).Value = "DataCriacao";
                worksheet.Cell(currentRow, 5).Value = "Justificativa";
                worksheet.Cell(currentRow, 6).Value = "SituacaoSolicitacao";
                worksheet.Cell(currentRow, 7).Value = "Observacoes";

                foreach (var solicitacaoJornada in solicitacoesJornadas)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = solicitacaoJornada.Feriado.CodigoFeriado + " - " + solicitacaoJornada.Feriado.DescricaoFeriado;
                    worksheet.Cell(currentRow, 2).Value = solicitacaoJornada.CodigoUsuarioJornada + " - " + solicitacaoJornada.NomeUsuarioJornada;
                    worksheet.Cell(currentRow, 3).Value = solicitacaoJornada.ApplicationUser.CodigoExterno + " - " + solicitacaoJornada.ApplicationUser.NomeUsuario;
                    worksheet.Cell(currentRow, 4).Value = solicitacaoJornada.DataCriacao;
                    worksheet.Cell(currentRow, 5).Value = solicitacaoJornada.Justificativa;
                    worksheet.Cell(currentRow, 6).Value = solicitacaoJornada.SituacaoSolicitacao;
                    worksheet.Cell(currentRow, 7).Value = solicitacaoJornada.Observacoes;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "SolicitacoesJornadas.xlsx");
                }
            }
        }

        public IActionResult ExportarExcelJornadasPendentes()
        {
            string codigoUsuarioLogado = userManager.GetUserId(User);

            var solicitacoesJornadas = from p in _context.SolicitacaoJornada.Include(p => p.ApplicationUser).Include(p => p.Feriado)
                             .Where(u => u.SituacaoSolicitacao == Dominio.SituacaoSolicitacaoJornada.Pendente)
                                       select p;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("SolicitacoesJornadas");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Solicitante";
                worksheet.Cell(currentRow, 2).Value = "DataCriacao";
                worksheet.Cell(currentRow, 3).Value = "Feriado";
                worksheet.Cell(currentRow, 4).Value = "UsuarioJornada";
                worksheet.Cell(currentRow, 5).Value = "Justificativa";

                foreach (var solicitacaoJornada in solicitacoesJornadas)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = solicitacaoJornada.ApplicationUser.CodigoExterno + " - " + solicitacaoJornada.ApplicationUser.NomeUsuario;
                    worksheet.Cell(currentRow, 2).Value = solicitacaoJornada.DataCriacao;
                    worksheet.Cell(currentRow, 3).Value = solicitacaoJornada.Feriado.CodigoFeriado + " - " + solicitacaoJornada.Feriado.DescricaoFeriado;
                    worksheet.Cell(currentRow, 4).Value = solicitacaoJornada.CodigoUsuarioJornada + " - " + solicitacaoJornada.NomeUsuarioJornada;
                    worksheet.Cell(currentRow, 5).Value = solicitacaoJornada.Justificativa;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "SolicitacoesJornadaPendentes.xlsx");
                }
            }
        }

        // GET: Feriados/CadastrarFeriado
        public async Task<IActionResult> FeriadoLookup(DateTime? dataFeriadoInicio,
                                                       DateTime? dataFeriadoFim,
                                                       string descricaoFeriado)
        {
            var feriados = from f in _context.Feriado.Include(f => f.ApplicationUser)
                           select f;

            if (dataFeriadoInicio != null)
            {
                feriados = feriados.Where(u => u.Data >= dataFeriadoInicio);
            }
            if (dataFeriadoFim != null)
            {
                feriados = feriados.Where(u => u.Data <= dataFeriadoFim);
            }
            if (!string.IsNullOrEmpty(descricaoFeriado))
            {
                feriados = feriados.Where(u => u.DescricaoFeriado.Contains(descricaoFeriado));
            }

            feriados = feriados.Where(u => u.Data >= DateTime.Now.Date);
            feriados = feriados.OrderBy(u => u.Data);

            return View(await feriados.AsNoTracking().ToListAsync());
        }


        [Authorize(Roles = "Administrador, Gerente, Supervisor")]
        // GET: Feriados
        public IActionResult SolicitarJornada()
        {
            return View();
        }

        [Authorize(Roles = "Administrador, Gerente, Supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SolicitarJornada([Bind("CodigoFeriado,CodigoUsuarioJornada,Justificativa")] SolicitacaoJornada solicitacaoJornada)
        {
            string codigoUsuario = userManager.GetUserId(User);
            DateTime dataHoraCriacao = DateTime.Now;
            DateTime hoje = DateTime.Now.Date;

            var solicitacaoExistente = await _context.SolicitacaoJornada
                .FirstOrDefaultAsync(s => s.CodigoFeriado == solicitacaoJornada.CodigoFeriado
                                     && s.CodigoUsuarioJornada == solicitacaoJornada.CodigoUsuarioJornada
                                     && s.SituacaoSolicitacao != Dominio.SituacaoSolicitacaoJornada.Recusada);
            if (solicitacaoExistente != null)
            {
                TempData["msgCriacaoJornada"] = "<script>alert('Já existe uma solicitação pendente/aprovada para este usuário neste feriado.');</script>";
                return View();
            }
            else
            {
                var feriadoJornada = _context.Feriado.FirstOrDefault(u => u.CodigoFeriado == solicitacaoJornada.CodigoFeriado);

                if (feriadoJornada == null)
                {
                    TempData["msgCriacaoJornada"] = "<script>alert('Feriado Inválido.');</script>";
                    return View();
                }

                var usuarioJornada = _context.Users.FirstOrDefault(u => u.CodigoExterno == solicitacaoJornada.CodigoUsuarioJornada);

                if (usuarioJornada == null)
                {
                    TempData["msgCriacaoJornada"] = "<script>alert('Usuário Inválido.');</script>";
                    return View();
                }

                if (feriadoJornada.Data <= DateTime.Now.Date)
                {
                    TempData["msgCriacaoJornada"] = "<script>alert('Jornadas devem ser cadastradas para uma data futura.');</script>";
                    return View();
                }

                solicitacaoJornada.DataCriacao = dataHoraCriacao;
                solicitacaoJornada.UserId = codigoUsuario;
                solicitacaoJornada.NomeUsuarioJornada = usuarioJornada.NomeUsuario;
                solicitacaoJornada.SituacaoSolicitacao = Dominio.SituacaoSolicitacaoJornada.Pendente;
                solicitacaoJornada.Observacoes = "";
                _context.Add(solicitacaoJornada);
                await _context.SaveChangesAsync();

                TempData["msgCriacaoJornada"] = "<script>alert('Solicitação de Jornada registrada com sucesso. Aguarde aprovação!');</script>";

                return RedirectToAction(nameof(ListaJornadas));
            }
        }

        [Authorize(Roles = "Administrador, Gerente, Supervisor")]
        // GET: Feriados
        public async Task<IActionResult> ListaJornadas(int ? codigoUsuario,
                                                       int ? codigoFeriado)
        {
            string codigoUsuarioLogado = userManager.GetUserId(User);

            var solicitacoesJornadas = from f in _context.SolicitacaoJornada.Include(f => f.ApplicationUser).Include(f => f.Feriado)
                                       select f;
            solicitacoesJornadas = solicitacoesJornadas.Where(s => s.UserId == codigoUsuarioLogado);

            if (codigoUsuario != null)
            {
                solicitacoesJornadas = solicitacoesJornadas.Where(s => s.CodigoUsuarioJornada == codigoUsuario);
            }
            if (codigoFeriado != null)
            {
                solicitacoesJornadas = solicitacoesJornadas.Where(s => s.CodigoFeriado == codigoFeriado);
            }
            
            return View(await solicitacoesJornadas.AsNoTracking().ToListAsync());
        }

        [Authorize(Roles = "Administrador, Gerente, Supervisor")]
        // GET: Feriados/ExcluirFeriado/5
        public async Task<IActionResult> ExcluirJornada(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jornada = await _context.SolicitacaoJornada
                .Include(f => f.ApplicationUser)
                .FirstOrDefaultAsync(m => m.CodigoSolicitacaoJornada == id);
            if (jornada == null)
            {
                return NotFound();
            }

            return View(jornada);
        }

        [Authorize(Roles = "Administrador, Gerente, Supervisor")]
        // POST: Feriados/ExcluirJornada/5
        [HttpPost, ActionName("ExcluirJornada")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedJornada(int id)
        {
            string codigoUsuario = userManager.GetUserId(User);

            var jornada = await _context.SolicitacaoJornada.FindAsync(id);

            if (jornada.UserId != codigoUsuario)
            {
                TempData["msgExclusaoJornada"] = "<script>alert('Operação não permitida.');</script>";
                return RedirectToAction(nameof(ListaJornadas));
            }

            var feriado = await _context.Feriado.FindAsync(jornada.CodigoFeriado);

            DateTime dataAtual = DateTime.Now.Date;
            if (feriado.Data <= dataAtual)
            {
                TempData["msgExclusaoJornada"] = "<script>alert('Operação de exclusão disponível apenas para jornadas FUTURAS.');</script>";
                return RedirectToAction(nameof(ListaJornadas));
            }

            _context.SolicitacaoJornada.Remove(jornada);
            await _context.SaveChangesAsync();

            TempData["msgExclusaoJornada"] = "<script>alert('Solicitação Jornada Excluída Com Sucesso!');</script>";
            return RedirectToAction(nameof(ListaJornadas));
        }


        public JsonResult GetDados(int id)
        {
            var sa = new JsonSerializerSettings();
            var feriados = _context.Feriado.Where(u => u.CodigoFeriado == id).FirstOrDefault();

            var resultado = new
            {
                Value = (feriados != null) ? feriados.CodigoFeriado.ToString() : null,
                Text = (feriados != null) ? feriados.DescricaoFeriado : null
            };
            return Json(resultado, sa);
        }

        public JsonResult GetDadosUsuario(int id)
        {
            var sa = new JsonSerializerSettings();
            var usuario = _context.Users.Where(u => u.CodigoExterno == id).FirstOrDefault();

            var resultado = new
            {
                Value = (usuario != null) ? usuario.CodigoExterno.ToString() : null,
                Text = (usuario != null) ? usuario.NomeUsuario : null
            };
            return Json(resultado, sa);
        }

        [HttpPost]
        public IActionResult GetData(int id)
        {
            var feriados = _context.Feriado.Where(u => u.CodigoFeriado == id).FirstOrDefault();
            return Ok(feriados.DescricaoFeriado);
        }

        public JsonResult ObterFeriadosLookup(string sDataFeriadoInicio,
                                                 string sDataFeriadoFim,
                                                 string descricaoFeriado)
        {
            var sa = new JsonSerializerSettings();

            var feriados = from f in _context.Feriado
                           select f;

            if (!string.IsNullOrEmpty(sDataFeriadoInicio))
            {
                feriados = feriados.Where(u => u.Data >= DateTime.Parse(sDataFeriadoInicio));
            }
            if (!string.IsNullOrEmpty(sDataFeriadoFim))
            {
                feriados = feriados.Where(u => u.Data <= DateTime.Parse(sDataFeriadoFim));
            }
            if (!string.IsNullOrEmpty(descricaoFeriado))
            {
                feriados = feriados.Where(u => u.DescricaoFeriado.Contains(descricaoFeriado));
            }

            feriados = feriados.Where(u => u.Data >= DateTime.Now.Date);
            feriados = feriados.OrderBy(u => u.Data);

            ViewData["ListaItemsFeriado"] = feriados;
            return Json(feriados, sa);
        }

        public JsonResult ObterUsuariosLookup(string sCodigoUsuario,
                                              string sNomeUsuario)
        {
            var sa = new JsonSerializerSettings();

            var usuarios = from u in userManager.Users
                           select u;

            if (!string.IsNullOrEmpty(sCodigoUsuario))
            {
                usuarios = usuarios.Where(u => u.CodigoExterno == int.Parse(sCodigoUsuario));
            }
            if (!string.IsNullOrEmpty(sNomeUsuario))
            {
                usuarios = usuarios.Where(u => u.NomeUsuario.Contains(sNomeUsuario));
            }
            
            ViewData["ListaItemsUsuario"] = usuarios;
            return Json(usuarios, sa);
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // GET: PontoEletronicos
        public async Task<IActionResult> JornadasPendentes()
        {
            string codigoUsuario = userManager.GetUserId(User);

            if (User.IsInRole("Administrador") || User.IsInRole("Gerente"))
            {
                var solicitacoesJornadas = from p in _context.SolicitacaoJornada.Include(p => p.ApplicationUser).Include(p => p.Feriado)
                             .Where(u => u.SituacaoSolicitacao == Dominio.SituacaoSolicitacaoJornada.Pendente)
                                           select p;

                return View(await solicitacoesJornadas.AsNoTracking().ToListAsync());
            }

            else
            {
                TempData["msgJornadaPendente"] = "<script>alert('Operação Inválida!');</script>";
                return RedirectToAction(nameof(ListaJornadas));
            }
        }

        [Authorize(Roles = "Administrador, Gerente")]
        public async Task<IActionResult> DetalhesJornadaPendente(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string codigoUsuario = userManager.GetUserId(User);

            var solicitacaoJornadaPendente = await _context.SolicitacaoJornada.Include(p => p.ApplicationUser)
                    .FirstOrDefaultAsync(m => m.CodigoSolicitacaoJornada == id && m.SituacaoSolicitacao == Dominio.SituacaoSolicitacaoJornada.Pendente);
            if (solicitacaoJornadaPendente == null)
            {
                TempData["msgJornadaPendente"] = "<script>alert('Operação Inválida!');</script>";
                return RedirectToAction(nameof(ListaJornadas));
            }
            return View(solicitacaoJornadaPendente);
        }

        [Authorize(Roles = "Administrador, Gerente")]
        public async Task<IActionResult> AprovarReprovarJornada(int? id, string observacao, string situacao)
        {
            if (id == null)
            {
                TempData["msgJornadaPendente"] = "<script>alert('Operação Inválida!');</script>";
                return RedirectToAction(nameof(ListaJornadas));
            }

            if (string.IsNullOrEmpty(situacao))
            {
                TempData["msgJornadaPendente"] = "<script>alert('Operação Inválida!');</script>";
                return RedirectToAction(nameof(ListaJornadas));
            }

            var solicitacaoJornada = await _context.SolicitacaoJornada
                    .FirstOrDefaultAsync(m => m.CodigoSolicitacaoJornada == id && m.SituacaoSolicitacao == Dominio.SituacaoSolicitacaoJornada.Pendente);
            if (solicitacaoJornada == null)
            {
                TempData["msgJornadaPendente"] = "<script>alert('Operação Inválida!');</script>";
                return RedirectToAction(nameof(ListaJornadas));
            }

            string codigoUsuario = userManager.GetUserId(User);
            var usuarioAtualizacao = _context.Users.FirstOrDefault(u => u.Id == codigoUsuario);
            if (situacao == "Reprovar")
            {
                solicitacaoJornada.Observacoes = observacao;
                solicitacaoJornada.CodigoUsuarioAprovador = usuarioAtualizacao.CodigoExterno;
                solicitacaoJornada.SituacaoSolicitacao = Dominio.SituacaoSolicitacaoJornada.Recusada;

                _context.Update(solicitacaoJornada);
                await _context.SaveChangesAsync();

                TempData["msgJornadaPendente"] = "<script>alert('Solicitação Reprovada com Sucesso!');</script>";
                return RedirectToAction(nameof(ListaJornadas));
            }
            else if (situacao == "Aprovar")
            {
                var feriadoJornada = _context.Feriado.FirstOrDefault(u => u.CodigoFeriado == solicitacaoJornada.CodigoFeriado);
                if (feriadoJornada.Data <= DateTime.Now.Date)
                {
                    solicitacaoJornada.Observacoes = "Jornada Expirada";
                    solicitacaoJornada.SituacaoSolicitacao = Dominio.SituacaoSolicitacaoJornada.Recusada;

                    _context.Update(solicitacaoJornada);
                    await _context.SaveChangesAsync();

                    TempData["msgJornadaPendente"] = "<script>alert('Esta solicitação não pode ser aprovada. E está sendo automaticamente reprovada!');</script>";
                    return RedirectToAction(nameof(ListaJornadas));
                }

                solicitacaoJornada.Observacoes = !string.IsNullOrEmpty(observacao) ? observacao : "";
                solicitacaoJornada.CodigoUsuarioAprovador = usuarioAtualizacao.CodigoExterno;
                solicitacaoJornada.SituacaoSolicitacao = Dominio.SituacaoSolicitacaoJornada.Aprovada;
                _context.Update(solicitacaoJornada);
                await _context.SaveChangesAsync();
                
                TempData["msgJornadaPendente"] = "<script>alert('Solicitação Aprovada Com Sucesso!');</script>";
                return RedirectToAction(nameof(ListaJornadas));
            }

            TempData["msgJornadaPendente"] = "<script>alert('Operação Inválida!');</script>";
            return RedirectToAction(nameof(ListaJornadas));
        }
    }
}
