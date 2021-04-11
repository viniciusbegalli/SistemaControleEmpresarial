using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaControleEmpresarial.Data;
using SistemaControleEmpresarial.Models;

namespace SistemaControleEmpresarial.Controllers
{
    [Authorize(Roles = "Administrador, Gerente, Supervisor, Analista")]
    public class ChamadosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public ChamadosController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> _userManager)
        {
            _context = context;
            this.roleManager = roleManager;
            userManager = _userManager;
        }

        // GET: Chamados
        public async Task<IActionResult> ListaChamados()
        {
            string codigoUsuario = userManager.GetUserId(User);

            var chamados = from p in _context.Chamado
                                   select p;
            chamados = chamados.Where(u => u.UserId == codigoUsuario);
            chamados = chamados.OrderBy(u => u.IDPrioridade);

            return View(await chamados.AsNoTracking().ToListAsync());
        }

        // GET: Chamados
        public async Task<IActionResult> FilaChamados()
        {
            IQueryable<Chamado> chamados = ChamadosNaFila();

            return View(await chamados.AsNoTracking().ToListAsync());
        }

        private IQueryable<Chamado> ChamadosNaFila()
        {
            string codigoUsuario = userManager.GetUserId(User);

            var chamados = from p in _context.Chamado.Include(p => p.ApplicationUser)
                           select p;

            if (User.IsInRole("Administrador"))
            {
                chamados = chamados.Where(u => u.Fila == Dominio.FilaChamado.Administrador);
            }
            else if (User.IsInRole("Gerente"))
            {
                chamados = chamados.Where(u => u.Fila == Dominio.FilaChamado.Gerente);
            }
            else if (User.IsInRole("Supervisor"))
            {
                chamados = chamados.Where(u => u.Fila == Dominio.FilaChamado.Supervisor);
            }
            else if (User.IsInRole("Analista"))
            {
                chamados = chamados.Where(u => u.Fila == Dominio.FilaChamado.Analista);
            }

            chamados = chamados.Where(u => u.SituacaoChamado == Dominio.SituacaoChamado.Aberto);
            chamados = chamados.OrderBy(u => u.IDPrioridade);
            return chamados;
        }

        // GET: Chamados/DetalhesChamado/5
        public async Task<IActionResult> DetalhesChamado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chamado = await _context.Chamado
                .Include(c => c.ApplicationUser)
                .Include(c => c.Notas)
                .FirstOrDefaultAsync(m => m.CodigoChamado == id);
            if (chamado == null)
            {
                return NotFound();
            }

            return View(chamado);
        }

        // GET: Chamados/FecharChamado/5
        public async Task<IActionResult> FecharChamado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chamado = await _context.Chamado
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.CodigoChamado == id);
            if (chamado == null)
            {
                return NotFound();
            }

            DateTime dataHoraAtual = DateTime.Now;
            string codigoUsuario = userManager.GetUserId(User);

            if (chamado.UserId != codigoUsuario)
            {
                TempData["msgCriacaoChamado"] = "<script>alert('Operação Inválida!');</script>";
                return RedirectToAction(nameof(ListaChamados));
            }

            ChamadoNota chamadoNota = new ChamadoNota();
            chamadoNota.CodigoChamado = chamado.CodigoChamado;
            chamadoNota.DataNota = dataHoraAtual;
            chamadoNota.UserId = codigoUsuario;
            chamadoNota.DescricaoNota = "Chamado Fechado";

            _context.Add(chamadoNota);
            await _context.SaveChangesAsync();

            chamado.SituacaoChamado = Dominio.SituacaoChamado.Fechado;
            _context.Update(chamado);

            await _context.SaveChangesAsync();

            TempData["msgCriacaoChamado"] = "<script>alert('Chamado Fechado Com Sucesso!');</script>";
            return RedirectToAction(nameof(ListaChamados));
        }

        // GET: Chamados/ReabrirChamado/5
        public async Task<IActionResult> ReabrirChamado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chamado = await _context.Chamado
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.CodigoChamado == id);
            if (chamado == null)
            {
                return NotFound();
            }

            DateTime dataHoraAtual = DateTime.Now;
            string codigoUsuario = userManager.GetUserId(User);

            if (chamado.UserId != codigoUsuario)
            {
                TempData["msgCriacaoChamado"] = "<script>alert('Operação Inválida!');</script>";
                return RedirectToAction(nameof(ListaChamados));
            }

            ChamadoNota chamadoNota = new ChamadoNota();
            chamadoNota.CodigoChamado = chamado.CodigoChamado;
            chamadoNota.DataNota = dataHoraAtual;
            chamadoNota.UserId = codigoUsuario;
            chamadoNota.DescricaoNota = "Chamado Reaberto";

            _context.Add(chamadoNota);
            await _context.SaveChangesAsync();

            chamado.SituacaoChamado = Dominio.SituacaoChamado.Aberto;
            _context.Update(chamado);

            await _context.SaveChangesAsync();

            TempData["msgCriacaoChamado"] = "<script>alert('Chamado Reaberto Com Sucesso!');</script>";
            return RedirectToAction(nameof(ListaChamados));
        }

        // GET: Chamados/AdicionarNotaChamado/5
        public async Task<IActionResult> AdicionarNotaChamado(int? id, string notaChamado)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chamado = await _context.Chamado
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.CodigoChamado == id);
            if (chamado == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(notaChamado))
            {
                TempData["msgCriacaoChamado"] = "<script>alert('Preencha a nota para incluí-la!');</script>";
                return RedirectToAction("DetalhesChamado", new { id = chamado.CodigoChamado });
            }

            DateTime dataHoraAtual = DateTime.Now;
            string codigoUsuario = userManager.GetUserId(User);
            var usuarioAtualizacao = _context.Users.FirstOrDefault(u => u.Id == codigoUsuario);

            ChamadoNota chamadoNota = new ChamadoNota();
            chamadoNota.CodigoChamado = chamado.CodigoChamado;
            chamadoNota.DataNota = dataHoraAtual;
            chamadoNota.UserId = codigoUsuario;
            chamadoNota.CodigoExterno = usuarioAtualizacao.CodigoExterno;
            chamadoNota.NomeUsuarioNota = usuarioAtualizacao.NomeUsuario;
            chamadoNota.DescricaoNota = notaChamado;

            _context.Add(chamadoNota);
            await _context.SaveChangesAsync();

            TempData["msgCriacaoChamado"] = "<script>alert('Nota Adicionada Com Sucesso!');</script>";
            return RedirectToAction("DetalhesChamado", new { id = chamado.CodigoChamado });
        }

        // GET: Chamados/CriarChamado
        public IActionResult CriarChamado()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Chamados/CriarChamado
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarChamado([Bind("Titulo,Descricao,Fila,Prioridade")] Chamado chamado)
        {
            string codigoUsuario = userManager.GetUserId(User);
            DateTime dataHoraCriacao = DateTime.Now;
            chamado.DataCriacao = dataHoraCriacao;
            chamado.SituacaoChamado = Dominio.SituacaoChamado.Aberto;
            chamado.UserId = codigoUsuario;

            if (chamado.Prioridade == Dominio.Prioridade.Alta)
            {
                chamado.IDPrioridade = Dominio.IDPrioridade.Alta;
            }
            else if (chamado.Prioridade == Dominio.Prioridade.Media)
            {
                chamado.IDPrioridade = Dominio.IDPrioridade.Media;
            }
            else
            {
                chamado.IDPrioridade = Dominio.IDPrioridade.Baixa;
            }

            _context.Add(chamado);
            await _context.SaveChangesAsync();
            TempData["msgCriacaoChamado"] = "<script>alert('Chamado Aberto Com Sucesso!');</script>";
            return RedirectToAction(nameof(ListaChamados));
        }

        // GET: Chamados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chamado = await _context.Chamado.FindAsync(id);
            if (chamado == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", chamado.UserId);
            return View(chamado);
        }

        // POST: Chamados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodigoChamado,UserId,DataCriacao,Titulo,Descricao,SituacaoChamado,Fila")] Chamado chamado)
        {
            if (id != chamado.CodigoChamado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chamado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChamadoExists(chamado.CodigoChamado))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListaChamados));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", chamado.UserId);
            return View(chamado);
        }

        // GET: Chamados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chamado = await _context.Chamado
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.CodigoChamado == id);
            if (chamado == null)
            {
                return NotFound();
            }

            return View(chamado);
        }

        // POST: Chamados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chamado = await _context.Chamado.FindAsync(id);
            _context.Chamado.Remove(chamado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListaChamados));
        }

        private bool ChamadoExists(int id)
        {
            return _context.Chamado.Any(e => e.CodigoChamado == id);
        }

        public IActionResult ExportarExcel()
        {
            string codigoUsuario = userManager.GetUserId(User);

            var chamados = from p in _context.Chamado
                           select p;
            chamados = chamados.Where(u => u.UserId == codigoUsuario);
            chamados = chamados.OrderBy(u => u.IDPrioridade);
            
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Chamados");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "CodigoChamado";
                worksheet.Cell(currentRow, 2).Value = "DataCriacao";
                worksheet.Cell(currentRow, 3).Value = "Titulo";
                worksheet.Cell(currentRow, 4).Value = "Descricao";
                worksheet.Cell(currentRow, 5).Value = "SituacaoChamado";
                worksheet.Cell(currentRow, 6).Value = "Fila";
                worksheet.Cell(currentRow, 7).Value = "Prioridade";

                foreach (var chamado in chamados)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = chamado.CodigoChamado;
                    worksheet.Cell(currentRow, 2).Value = chamado.DataCriacao;
                    worksheet.Cell(currentRow, 3).Value = chamado.Titulo;
                    worksheet.Cell(currentRow, 4).Value = chamado.Descricao;
                    worksheet.Cell(currentRow, 5).Value = chamado.SituacaoChamado;
                    worksheet.Cell(currentRow, 6).Value = chamado.Fila;
                    worksheet.Cell(currentRow, 7).Value = chamado.Prioridade;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Chamados.xlsx");
                }
            }
        }

        public IActionResult ExportarExcelChamadosFila()
        {
            IQueryable<Chamado> chamados = ChamadosNaFila();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Chamados");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "CodigoChamado";
                worksheet.Cell(currentRow, 2).Value = "Usuario";
                worksheet.Cell(currentRow, 3).Value = "DataCriacao";
                worksheet.Cell(currentRow, 4).Value = "Titulo";
                worksheet.Cell(currentRow, 5).Value = "Descricao";
                worksheet.Cell(currentRow, 6).Value = "SituacaoChamado";
                worksheet.Cell(currentRow, 7).Value = "Fila";
                worksheet.Cell(currentRow, 8).Value = "Prioridade";

                foreach (var chamado in chamados)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = chamado.CodigoChamado;
                    worksheet.Cell(currentRow, 2).Value = chamado.ApplicationUser.CodigoExterno + " - " + chamado.ApplicationUser.NomeUsuario;
                    worksheet.Cell(currentRow, 3).Value = chamado.DataCriacao;
                    worksheet.Cell(currentRow, 4).Value = chamado.Titulo;
                    worksheet.Cell(currentRow, 5).Value = chamado.Descricao;
                    worksheet.Cell(currentRow, 6).Value = chamado.SituacaoChamado;
                    worksheet.Cell(currentRow, 7).Value = chamado.Fila;
                    worksheet.Cell(currentRow, 8).Value = chamado.Prioridade;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Chamados.xlsx");
                }
            }
        }
    }
}