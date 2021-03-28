using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaControleEmpresarial.Models;
using SistemaControleEmpresarial.Data;
using Microsoft.AspNetCore.Identity;
using SistemaControleEmpresarial.Controllers;

namespace SistemaControleEmpresarial.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public FuncionariosController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> _userManager)
        {
            _context = context;
            this.roleManager = roleManager;
            userManager = _userManager;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
            string codigoUsuario = userManager.GetUserId(User);

            Funcionario funcionario = _context.Funcionario
                        .FirstOrDefault(m => m.CodigoUsuario == codigoUsuario);

            if (funcionario == null)
            {
                return RedirectToAction(nameof(CreateFuncionario));
            }
            else
            {
                return View(funcionario);
            }
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(m => m.CodigoFuncionario == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/CreateFuncionario
        public IActionResult CreateFuncionario()
        {
            return View();
        }

        // POST: Funcionarios/CreateFuncionario
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFuncionario([Bind("NomeUsuario,Sexo,DataNascimento,CPF,Telefone")] Funcionario funcionario)
        {
            if (!Util.ValidarCPF(funcionario.CPF))
            {
                ModelState.AddModelError("", "CPF Inválido.");
                return View(funcionario);
            }
            string codigoUsuario = userManager.GetUserId(User);
            funcionario.CodigoUsuario = codigoUsuario;
            _context.Add(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> EditFuncionario(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFuncionario(int id, [Bind("CodigoFuncionario,CodigoUsuario,NomeUsuario,Sexo,DataNascimento,CPF,Telefone")] Funcionario funcionario)
        {
            if (id != funcionario.CodigoFuncionario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.CodigoFuncionario))
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
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(m => m.CodigoFuncionario == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionario.FindAsync(id);
            _context.Funcionario.Remove(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionario.Any(e => e.CodigoFuncionario == id);
        }
    }
}
