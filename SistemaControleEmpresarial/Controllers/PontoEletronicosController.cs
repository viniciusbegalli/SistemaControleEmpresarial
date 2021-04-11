using System;
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

        [Authorize(Roles = "Supervisor, Analista")]
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

        [Authorize(Roles = "Supervisor, Analista")]
        // GET: PontoEletronicos
        public async Task<IActionResult> AjustesPonto()
        {
            string codigoUsuario = userManager.GetUserId(User);

            var ajustePontoEletronicos = from p in _context.AjustePontoEletronico
                                         select p;
            ajustePontoEletronicos = ajustePontoEletronicos.Where(u => u.UsuarioId == codigoUsuario);

            return View(await ajustePontoEletronicos.AsNoTracking().ToListAsync());
        }


        [Authorize(Roles = "Gerente, Supervisor")]
        // GET: PontoEletronicos
        public async Task<IActionResult> AjustesPendentes()
        {
            string codigoUsuario = userManager.GetUserId(User);

            
            if (User.IsInRole("Administrador") || User.IsInRole("Gerente"))
            {
                var ajustePontoEletronicos = from p in _context.AjustePontoEletronico.Include(p => p.ApplicationUser)
                             .Where(u => u.SituacaoAjuste == Dominio.SituacaoAjustePonto.Pendente && u.UsuarioId != codigoUsuario)
                                             join ur in _context.UserRoles on p.ApplicationUser.Id equals ur.UserId
                                             join r in _context.Roles on ur.RoleId equals r.Id
                                             join r2 in _context.Roles on
                                             new { r.Id, r.Name }
                                                    equals new { r2.Id, Name = "Supervisor" }
                                             select p;
                return View(await ajustePontoEletronicos.AsNoTracking().ToListAsync());
            }
            else
            {
                var ajustePontoEletronicos = from p in _context.AjustePontoEletronico.Include(p => p.ApplicationUser)
                                             .Where(u => u.SituacaoAjuste == Dominio.SituacaoAjustePonto.Pendente && u.UsuarioId != codigoUsuario)
                                             join ur in _context.UserRoles on p.ApplicationUser.Id equals ur.UserId
                                             join r in _context.Roles on ur.RoleId equals r.Id
                                             join r2 in _context.Roles on 
                                             new { r.Id, r.Name }
                                                    equals new { r2.Id, Name = "Analista" }
                                             select p;
      
                return View(await ajustePontoEletronicos.AsNoTracking().ToListAsync());
            }
        }

        [Authorize(Roles = "Supervisor, Analista")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarPonto([Bind("CodigoPontoEletronico,Usuario,UsuarioId")] PontoEletronico pontoEletronico)
        {
            string codigoUsuario = userManager.GetUserId(User);
            DateTime agora = DateTime.Now;
            DateTime hoje = DateTime.Today;

            var feriadoExistente = await _context.Feriado
                .FirstOrDefaultAsync(m => m.Data.Date == hoje.Date);

            if (feriadoExistente != null)
            {
                var usuarioAtualizacao = _context.Users.FirstOrDefault(u => u.Id == codigoUsuario);
                var solicitacaoJornada = await _context.SolicitacaoJornada
                    .FirstOrDefaultAsync(s => s.CodigoFeriado == feriadoExistente.CodigoFeriado
                    && s.CodigoUsuarioJornada == usuarioAtualizacao.CodigoExterno
                    && s.SituacaoSolicitacao == Dominio.SituacaoSolicitacaoJornada.Aprovada);

                if (solicitacaoJornada == null)
                {
                    TempData["msgRegistroPonto"] = "<script>alert('Você não possui uma jornada aprovada para o feriado de hoje!');</script>";
                    return RedirectToAction(nameof(MeuPontoEletronico));
                }
            }

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

        [Authorize(Roles = "Supervisor, Analista")]
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
                        "PontoEletronico.xlsx");
                }
            }
        }

        [Authorize(Roles = "Supervisor, Analista")]
        // GET: AjustePontoEletronicos/SolicitarAjustePonto
        public IActionResult SolicitarAjustePonto()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        [Authorize(Roles = "Supervisor, Analista")]
        // POST: AjustePontoEletronicos/SolicitarAjustePonto
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SolicitarAjustePonto([Bind("CodigoAjuste,UsuarioId,DataAjuste,HoraPrimeiraEntrada,HoraPrimeiraSaida,HoraSegundaEntrada,HoraSegundaSaida,Descricao,SituacaoAjuste,Observacoes,Justificativa")] AjustePontoEletronico ajustePontoEletronico)
        {
            DateTime hoje = DateTime.Now.Date;
            DateTime dataAjuste = ajustePontoEletronico.DataAjuste;
            DateTime primeiraEntrada = ajustePontoEletronico.HoraPrimeiraEntrada;
            DateTime primeiraSaida = ajustePontoEletronico.HoraPrimeiraSaida;
            DateTime segundaEntrada = ajustePontoEletronico.HoraSegundaEntrada;
            DateTime segundaSaida = ajustePontoEletronico.HoraSegundaSaida;
            string codigoUsuario = userManager.GetUserId(User);

            if (dataAjuste >= hoje)
            {
                TempData["msgRegistroPonto"] = "<script>alert('O ajuste deve ser inferior ao dia de hoje!');</script>";
            }
            else if (ComparaHoras(primeiraEntrada, primeiraSaida, segundaEntrada, segundaSaida))
            {
                TempData["msgRegistroPonto"] = "<script>alert('Verifique o intervalo de horas informado!');</script>";
            }
            else
            {
                var feriadoExistente = await _context.Feriado
                .FirstOrDefaultAsync(m => m.Data.Date == dataAjuste);

                if (feriadoExistente != null)
                {
                    var usuarioAtualizacao = _context.Users.FirstOrDefault(u => u.Id == codigoUsuario);
                    var solicitacaoJornada = await _context.SolicitacaoJornada
                        .FirstOrDefaultAsync(s => s.CodigoFeriado == feriadoExistente.CodigoFeriado
                        && s.CodigoUsuarioJornada == usuarioAtualizacao.CodigoExterno
                        && s.SituacaoSolicitacao == Dominio.SituacaoSolicitacaoJornada.Aprovada);

                    if (solicitacaoJornada == null)
                    {
                        TempData["msgRegistroPonto"] = "<script>alert('A data do ajuste coincidiu com um feriado e você não possui uma jornada permida para esta data!');</script>";
                        return RedirectToAction(nameof(AjustesPonto));
                    }
                }

                
                var ajustesPontoEletronicoExistente = await _context.AjustePontoEletronico
                .FirstOrDefaultAsync(m => m.UsuarioId == codigoUsuario &&
                                     m.DataAjuste == dataAjuste &&
                                     m.SituacaoAjuste == Dominio.SituacaoAjustePonto.Pendente);
                if (ajustesPontoEletronicoExistente == null)
                {
                    ajustePontoEletronico.HoraPrimeiraEntrada = ConstruirDataHoraPonto(dataAjuste, primeiraEntrada);
                    ajustePontoEletronico.HoraPrimeiraSaida = ConstruirDataHoraPonto(dataAjuste, primeiraSaida);
                    ajustePontoEletronico.HoraSegundaEntrada = ConstruirDataHoraPonto(dataAjuste, segundaEntrada);
                    ajustePontoEletronico.HoraSegundaSaida = ConstruirDataHoraPonto(dataAjuste, segundaSaida);
                    ajustePontoEletronico.SituacaoAjuste = Dominio.SituacaoAjustePonto.Pendente;
                    ajustePontoEletronico.UsuarioId = codigoUsuario;
                    ajustePontoEletronico.Observacoes = " ";
                    _context.Add(ajustePontoEletronico);
                    await _context.SaveChangesAsync();
                    TempData["msgRegistroPonto"] = "<script>alert('Solicitiação de ajuste criada com sucesso!');</script>";
                    return RedirectToAction(nameof(AjustesPonto));
                }
                else
                {
                    TempData["msgRegistroPonto"] = "<script>alert('Já existe uma solicitiação de ajuste pendente para esta data!');</script>";
                }
            }
            return RedirectToAction(nameof(AjustesPonto));
        }

        public bool ComparaHoras(DateTime primeiraHora, DateTime segundaHora, DateTime terceiraHora, DateTime quartaHora)
        {
            return !((primeiraHora < segundaHora) && (segundaHora < terceiraHora) && (terceiraHora < quartaHora));
        }

        public DateTime ConstruirDataHoraPonto(DateTime data, DateTime horaDataInformada)
        {
            int hora = horaDataInformada.Hour;
            int minuto = horaDataInformada.Minute;

            data = data.AddHours(Convert.ToDouble(hora));
            data = data.AddMinutes(Convert.ToDouble(minuto));
            return data;
        }

        [Authorize(Roles = "Supervisor, Analista")]
        // GET: PontoEletronicos/DetalhesAjuste/5
        public async Task<IActionResult> DetalhesAjuste(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string codigoUsuario = userManager.GetUserId(User);
            var ajustePontoEletronico = await _context.AjustePontoEletronico
                .FirstOrDefaultAsync(m => m.CodigoAjuste == id && m.UsuarioId == codigoUsuario);
            if (ajustePontoEletronico == null)
            {
                TempData["msgRegistroPonto"] = "<script>alert('Operação Inválida!');</script>";
                return RedirectToAction(nameof(AjustesPonto));
            }
            return View(ajustePontoEletronico);
        }

        [Authorize(Roles = "Administrador, Gerente, Supervisor")]
        public async Task<IActionResult> DetalhesAjustePendente(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string codigoUsuario = userManager.GetUserId(User);
            
            if (User.IsInRole("Administrador") || User.IsInRole("Gerente"))
            {
                var ajustePontoEletronico = await _context.AjustePontoEletronico.Include(p => p.ApplicationUser)
                    .FirstOrDefaultAsync(m => m.CodigoAjuste == id && m.UsuarioId != codigoUsuario 
                                        && m.SituacaoAjuste == Dominio.SituacaoAjustePonto.Pendente);
                if (ajustePontoEletronico == null)
                {
                    TempData["msgRegistroPonto"] = "<script>alert('Operação Inválida!');</script>";
                    return RedirectToAction(nameof(AjustesPonto));
                }

                var roleSupervisor = roleManager.Roles.Where(r3 => r3.Name == "Supervisor").FirstOrDefault();
                var userRoleSupervisor = _context.UserRoles.Where(u => u.RoleId == roleSupervisor.Id && u.UserId == ajustePontoEletronico.UsuarioId).FirstOrDefault();
                if (userRoleSupervisor == null)
                {
                    TempData["msgRegistroPonto"] = "<script>alert('Operação Inválida!');</script>";
                    return RedirectToAction(nameof(AjustesPonto));
                }

                return View(ajustePontoEletronico);
            }
            else
            {
                var ajustePontoEletronico = await _context.AjustePontoEletronico.Include(p => p.ApplicationUser)
                    .FirstOrDefaultAsync(m => m.CodigoAjuste == id && m.UsuarioId != codigoUsuario
                                        && m.SituacaoAjuste == Dominio.SituacaoAjustePonto.Pendente);
                if (ajustePontoEletronico == null)
                {
                    TempData["msgRegistroPonto"] = "<script>alert('Operação Inválida!');</script>";
                    return RedirectToAction(nameof(AjustesPonto));
                }

                var roleAnalista = roleManager.Roles.Where(r3 => r3.Name == "Analista").FirstOrDefault();
                var userRoleAnalista = _context.UserRoles.Where(u => u.RoleId == roleAnalista.Id && u.UserId == ajustePontoEletronico.UsuarioId).FirstOrDefault();
                if (userRoleAnalista == null)
                {
                    TempData["msgRegistroPonto"] = "<script>alert('Operação Inválida!');</script>";
                    return RedirectToAction(nameof(AjustesPonto));
                }

                return View(ajustePontoEletronico);
            }
        }

        [Authorize(Roles = "Gerente, Supervisor")]
        public  async Task<IActionResult> AprovarReprovarAjuste(int ? id, string observacao, string situacao)
        {
            if (id == null)
            {
                TempData["msgRegistroPonto"] = "<script>alert('Operação Inválida!');</script>";
                return RedirectToAction(nameof(AjustesPendentes));
            }

            if (string.IsNullOrEmpty(situacao))
            {
                TempData["msgRegistroPonto"] = "<script>alert('Operação Inválida!');</script>";
                return RedirectToAction(nameof(AjustesPendentes));
            }

            var ajustePontoEletronico = await _context.AjustePontoEletronico
                    .FirstOrDefaultAsync(m => m.CodigoAjuste == id && m.SituacaoAjuste == Dominio.SituacaoAjustePonto.Pendente);
            if (ajustePontoEletronico == null)
            {
                TempData["msgRegistroPonto"] = "<script>alert('Operação Inválida!');</script>";
                return RedirectToAction(nameof(AjustesPendentes));
            }

            string codigoUsuario = userManager.GetUserId(User);
            var usuarioAtualizacao = _context.Users.FirstOrDefault(u => u.Id == codigoUsuario);
            if (situacao == "Reprovar")
            {
                ajustePontoEletronico.Observacoes = observacao;
                ajustePontoEletronico.CodigoUsuarioAprovador = usuarioAtualizacao.CodigoExterno;
                ajustePontoEletronico.SituacaoAjuste = Dominio.SituacaoAjustePonto.Reprovada;

                _context.Update(ajustePontoEletronico);
                await _context.SaveChangesAsync();

                TempData["msgRegistroPonto"] = "<script>alert('Solicitação Reprovada com Sucesso!');</script>";
                return RedirectToAction(nameof(AjustesPendentes));
            }
            else if (situacao == "Aprovar")
            {
                PontoEletronico pontoEletronicoExistente = _context.PontoEletronico
                    .FirstOrDefault(m => m.UserId == ajustePontoEletronico.UsuarioId
                                    && m.Data == ajustePontoEletronico.DataAjuste.ToShortDateString());

                if (pontoEletronicoExistente == null)
                {
                    PontoEletronico ponto = new PontoEletronico();
                    ponto.UserId= ajustePontoEletronico.UsuarioId;
                    ponto.DataHoraPrimeiraEntrada = ajustePontoEletronico.HoraPrimeiraEntrada;
                    ponto.DataHoraPrimeiraSaida = ajustePontoEletronico.HoraPrimeiraSaida;
                    ponto.DataHoraSegundaEntrada = ajustePontoEletronico.HoraSegundaEntrada;
                    ponto.DataHoraSegundaSaida = ajustePontoEletronico.HoraSegundaSaida;

                    _context.Add(ponto);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    pontoEletronicoExistente.DataHoraPrimeiraEntrada = ajustePontoEletronico.HoraPrimeiraEntrada;
                    pontoEletronicoExistente.DataHoraPrimeiraSaida = ajustePontoEletronico.HoraPrimeiraSaida;
                    pontoEletronicoExistente.DataHoraSegundaEntrada = ajustePontoEletronico.HoraSegundaEntrada;
                    pontoEletronicoExistente.DataHoraSegundaSaida = ajustePontoEletronico.HoraSegundaSaida;

                    _context.Update(pontoEletronicoExistente);
                    await _context.SaveChangesAsync();
                }
                ajustePontoEletronico.Observacoes = !string.IsNullOrEmpty(observacao) ? observacao : "";
                ajustePontoEletronico.CodigoUsuarioAprovador = usuarioAtualizacao.CodigoExterno;
                ajustePontoEletronico.SituacaoAjuste = Dominio.SituacaoAjustePonto.Aprovada;

                _context.Update(ajustePontoEletronico);
                await _context.SaveChangesAsync();

                TempData["msgRegistroPonto"] = "<script>alert('Solicitação Aprovada Com Sucesso!');</script>";
                return RedirectToAction(nameof(AjustesPendentes));
            }

            TempData["msgRegistroPonto"] = "<script>alert('Operação Inválida!');</script>";
            return RedirectToAction(nameof(AjustesPendentes));
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

        [Authorize(Roles = "Administrador, Supervisor, Analista")]
        public IActionResult ExportarExcelAjustes()
        {
            string codigoUsuario = userManager.GetUserId(User);

            var ajustePontoEletronicos = from p in _context.AjustePontoEletronico
                                         select p;
            ajustePontoEletronicos = ajustePontoEletronicos.Where(u => u.UsuarioId == codigoUsuario);
            
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("AjustesSolicitados");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "DataAjuste";
                worksheet.Cell(currentRow, 2).Value = "PrimeiraEntrada";
                worksheet.Cell(currentRow, 3).Value = "PrimeiraSaida";
                worksheet.Cell(currentRow, 4).Value = "SegundaEntrada";
                worksheet.Cell(currentRow, 5).Value = "SegundaSaida";
                worksheet.Cell(currentRow, 6).Value = "Justificativa";
                worksheet.Cell(currentRow, 7).Value = "SituacaoAjuste";
                worksheet.Cell(currentRow, 8).Value = "Observacoes";
                worksheet.Cell(currentRow, 9).Value = "CodigoUsuarioAprovador";


                foreach (var ajuste in ajustePontoEletronicos)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = ajuste.DataAjuste;
                    worksheet.Cell(currentRow, 2).Value = ajuste.HoraPrimeiraEntrada;
                    worksheet.Cell(currentRow, 3).Value = ajuste.HoraPrimeiraSaida;
                    worksheet.Cell(currentRow, 4).Value = ajuste.HoraSegundaEntrada;
                    worksheet.Cell(currentRow, 5).Value = ajuste.HoraSegundaSaida;
                    worksheet.Cell(currentRow, 6).Value = ajuste.Justificativa;
                    worksheet.Cell(currentRow, 7).Value = ajuste.SituacaoAjuste;
                    worksheet.Cell(currentRow, 8).Value = ajuste.Observacoes;
                    worksheet.Cell(currentRow, 9).Value = ajuste.CodigoUsuarioAprovador;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "AjustesSolicitados.xlsx");
                }
            }
        }

        [Authorize(Roles = "Administrador, Supervisor, Gerente")]
        public IActionResult ExportarExcelAjustesPendentes()
        {
            string codigoUsuario = userManager.GetUserId(User);

            var ajustePontoEletronicos = from p in _context.AjustePontoEletronico.Include(p => p.ApplicationUser)
                             .Where(u => u.SituacaoAjuste == Dominio.SituacaoAjustePonto.Pendente && u.UsuarioId != codigoUsuario)
                                         join ur in _context.UserRoles on p.ApplicationUser.Id equals ur.UserId
                                         join r in _context.Roles on ur.RoleId equals r.Id
                                         join r2 in _context.Roles on
                                         new { r.Id, r.Name }
                                                equals new { r2.Id, Name = "Supervisor" }
                                         select p;

            if (User.IsInRole("Supervisor"))
            {
                ajustePontoEletronicos = from p in _context.AjustePontoEletronico.Include(p => p.ApplicationUser)
                                             .Where(u => u.SituacaoAjuste == Dominio.SituacaoAjustePonto.Pendente && u.UsuarioId != codigoUsuario)
                                             join ur in _context.UserRoles on p.ApplicationUser.Id equals ur.UserId
                                             join r in _context.Roles on ur.RoleId equals r.Id
                                             join r2 in _context.Roles on
                                             new { r.Id, r.Name }
                                                    equals new { r2.Id, Name = "Analista" }
                                             select p;

            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("AjustesSolicitados");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Usuario";
                worksheet.Cell(currentRow, 2).Value = "DataAjuste";
                worksheet.Cell(currentRow, 3).Value = "PrimeiraEntrada";
                worksheet.Cell(currentRow, 4).Value = "PrimeiraSaida";
                worksheet.Cell(currentRow, 5).Value = "SegundaEntrada";
                worksheet.Cell(currentRow, 6).Value = "SegundaSaida";
                worksheet.Cell(currentRow, 7).Value = "Justificativa";
                worksheet.Cell(currentRow, 8).Value = "SituacaoAjuste";


                foreach (var ajuste in ajustePontoEletronicos)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = ajuste.ApplicationUser.CodigoExterno + " - " + ajuste.ApplicationUser.NomeUsuario;
                    worksheet.Cell(currentRow, 2).Value = ajuste.DataAjuste;
                    worksheet.Cell(currentRow, 3).Value = ajuste.HoraPrimeiraEntrada;
                    worksheet.Cell(currentRow, 4).Value = ajuste.HoraPrimeiraSaida;
                    worksheet.Cell(currentRow, 5).Value = ajuste.HoraSegundaEntrada;
                    worksheet.Cell(currentRow, 6).Value = ajuste.HoraSegundaSaida;
                    worksheet.Cell(currentRow, 7).Value = ajuste.Justificativa;
                    worksheet.Cell(currentRow, 8).Value = ajuste.SituacaoAjuste;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "AjustesPendentes.xlsx");
                }
            }
        }
    }
}
