using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaControleEmpresarial.Data;
using SistemaControleEmpresarial.Models;

namespace SistemaControleEmpresarial.Controllers
{
    [Authorize(Roles = "Administrador, Gerente, Supervisor, Analista")]
    [Authorize]
    public class AjustePontoEletronicosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AjustePontoEletronicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AjustePontoEletronicos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AjustePontoEletronico.Include(a => a.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AjustePontoEletronicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ajustePontoEletronico = await _context.AjustePontoEletronico
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.CodigoAjuste == id);
            if (ajustePontoEletronico == null)
            {
                return NotFound();
            }

            return View(ajustePontoEletronico);
        }

        // GET: AjustePontoEletronicos/SolicitarAjustePonto
        public IActionResult SolicitarAjustePonto()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: AjustePontoEletronicos/SolicitarAjustePonto
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SolicitarAjustePonto([Bind("CodigoAjuste,UsuarioId,DataAjuste,HoraPrimeiraEntrada,HoraPrimeiraSaida,HoraSegundaEntrada,HoraSegundaSaida,Descricao,SituacaoAjuste,Observacoes")] AjustePontoEletronico ajustePontoEletronico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ajustePontoEletronico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", ajustePontoEletronico.UsuarioId);
            return View(ajustePontoEletronico);
        }

        // GET: AjustePontoEletronicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ajustePontoEletronico = await _context.AjustePontoEletronico.FindAsync(id);
            if (ajustePontoEletronico == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", ajustePontoEletronico.UsuarioId);
            return View(ajustePontoEletronico);
        }

        // POST: AjustePontoEletronicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodigoAjuste,UsuarioId,DataAjuste,HoraPrimeiraEntrada,HoraPrimeiraSaida,HoraSegundaEntrada,HoraSegundaSaida,Descricao,SituacaoAjuste,Observacoes")] AjustePontoEletronico ajustePontoEletronico)
        {
            if (id != ajustePontoEletronico.CodigoAjuste)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ajustePontoEletronico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AjustePontoEletronicoExists(ajustePontoEletronico.CodigoAjuste))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", ajustePontoEletronico.UsuarioId);
            return View(ajustePontoEletronico);
        }

        // GET: AjustePontoEletronicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ajustePontoEletronico = await _context.AjustePontoEletronico
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.CodigoAjuste == id);
            if (ajustePontoEletronico == null)
            {
                return NotFound();
            }

            return View(ajustePontoEletronico);
        }

        // POST: AjustePontoEletronicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ajustePontoEletronico = await _context.AjustePontoEletronico.FindAsync(id);
            _context.AjustePontoEletronico.Remove(ajustePontoEletronico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AjustePontoEletronicoExists(int id)
        {
            return _context.AjustePontoEletronico.Any(e => e.CodigoAjuste == id);
        }
    }
}
