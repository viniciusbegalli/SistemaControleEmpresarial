using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaControleEmpresarial.Data;
using SistemaControleEmpresarial.Models;

namespace SistemaControleEmpresarial.Controllers
{
    public class PontoEletronicosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public PontoEletronicosController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> _userManager)
        {
            _context = context;
            this.roleManager = roleManager;
            userManager = _userManager;
        }

        // GET: PontoEletronicos
        public async Task<IActionResult> MeuPontoEletronico()
        {
            string codigoUsuario = userManager.GetUserId(User);

            var pontoEletronicos = from p in _context.PontoEletronico
                                   select p;
            pontoEletronicos = pontoEletronicos.Where(u => u.UserId == codigoUsuario);
            pontoEletronicos = pontoEletronicos.OrderByDescending(u => u.DataHoraPrimeiraEntrada);

            return View(await pontoEletronicos.AsNoTracking().ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarPonto([Bind("CodigoPontoEletronico,Usuario,UsuarioId")] PontoEletronico pontoEletronico)
        {
            string codigoUsuario = userManager.GetUserId(User);
            DateTime agora = DateTime.Now;
            DateTime hoje = DateTime.Today;

            PontoEletronico pontoEletronicoExistente = _context.PontoEletronico
                    .FirstOrDefault(m => m.UserId == codigoUsuario
                                    && m.DataHoraPrimeiraEntrada > hoje &&
                                    (string.IsNullOrEmpty(m.DataHoraPrimeiraSaida.ToString())
                                    || string.IsNullOrEmpty(m.DataHoraSegundaEntrada.ToString())
                                    || string.IsNullOrEmpty(m.DataHoraSegundaSaida.ToString())));

            if (pontoEletronicoExistente != null)
            {
                if (string.IsNullOrEmpty(pontoEletronicoExistente.DataHoraPrimeiraSaida.ToString()))
                {
                    pontoEletronicoExistente.DataHoraPrimeiraSaida = agora;
                    _context.Update(pontoEletronicoExistente);
                    await _context.SaveChangesAsync();
                }
                else if (string.IsNullOrEmpty(pontoEletronicoExistente.DataHoraSegundaEntrada.ToString()))
                {
                    pontoEletronicoExistente.DataHoraSegundaEntrada = agora;
                    _context.Update(pontoEletronicoExistente);
                    await _context.SaveChangesAsync();
                }
                else if (string.IsNullOrEmpty(pontoEletronicoExistente.DataHoraSegundaSaida.ToString()))
                {
                    pontoEletronicoExistente.DataHoraSegundaSaida = agora;
                    _context.Update(pontoEletronicoExistente);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                PontoEletronico pontoEletronicoJaConcluido = _context.PontoEletronico
                    .FirstOrDefault(m => m.UserId == codigoUsuario
                    && m.DataHoraPrimeiraEntrada > hoje
                    && !string.IsNullOrEmpty(m.DataHoraPrimeiraSaida.ToString())
                    && !string.IsNullOrEmpty(m.DataHoraSegundaEntrada.ToString())
                    && !string.IsNullOrEmpty(m.DataHoraSegundaSaida.ToString()));

                if (pontoEletronicoJaConcluido != null)
                {
                    TempData["msgRegistroPonto"] = "<script>alert('Você já registrou todos os pontos hoje.');</script>";
                    return RedirectToAction(nameof(MeuPontoEletronico));
                }
                else
                {
                    pontoEletronico.UserId = codigoUsuario;
                    pontoEletronico.DataHoraPrimeiraEntrada = agora;
                    _context.Add(pontoEletronico);
                    await _context.SaveChangesAsync();
                }
            }

            TempData["msgRegistroPonto"] = "<script>alert('Ponto Registrado Com Sucesso!');</script>";
            return RedirectToAction(nameof(MeuPontoEletronico));
        }

        public IActionResult ExportarExcel()
        {
            string codigoUsuario = userManager.GetUserId(User);

            var pontoEletronicos = from p in _context.PontoEletronico
                                   select p;
            pontoEletronicos = pontoEletronicos.Where(u => u.UserId == codigoUsuario);
            pontoEletronicos = pontoEletronicos.OrderByDescending(u => u.DataHoraPrimeiraEntrada);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("MeuPontoEletronico");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Data";
                worksheet.Cell(currentRow, 2).Value = "PrimeiraEntrada";
                worksheet.Cell(currentRow, 3).Value = "PrimeiraSaida";
                worksheet.Cell(currentRow, 4).Value = "SegundaEntrada";
                worksheet.Cell(currentRow, 5).Value = "SegundaSaida";

                foreach (var pontoEletronico in pontoEletronicos)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = pontoEletronico.Data;
                    worksheet.Cell(currentRow, 2).Value = pontoEletronico.HoraPrimeiraEntrada;
                    worksheet.Cell(currentRow, 3).Value = pontoEletronico.HoraPrimeiraSaida;
                    worksheet.Cell(currentRow, 4).Value = pontoEletronico.HoraSegundaEntrada;
                    worksheet.Cell(currentRow, 5).Value = pontoEletronico.HoraSegundaSaida;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "users.xlsx");
                }
            }
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
                return RedirectToAction(nameof(MeuPontoEletronico));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", ajustePontoEletronico.UsuarioId);
            return View(ajustePontoEletronico);
        }

        // GET: PontoEletronicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontoEletronico = await _context.PontoEletronico
                .FirstOrDefaultAsync(m => m.CodigoPontoEletronico == id);
            if (pontoEletronico == null)
            {
                return NotFound();
            }

            return View(pontoEletronico);
        }

        // GET: PontoEletronicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PontoEletronicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoPontoEletronico,CodigoUsuario,DataHoraPrimeiraEntrada,DataHoraPrimeiraSaida,DataHoraSegundaEntrada,DataHoraSegundaSaida")] PontoEletronico pontoEletronico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pontoEletronico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MeuPontoEletronico));
            }
            return View(pontoEletronico);
        }

        // GET: PontoEletronicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontoEletronico = await _context.PontoEletronico.FindAsync(id);
            if (pontoEletronico == null)
            {
                return NotFound();
            }
            return View(pontoEletronico);
        }

        // POST: PontoEletronicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodigoPontoEletronico,CodigoUsuario,DataHoraPrimeiraEntrada,DataHoraPrimeiraSaida,DataHoraSegundaEntrada,DataHoraSegundaSaida")] PontoEletronico pontoEletronico)
        {
            if (id != pontoEletronico.CodigoPontoEletronico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pontoEletronico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PontoEletronicoExists(pontoEletronico.CodigoPontoEletronico))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MeuPontoEletronico));
            }
            return View(pontoEletronico);
        }

        // GET: PontoEletronicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontoEletronico = await _context.PontoEletronico
                .FirstOrDefaultAsync(m => m.CodigoPontoEletronico == id);
            if (pontoEletronico == null)
            {
                return NotFound();
            }

            return View(pontoEletronico);
        }

        // POST: PontoEletronicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pontoEletronico = await _context.PontoEletronico.FindAsync(id);
            _context.PontoEletronico.Remove(pontoEletronico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MeuPontoEletronico));
        }

        private bool PontoEletronicoExists(int id)
        {
            return _context.PontoEletronico.Any(e => e.CodigoPontoEletronico == id);
        }
    }
}
