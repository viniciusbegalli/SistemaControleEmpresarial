using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaControleEmpresarial.Data;
using SistemaControleEmpresarial.Models;

namespace SistemaControleEmpresarial.Controllers
{
    public class TreinamentosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public TreinamentosController(ApplicationDbContext context, 
                                      RoleManager<IdentityRole> roleManager, 
                                      UserManager<ApplicationUser> _userManager)
        {
            _context = context;
            this.roleManager = roleManager;
            userManager = _userManager;
        }

        // GET: Treinamentos
        public async Task<IActionResult> ListaTreinamentos()
        {
            HttpContext.Session.SetString("paramListaCodigosInstrutores", string.Empty);
            var treinamentos = _context.Treinamento.Include(t => t.ApplicationUser).OrderByDescending(t => t.DataInicioTreinamento);
            if (!(User.IsInRole("Administrador") || User.IsInRole("Gerente")))
            {
                var treinamentosPublico = _context.Treinamento.Include(t => t.ApplicationUser)
                    .Where(t => t.DataInicioTreinamento >= DateTime.Now.Date && t.SituacaoTreinamento == Dominio.SituacaoTreinamento.Ativo)
                    .OrderByDescending(t => t.DataInicioTreinamento);
                return View(await treinamentosPublico.ToListAsync());
            }
            return View(await treinamentos.ToListAsync());
        }

