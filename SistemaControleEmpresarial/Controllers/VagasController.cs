using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaControleEmpresarial.Data;
using SistemaControleEmpresarial.Models;

namespace SistemaControleEmpresarial.Controllers
{
    public class VagasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public VagasController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> _userManager)
        {
            _context = context;
            this.roleManager = roleManager;
            userManager = _userManager;
        }

        // GET: Vagas
        public async Task<IActionResult> ListaVagas(string situacaoVaga)
        {
            if (User.IsInRole("Administrador") || User.IsInRole("Gerente"))
            {
                var vagas = from f in _context.Vaga.Include(f => f.ApplicationUser)
                               select f;

                if (!string.IsNullOrEmpty(situacaoVaga))
                {
                    vagas = vagas.Where(v => v.SituacaoVaga == situacaoVaga);
                }

                return View(await vagas.AsNoTracking().ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Vaga.Include(v => v.ApplicationUser).Where(v => v.SituacaoVaga == Dominio.SituacaoVaga.Aberta);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // GET: ListaVagaCandidatos
        public async Task<IActionResult> ListaVagaCandidatos(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga.FindAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }

            var vagaCandidatos = from f in _context.VagaCandidato.Include(f => f.Vaga)
                        select f;
            vagaCandidatos = vagaCandidatos.Where(v => v.CodigoVaga == vaga.CodigoVaga);

            ViewData["CodigoVaga"] = vaga.CodigoVaga;

            return View(await vagaCandidatos.AsNoTracking().ToListAsync());
        }
        
        // GET: Vagas
        public async Task<IActionResult> CandidatarVaga(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga.FindAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }

            VagaCandidato vagaCandidato = new VagaCandidato();
            vagaCandidato.CodigoVaga = vaga.CodigoVaga;
            return View(vagaCandidato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CandidatarVaga([Bind("CodigoVaga,NomeCandidato,Email,LinkedIn,Observacoes")] VagaCandidato vagaCandidato)
        {
            var vagaCandidatoExistente = _context.VagaCandidato.FirstOrDefault(v => v.CodigoVaga == vagaCandidato.CodigoVaga && v.Email == vagaCandidato.Email);

            if (vagaCandidatoExistente != null)
            {
                TempData["msgCriacaoVaga"] = "<script>alert('E-mail já inscrito para esta vaga!');</script>";
                return RedirectToAction(nameof(ListaVagas));
            }

            _context.Add(vagaCandidato);
            await _context.SaveChangesAsync();

            TempData["msgCriacaoVaga"] = "<script>alert('Candidatura Enviada Com Sucesso!');</script>";

            return RedirectToAction(nameof(ListaVagas));
        }

        public async Task<IActionResult> DetalhesVaga(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga
                .FirstOrDefaultAsync(v => v.CodigoVaga == id);
            if (vaga == null)
            {
                return NotFound();
            }

            return View(vaga);
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // GET: Vagas/CriarVaga
        public IActionResult CriarVaga()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // POST: Vagas/CriarVaga
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarVaga([Bind("CodigoVaga,Titulo,Descricao")] Vaga vaga)
        {
            string codigoUsuario = userManager.GetUserId(User);
            DateTime dataHoraCriacao = DateTime.Now;

            vaga.DataCriacao = dataHoraCriacao;
            vaga.UserId = codigoUsuario;
            vaga.SituacaoVaga = Dominio.SituacaoVaga.Aberta;
            _context.Add(vaga);
            await _context.SaveChangesAsync();

            int codigoLogAlteracao = LogarAlteracoes(vaga.CodigoVaga, Dominio.TipoOperacaoLog.Inclusao, codigoUsuario);
            CriarLogAlteracaoItem(codigoLogAlteracao, "Titulo", string.Empty, vaga.Titulo);
            CriarLogAlteracaoItem(codigoLogAlteracao, "Descricao", string.Empty, vaga.Descricao);
            CriarLogAlteracaoItem(codigoLogAlteracao, "SituacaoVaga", string.Empty, vaga.SituacaoVaga);
            CriarLogAlteracaoItem(codigoLogAlteracao, "UsuarioCriacao", string.Empty, codigoUsuario.ToString());
            CriarLogAlteracaoItem(codigoLogAlteracao, "DataHoraCriacao", string.Empty, dataHoraCriacao.ToString());
            _context.SaveChanges();

            TempData["msgCriacaoVaga"] = "<script>alert('Vaga Cadastrada Com Sucesso!');</script>";

            return RedirectToAction(nameof(ListaVagas));
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // GET: Vagas/EditarVaga/5
        public async Task<IActionResult> EditarVaga(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga.FindAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", vaga.UserId);
            return View(vaga);
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // GET: Vagas/ReabrirVaga/5
        public async Task<IActionResult> ReabrirVaga(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga.FindAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }

            DateTime dataHoraAtual = DateTime.Now;
            string codigoUsuario = userManager.GetUserId(User);
            var usuarioAtualizacao = _context.Users.FirstOrDefault(u => u.Id == codigoUsuario);

            vaga.DataHoraUltimaAlteracao = dataHoraAtual;
            vaga.CodigoUsuarioUltimaAtualizacao = usuarioAtualizacao.CodigoExterno.ToString();
            vaga.NomeUsuarioUltimaAtualizacao = usuarioAtualizacao.NomeUsuario;
            vaga.SituacaoVaga = Dominio.SituacaoVaga.Aberta;
            int codigoLogAlteracao = LogarAlteracoes(vaga.CodigoVaga, Dominio.TipoOperacaoLog.Edicao, codigoUsuario);
            CriarLogAlteracaoItem(codigoLogAlteracao, "SituacaoVaga", vaga.SituacaoVaga, vaga.SituacaoVaga);
            _context.Update(vaga);

            await _context.SaveChangesAsync();

            TempData["msgCriacaoVaga"] = "<script>alert('Vaga Reaberta Com Sucesso!');</script>";

            return RedirectToAction(nameof(ListaVagas));
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // GET: Vagas/FecharVaga/5
        public async Task<IActionResult> FecharVaga(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga.FindAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }

            DateTime dataHoraAtual = DateTime.Now;
            string codigoUsuario = userManager.GetUserId(User);
            var usuarioAtualizacao = _context.Users.FirstOrDefault(u => u.Id == codigoUsuario);

            vaga.DataHoraUltimaAlteracao = dataHoraAtual;
            vaga.CodigoUsuarioUltimaAtualizacao = usuarioAtualizacao.CodigoExterno.ToString();
            vaga.NomeUsuarioUltimaAtualizacao = usuarioAtualizacao.NomeUsuario;
            vaga.SituacaoVaga = Dominio.SituacaoVaga.Fechada;
            int codigoLogAlteracao = LogarAlteracoes(vaga.CodigoVaga, Dominio.TipoOperacaoLog.Edicao, codigoUsuario);
            CriarLogAlteracaoItem(codigoLogAlteracao, "SituacaoVaga", vaga.SituacaoVaga, vaga.SituacaoVaga);
            _context.Update(vaga);

            await _context.SaveChangesAsync();

            TempData["msgCriacaoVaga"] = "<script>alert('Vaga Fechada Com Sucesso!');</script>";

            return RedirectToAction(nameof(ListaVagas));
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // POST: Vagas/EditarVaga/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarVaga(int id, [Bind("CodigoVaga,Titulo,Descricao")] Vaga vaga)
        {
            var vagaCandidatosExistente = await _context.VagaCandidato
                .FirstOrDefaultAsync(s => s.CodigoVaga == id);
            if (vagaCandidatosExistente != null)
            {
                TempData["msgCriacaoVaga"] = "<script>alert('Vaga não pode ser editada, pois já existem candidatos registrados.');</script>";
                return RedirectToAction(nameof(ListaVagas));
            }

            if (id != vaga.CodigoVaga)
            {
                return NotFound();
            }

            DateTime dataHoraAtual = DateTime.Now;
            var vagaAtualizacao = await _context.Vaga
            .FirstOrDefaultAsync(m => m.CodigoVaga == id);

            string tituloAntigo = vagaAtualizacao.Titulo;
            string descricaoAntiga = vagaAtualizacao.Descricao;

            string codigoUsuario = userManager.GetUserId(User);
            var usuarioAtualizacao = _context.Users.FirstOrDefault(u => u.Id == codigoUsuario);

            vagaAtualizacao.Titulo = vaga.Titulo;
            vagaAtualizacao.Descricao = vaga.Descricao;
            vagaAtualizacao.DataHoraUltimaAlteracao = dataHoraAtual;
            vagaAtualizacao.CodigoUsuarioUltimaAtualizacao = usuarioAtualizacao.CodigoExterno.ToString();
            vagaAtualizacao.NomeUsuarioUltimaAtualizacao = usuarioAtualizacao.NomeUsuario;

            int codigoLogAlteracao = LogarAlteracoes(vagaAtualizacao.CodigoVaga, Dominio.TipoOperacaoLog.Edicao, codigoUsuario);
            CriarLogAlteracaoItem(codigoLogAlteracao, "Titulo", tituloAntigo, vaga.Titulo);
            CriarLogAlteracaoItem(codigoLogAlteracao, "Descricao", descricaoAntiga, vaga.Descricao);
            _context.Update(vagaAtualizacao);

            await _context.SaveChangesAsync();
            TempData["msgCriacaoVaga"] = "<script>alert('Vaga Alterada Com Sucesso!');</script>";

            return RedirectToAction(nameof(ListaVagas));
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // GET: Vagas/ExcluirVaga/5
        public async Task<IActionResult> ExcluirVaga(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga
                .Include(v => v.ApplicationUser)
                .FirstOrDefaultAsync(m => m.CodigoVaga == id);
            if (vaga == null)
            {
                return NotFound();
            }

            return View(vaga);
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // POST: Vagas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vagaCandidatosExistente = await _context.VagaCandidato
                .FirstOrDefaultAsync(s => s.CodigoVaga == id);
            if (vagaCandidatosExistente != null)
            {
                TempData["msgCriacaoVaga"] = "<script>alert('Vaga não pode ser excluída, pois já existem candidatos registrados.');</script>";
                return RedirectToAction(nameof(ListaVagas));
            }

            string codigoUsuario = userManager.GetUserId(User);

            int codigoLogAlteracao = LogarAlteracoes(id, Dominio.TipoOperacaoLog.Exclusao, codigoUsuario);

            var vaga = await _context.Vaga.FindAsync(id);
            _context.Vaga.Remove(vaga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListaVagas));
        }

        private bool VagaExists(int id)
        {
            return _context.Vaga.Any(e => e.CodigoVaga == id);
        }

        public int LogarAlteracoes(int codigoFeriado, string tipoOperacao, string codigoUsuario)
        {
            LogAlteracao logAlteracao = new LogAlteracao();
            logAlteracao.DataCriacao = DateTime.Now;
            logAlteracao.TipoOperacaoLog = tipoOperacao;
            logAlteracao.UserId = codigoUsuario;
            logAlteracao.Entidade = "Vaga";
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

        [Authorize(Roles = "Administrador, Gerente")]
        public IActionResult ExportarExcel()
        {
            string situacaoVaga;
            RecuperarParametrosConsulta(out situacaoVaga);

            var vagas = from f in _context.Vaga.Include(f => f.ApplicationUser)
                           select f;

            if (!string.IsNullOrEmpty(situacaoVaga))
            {
                vagas = vagas.Where(u => u.SituacaoVaga == situacaoVaga);
            }
            
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Vagas");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Titulo";
                worksheet.Cell(currentRow, 2).Value = "Descricao";
                worksheet.Cell(currentRow, 3).Value = "UsuarioCriacao";
                worksheet.Cell(currentRow, 4).Value = "DataCriacao";
                worksheet.Cell(currentRow, 5).Value = "Situacao";
                worksheet.Cell(currentRow, 6).Value = "UsuarioUltimaAlteracao";
                worksheet.Cell(currentRow, 7).Value = "DataHoraUltimaAlteracao";

                foreach (var vaga in vagas)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = vaga.Titulo;
                    worksheet.Cell(currentRow, 2).Value = vaga.Descricao;
                    worksheet.Cell(currentRow, 3).Value = vaga.ApplicationUser.CodigoExterno + " - " + vaga.ApplicationUser.NomeUsuario;
                    worksheet.Cell(currentRow, 4).Value = vaga.DataCriacao;
                    worksheet.Cell(currentRow, 5).Value = vaga.SituacaoVaga;
                    worksheet.Cell(currentRow, 6).Value = vaga.CodigoUsuarioUltimaAtualizacao + " - " + vaga.NomeUsuarioUltimaAtualizacao;
                    worksheet.Cell(currentRow, 7).Value = vaga.DataHoraUltimaAlteracao;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Vagas.xlsx");
                }
            }
        }

        [Authorize(Roles = "Administrador, Gerente")]
        public IActionResult ExportarCandidatosExcel(int id)
        {
            var vaga = _context.Vaga.Where(u => u.CodigoVaga == id).FirstOrDefault();
            if (vaga == null)
            {
                return NotFound();
            }

            var vagaCandidatos = from f in _context.VagaCandidato.Include(f => f.Vaga)
                                 select f;
            vagaCandidatos = vagaCandidatos.Where(v => v.CodigoVaga == vaga.CodigoVaga);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("VagaCandidatos");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "CodigoVaga";
                worksheet.Cell(currentRow, 2).Value = "NomeCandidato";
                worksheet.Cell(currentRow, 3).Value = "Email";
                worksheet.Cell(currentRow, 4).Value = "LinkedIn";
                worksheet.Cell(currentRow, 5).Value = "Observacoes";

                foreach (var vagaCandidato in vagaCandidatos)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = vagaCandidato.CodigoVaga;
                    worksheet.Cell(currentRow, 2).Value = vagaCandidato.NomeCandidato;
                    worksheet.Cell(currentRow, 3).Value = vagaCandidato.Email;
                    worksheet.Cell(currentRow, 4).Value = vagaCandidato.LinkedIn;
                    worksheet.Cell(currentRow, 5).Value = vagaCandidato.Observacoes;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "VagaCandidatos.xlsx");
                }
            }
        }

        private void SetarParametrosDeConsulta(string situacaoVaga)
        {
            HttpContext.Session.SetString("paramSituacaoVaga", situacaoVaga);
        }

        private void RecuperarParametrosConsulta(out string situacaoVaga)
        {
            situacaoVaga = HttpContext.Session.GetString("paramSituacaoVaga");
        }
    }
}