        // GET: Treinamentos/DetalhesTreinamento/5
        public async Task<IActionResult> DetalhesTreinamento(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treinamento = await _context.Treinamento
                .Include(t => t.ApplicationUser)
                .Include(t => t.InstrutoresTreinamento)
                .FirstOrDefaultAsync(m => m.CodigoTreinamento == id);
            if (treinamento == null)
            {
                return NotFound();
            }

            return View(treinamento);
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // GET: Treinamentos/CriarTreinamento
        public IActionResult CriarTreinamento()
        {
            HttpContext.Session.SetString("paramListaCodigosInstrutores", string.Empty);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Treinamentos/CriarTreinamento
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador, Gerente")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarTreinamento([Bind("Titulo,Descricao,DataInicioTreinamento,DataFimTreinamento,HoraInicioTreinamento,HoraFimTreinamento")] Treinamento treinamento)
        {
            string codigoUsuario = userManager.GetUserId(User);
            DateTime dataHoraCriacao = DateTime.Now;
            DateTime hoje = DateTime.Now.Date;

            if (treinamento.DataInicioTreinamento > treinamento.DataFimTreinamento)
            {
                TempData["msgCriacaoTreinamento"] = "<script>alert('A data de término, deve ser maior ou igual a data de início.');</script>";
            }
            else if (treinamento.DataInicioTreinamento <= hoje)
            {
                TempData["msgCriacaoTreinamento"] = "<script>alert('O treinamento deve ter início em uma data FUTURA.');</script>";
            }
            else if (treinamento.HoraInicioTreinamento >= treinamento.HoraFimTreinamento)
            {
                TempData["msgCriacaoTreinamento"] = "<script>alert('A hora de término deve ser maior que a hora de início.');</script>";
            }
            else
            {
                _context.Add(treinamento);
                _context.SaveChanges();
            }
            



            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", treinamento.UserId);
            return View(treinamento);
        }

        // GET: Vagas
        public async Task<IActionResult> ParticiparTreinamento(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treinamento = await _context.Treinamento.FindAsync(id);
            if (treinamento == null)
            {
                return NotFound();
            }

            TreinamentoParticipante treinamentoParticipante = new TreinamentoParticipante();
            treinamentoParticipante.CodigoTreinamento = treinamento.CodigoTreinamento;
            return View(treinamentoParticipante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ParticiparTreinamento([Bind("CodigoTreinamento,Nome,Email")] TreinamentoParticipante treinamentoParticipante)
        {
            var treinamentoParticipanteExistente = _context.TreinamentoParticipante.FirstOrDefault(t => t.CodigoTreinamento == treinamentoParticipante.CodigoTreinamento && t.Email == treinamentoParticipante.Email);

            if (treinamentoParticipanteExistente != null)
            {
                TempData["msgListaTreinamentos"] = "<script>alert('E-mail já inscrito para este treinamento!');</script>";
                return RedirectToAction(nameof(ListaTreinamentos));
            }

            _context.Add(treinamentoParticipante);
            await _context.SaveChangesAsync();

            TempData["msgListaTreinamentos"] = "<script>alert('Inscrição Enviada Com Sucesso!');</script>";

            return RedirectToAction(nameof(ListaTreinamentos));
        }

        [Authorize(Roles = "Administrador, Gerente")]
        // GET: Treinamentos/EditarTreinamento/5
        public async Task<IActionResult> EditarTreinamento(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treinamento = await _context.Treinamento.FindAsync(id);
            if (treinamento == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", treinamento.UserId);
            return View(treinamento);
        }

        // POST: Treinamentos/EditarTreinamento/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador, Gerente")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarTreinamento(int id, [Bind("CodigoTreinamento,Titulo,Descricao,DataInicioTreinamento,DataFimTreinamento,HoraInicioTreinamento,HoraFimTreinamento,UserId,DataCriacao,SituacaoTreinamento,Observacoes")] Treinamento treinamento)
        {
            if (id != treinamento.CodigoTreinamento)
            {
                return NotFound();
            }

            try
            {
                DateTime hoje = DateTime.Now.Date;
                if (treinamento.DataInicioTreinamento > treinamento.DataFimTreinamento)
                {
                    TempData["msgEditaTreinamentos"] = "<script>alert('A data de término, deve ser maior ou igual a data de início.');</script>";
                    return View(treinamento);
                }
                else if (treinamento.DataInicioTreinamento <= hoje)
                {
                    TempData["msgEditaTreinamentos"] = "<script>alert('O treinamento deve ter início em uma data FUTURA.');</script>";
                    return View(treinamento);
                }
                else if (treinamento.HoraInicioTreinamento >= treinamento.HoraFimTreinamento)
                {
                    TempData["msgEditaTreinamentos"] = "<script>alert('A hora de término deve ser maior que a hora de início.');</script>";
                    return View(treinamento);
                }

                var treinamentoAtualizacao = _context.Treinamento.FirstOrDefault(u => u.CodigoTreinamento == id);
                treinamentoAtualizacao.Titulo = treinamento.Titulo;
                treinamentoAtualizacao.Descricao = treinamento.Descricao;
                treinamentoAtualizacao.DataInicioTreinamento = treinamento.DataInicioTreinamento;
                treinamentoAtualizacao.DataFimTreinamento = treinamento.DataFimTreinamento;
                treinamentoAtualizacao.HoraInicioTreinamento = treinamento.HoraInicioTreinamento;
                treinamentoAtualizacao.HoraFimTreinamento = treinamento.HoraFimTreinamento;
                _context.Update(treinamentoAtualizacao);
                await _context.SaveChangesAsync();

                TempData["msgEditaTreinamentos"] = "<script>alert('Treinamento Alterado Com Sucesso.');</script>";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreinamentoExists(treinamento.CodigoTreinamento))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return View(treinamento);
        }

        // GET: Treinamentos/EditarTreinamentoInstrutores/5
        [Authorize(Roles = "Administrador, Gerente")]
        public async Task<IActionResult> EditarTreinamentoInstrutores(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treinamentoInstrutores = await _context.TreinamentoInstrutor.FindAsync(id);
            if (treinamentoInstrutores == null)
            {
                return NotFound();
            }
            return View(treinamentoInstrutores);
        }


        // GET: Treinamentos/ExcluirTreinamento/5
        [Authorize(Roles = "Administrador, Gerente")]
        public async Task<IActionResult> ExcluirTreinamento(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treinamento = await _context.Treinamento
                .Include(t => t.ApplicationUser)
                .Include(t => t.InstrutoresTreinamento)
                .FirstOrDefaultAsync(m => m.CodigoTreinamento == id);
            if (treinamento == null)
            {
                return NotFound();
            }

            return View(treinamento);
        }

        // POST: Treinamentos/ExcluirTreinamento/5
        [Authorize(Roles = "Administrador, Gerente")]
        [HttpPost, ActionName("ExcluirTreinamento")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var treinamento = await _context.Treinamento.FindAsync(id);

            var treinamentoParticipantes = _context.TreinamentoParticipante
                .Include(t => t.Treinamento)
                .Where(u => u.CodigoTreinamento == treinamento.CodigoTreinamento);

            if (treinamentoParticipantes != null && treinamentoParticipantes.Count() > 0)
            {
                TempData["msgListaTreinamentos"] = "<script>alert('Não é possível excluir este treinamento, pois já existem participantes inscritos.');</script>";
                return RedirectToAction(nameof(ListaTreinamentos));
            }
            _context.Treinamento.Remove(treinamento);
            await _context.SaveChangesAsync();
            TempData["msgListaTreinamentos"] = "<script>alert('Treinamento Excluído Com Sucesso.');</script>";
            return RedirectToAction(nameof(ListaTreinamentos));
        }

        [Authorize(Roles = "Administrador, Gerente")]
        public async Task<IActionResult> CancelarTreinamento(int id)
        {
            var treinamento = await _context.Treinamento.FindAsync(id);
            DateTime hoje = DateTime.Now.Date;
            if (treinamento.DataInicioTreinamento < hoje)
            {
                TempData["msgListaTreinamentos"] = "<script>alert('Somente é possível cancelar treinamentos futuros.');</script>";
                return RedirectToAction(nameof(ListaTreinamentos));
            }

            treinamento.SituacaoTreinamento = Dominio.SituacaoTreinamento.Cancelado;
            await _context.SaveChangesAsync();
            TempData["msgListaTreinamentos"] = "<script>alert('Treinamento Cancelado Com Sucesso.');</script>";
            return RedirectToAction(nameof(ListaTreinamentos));
        }

        [Authorize(Roles = "Administrador, Gerente")]
        public async Task<IActionResult> ReativarTreinamento(int id)
        {
            var treinamento = await _context.Treinamento.FindAsync(id);
            DateTime hoje = DateTime.Now.Date;
            if (treinamento.DataInicioTreinamento < hoje)
            {
                TempData["msgListaTreinamentos"] = "<script>alert('Somente é possível reativar treinamentos futuros.');</script>";
                return RedirectToAction(nameof(ListaTreinamentos));
            }
            treinamento.SituacaoTreinamento = Dominio.SituacaoTreinamento.Ativo;
            await _context.SaveChangesAsync();
            TempData["msgListaTreinamentos"] = "<script>alert('Treinamento Reativado Com Sucesso.');</script>";
            return RedirectToAction(nameof(ListaTreinamentos));
        }

        private bool TreinamentoExists(int id)
        {
            return _context.Treinamento.Any(e => e.CodigoTreinamento == id);
        }

        public JsonResult GetDadosInstrutor(int id)
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
        public IActionResult SetInstrutor(int id)
        {
            string msgCriacaoTreinamento = string.Empty;
            var usuario = _context.Users.Where(u => u.CodigoExterno == id).FirstOrDefault();

            if (usuario == null)
            {
                msgCriacaoTreinamento = "Código Inválido.";
            }
            else
            {
                TreinamentoInstrutor instrutor = new TreinamentoInstrutor();
             //   instrutor.UserId = usuario.Id;
            }
            /*else
            {
                string listaCodigoInstrutores = HttpContext.Session.GetString("paramListaCodigosInstrutores");
                if (!string.IsNullOrEmpty(listaCodigoInstrutores) && listaCodigoInstrutores.Contains(id.ToString()))
                {
                    msgCriacaoTreinamento = "Instrutor Já Adicionado.";
                }
                else
                {
                    listaCodigoInstrutores += id.ToString() + ";";
                    HttpContext.Session.SetString("paramListaCodigosInstrutores", listaCodigoInstrutores);
                }
            }*/
            return Ok(msgCriacaoTreinamento);
        }

        [HttpPost]
        public IActionResult SalvaTreinamento(string CodigoTreinamento, string Titulo, string Descricao, string DataInicioTreinamento, string DataFimTreinamento, string HoraInicioTreinamento, string HoraFimTreinamento)
        {
            Treinamento treinamento = new Treinamento();
            if (string.IsNullOrEmpty(Titulo) || string.IsNullOrEmpty(Descricao) || string.IsNullOrEmpty(DataInicioTreinamento) || string.IsNullOrEmpty(DataFimTreinamento) || string.IsNullOrEmpty(HoraInicioTreinamento) || string.IsNullOrEmpty(HoraFimTreinamento))
            {
                TempData["msgCriacaoTreinamento"] = "<script>alert('Preencha todos os campos.');</script>";
            }
            else
            {
                string codigoUsuario = userManager.GetUserId(User);
                DateTime dataHoraCriacao = DateTime.Now;
                DateTime hoje = DateTime.Now.Date;

                if (string.IsNullOrEmpty(CodigoTreinamento))
                {
                    treinamento.Titulo = Titulo;
                    treinamento.Descricao = Descricao;
                    treinamento.DataInicioTreinamento = DateTime.Parse(DataInicioTreinamento);
                    treinamento.DataFimTreinamento = DateTime.Parse(DataFimTreinamento);
                    treinamento.HoraInicioTreinamento = DateTime.Parse(HoraInicioTreinamento);
                    treinamento.HoraFimTreinamento = DateTime.Parse(HoraFimTreinamento);

                    if (treinamento.DataInicioTreinamento > treinamento.DataFimTreinamento)
                    {
                        TempData["msgCriacaoTreinamento"] = "<script>alert('A data de término, deve ser maior ou igual a data de início.');</script>";
                    }
                    else if (treinamento.DataInicioTreinamento <= hoje)
                    {
                        TempData["msgCriacaoTreinamento"] = "<script>alert('O treinamento deve ter início em uma data FUTURA.');</script>";
                    }
                    else if (treinamento.HoraInicioTreinamento >= treinamento.HoraFimTreinamento)
                    {
                        TempData["msgCriacaoTreinamento"] = "<script>alert('A hora de término deve ser maior que a hora de início.');</script>";
                    }
                    else
                    {
                        treinamento.UserId = codigoUsuario;
                        treinamento.DataCriacao = DateTime.Now;
                        treinamento.SituacaoTreinamento = Dominio.SituacaoTreinamento.Ativo;
                        _context.Add(treinamento);
                        _context.SaveChanges();

                        ViewBag.Treinamento = treinamento.CodigoTreinamento;
                    }
                }
                else
                {
                    var treinamentoAtualizacao = _context.Treinamento.FirstOrDefault(u => u.CodigoTreinamento == int.Parse(CodigoTreinamento));
                    treinamentoAtualizacao.Titulo = Titulo;
                    treinamentoAtualizacao.Descricao = Descricao;
                    treinamentoAtualizacao.DataInicioTreinamento = DateTime.Parse(DataInicioTreinamento);
                    treinamentoAtualizacao.DataFimTreinamento = DateTime.Parse(DataFimTreinamento);
                    treinamentoAtualizacao.HoraInicioTreinamento = DateTime.Parse(HoraInicioTreinamento);
                    treinamentoAtualizacao.HoraFimTreinamento = DateTime.Parse(HoraFimTreinamento);
                    _context.Update(treinamentoAtualizacao);
                    _context.SaveChanges();
                }
            }
            return Ok(treinamento.CodigoTreinamento);
        }

        [HttpPost]
        public IActionResult SalvaInstrutor(string idInstrutor, string idTreinamento)
        {
            string codigoUsuario = userManager.GetUserId(User);
            DateTime dataHoraCriacao = DateTime.Now;
            DateTime hoje = DateTime.Now.Date;

            var treinamento = _context.Treinamento.Where(u => u.CodigoTreinamento == int.Parse(idTreinamento));

            if (treinamento != null)
            {
                var usuarioAtualizacao = _context.Users.FirstOrDefault(u => u.CodigoExterno == int.Parse(idInstrutor));
                var treinamentoInstrutorExistente = _context.TreinamentoInstrutor.FirstOrDefault(t => t.CodigoTreinamento == int.Parse(idTreinamento) && t.CodigoExterno == usuarioAtualizacao.CodigoExterno);

                if (treinamentoInstrutorExistente == null)
                {
                    TreinamentoInstrutor treinamentoInstrutor = new TreinamentoInstrutor();
                    treinamentoInstrutor.CodigoTreinamento = int.Parse(idTreinamento);
                    treinamentoInstrutor.UserId = usuarioAtualizacao.Id;
                    treinamentoInstrutor.CodigoExterno = usuarioAtualizacao.CodigoExterno;
                    treinamentoInstrutor.NomeInstrutor = usuarioAtualizacao.NomeUsuario;
                    _context.Add(treinamentoInstrutor);
                    _context.SaveChanges();
                }
                ViewBag.Treinamento = int.Parse(idTreinamento);
            }
            return Ok(int.Parse(idTreinamento));
        }

        [HttpPost]
        public IActionResult RemoveInstrutor(string idInstrutor, string idTreinamento)
        {
            string codigoUsuario = userManager.GetUserId(User);
            DateTime dataHoraCriacao = DateTime.Now;
            DateTime hoje = DateTime.Now.Date;

            var treinamento = _context.Treinamento.Where(u => u.CodigoTreinamento == int.Parse(idTreinamento));

            if (treinamento != null)
            {

                var usuarioAtualizacao = _context.Users.FirstOrDefault(u => u.CodigoExterno == int.Parse(idInstrutor));
                var treinamentoInstrutor = _context.TreinamentoInstrutor.FirstOrDefault(t => t.CodigoTreinamento == int.Parse(idTreinamento) && t.CodigoExterno == usuarioAtualizacao.CodigoExterno);
                
                if (treinamentoInstrutor != null)
                {
                    _context.Remove(treinamentoInstrutor);
                    _context.SaveChanges();
                }
                
                ViewBag.Treinamento = int.Parse(idTreinamento);
            }
            return Ok(int.Parse(idTreinamento));
        }

        public ActionResult ListaInstrutores(int id)
        {
            var lista = _context.TreinamentoInstrutor.Where(u => u.CodigoTreinamento == id).Include(u => u.Treinamento).Include(u => u.Instrutor);

            ViewBag.Treinamento = id;
            return PartialView(lista);
        }

        [HttpPost]
        public IActionResult RemoveInstrutor1(int id)
        {
            string msgCriacaoTreinamento = string.Empty;
            var usuario = _context.Users.Where(u => u.CodigoExterno == id).FirstOrDefault();

            if (usuario == null)
            {
                msgCriacaoTreinamento = "Código Inválido.";
            }
            else
            {
                string listaCodigoInstrutores = HttpContext.Session.GetString("paramListaCodigosInstrutores");

                if (!string.IsNullOrEmpty(listaCodigoInstrutores) && !listaCodigoInstrutores.Contains(id.ToString()))
                {
                    msgCriacaoTreinamento = "Operação Inválida.";
                }
                else
                {
                    string novaLista = string.Empty;

                    foreach (string valor in listaCodigoInstrutores.Split(";"))
                    {
                        if (valor != id.ToString())
                        {
                            novaLista += valor + ";";
                        }
                    }
                    HttpContext.Session.SetString("paramListaCodigosInstrutores", novaLista);
                }
            }
            return Ok(msgCriacaoTreinamento);
        }

        public IActionResult ExportarExcel()
        {
            var treinamentos = _context.Treinamento
                .Include(t => t.ApplicationUser)
                .Include(t => t.InstrutoresTreinamento);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Feriados");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Codigo";
                worksheet.Cell(currentRow, 2).Value = "Titulo";
                worksheet.Cell(currentRow, 3).Value = "Descricao";
                worksheet.Cell(currentRow, 4).Value = "DataInicio";
                worksheet.Cell(currentRow, 5).Value = "DataFim";
                worksheet.Cell(currentRow, 6).Value = "HoraInicio";
                worksheet.Cell(currentRow, 7).Value = "HoraFim";
                worksheet.Cell(currentRow, 8).Value = "DataCriacao";
                worksheet.Cell(currentRow, 9).Value = "UsuarioCriacao";
                worksheet.Cell(currentRow, 10).Value = "Situacao";
                worksheet.Cell(currentRow, 11).Value = "Instrutores";

                foreach (var treinamento in treinamentos)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = treinamento.CodigoTreinamento;
                    worksheet.Cell(currentRow, 2).Value = treinamento.Titulo;
                    worksheet.Cell(currentRow, 3).Value = treinamento.Descricao;
                    worksheet.Cell(currentRow, 4).Value = treinamento.DataInicioTreinamento;
                    worksheet.Cell(currentRow, 5).Value = treinamento.DataFimTreinamento;
                    worksheet.Cell(currentRow, 6).Value = treinamento.HoraInicioTreinamento;
                    worksheet.Cell(currentRow, 7).Value = treinamento.HoraFimTreinamento;
                    worksheet.Cell(currentRow, 8).Value = treinamento.DataCriacao;
                    worksheet.Cell(currentRow, 9).Value = treinamento.ApplicationUser.CodigoExterno + " - " + treinamento.ApplicationUser.NomeUsuario;
                    worksheet.Cell(currentRow, 10).Value = treinamento.SituacaoTreinamento;

                    StringBuilder listaInstrutores = new StringBuilder();
                    if (treinamento.InstrutoresTreinamento != null && treinamento.InstrutoresTreinamento.Count() > 0)
                    {
                        int contador = 0;
                        int qtde = treinamento.InstrutoresTreinamento.Count();
                        foreach (var treinamentoInstrutor in treinamento.InstrutoresTreinamento)
                        {
                            contador++;
                            listaInstrutores.Append(treinamentoInstrutor.NomeInstrutor);
                            if (contador < qtde)
                            {
                                listaInstrutores.Append(" - ");
                            }
                        }
                    }
                    worksheet.Cell(currentRow, 11).Value = listaInstrutores.ToString();
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Treinamentos.xlsx");
                }
            }
        }

        public IActionResult ExportarInscritosExcel(int id)
        {
            var treinamentoParticipantes = _context.TreinamentoParticipante
                .Include(t => t.Treinamento)
                .Where(u => u.CodigoTreinamento == id);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("TreinamentoParticipantes");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Treinamento";
                worksheet.Cell(currentRow, 2).Value = "Nome";
                worksheet.Cell(currentRow, 3).Value = "Email";
                
                foreach (var treinamentoParticipante in treinamentoParticipantes)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = treinamentoParticipante.Treinamento.CodigoTreinamento + " - " + treinamentoParticipante.Treinamento.Descricao;
                    worksheet.Cell(currentRow, 2).Value = treinamentoParticipante.Nome;
                    worksheet.Cell(currentRow, 3).Value = treinamentoParticipante.Email;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "TreinamentoParticipantes.xlsx");
                }
            }
        }
    }
}
