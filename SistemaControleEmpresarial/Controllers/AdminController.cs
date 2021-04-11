using SistemaControleEmpresarial.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using ClosedXML.Excel;
using System.IO;
using SistemaControleEmpresarial.Data;
using Microsoft.AspNetCore.Http;

namespace SistemaControleEmpresarial.Controllers
{
    [Authorize(Roles = "Administrador, Gerente, Supervisor")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext _context;
        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> _userManager, ApplicationDbContext context)
        {
            this.roleManager = roleManager;
            userManager = _userManager;
            _context = context;
        }

        [Authorize(Roles = "Administrador, Gerente")]
        [HttpGet]
        public IActionResult CriarPerfil()
        {
            return View();
        }
        [Authorize(Roles = "Administrador, Gerente")]
        [HttpPost]
        public async Task<IActionResult> CriarPerfil(Role model)
        {
            if (ModelState.IsValid)
            {
                // precisamos apenas especificar o nome único da role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                // Salva a role na tabela AspNetRole
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    TempData["msgRetornosPerfil"] = "<script>alert('Perfil criado com sucesso.');</script>";
                    return RedirectToAction("ListaPerfis", "Admin");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Administrador, Gerente")]
        [HttpGet]
        public IActionResult ListaPerfis()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        public async Task<IActionResult> ListaUsuarios(string filtroCodigoUsuario, string filtroNomeUsuario)
        {
            bool ignorarFiltro = false;
            if (!string.IsNullOrEmpty(filtroCodigoUsuario) && !int.TryParse(filtroCodigoUsuario, out int codigoUsuarioFiltro))
            {
                TempData["msgListaUsuarios"] = "<script>alert('Filtro Inválido.');</script>";
                ignorarFiltro = true;
            }

            SetarParametrosDeConsulta(filtroCodigoUsuario, filtroNomeUsuario);

            if (User.IsInRole("Administrador") || User.IsInRole("Gerente"))
            {
                var usuarios = userManager.Users;
                if (!string.IsNullOrEmpty(filtroCodigoUsuario) && !ignorarFiltro)
                {
                    usuarios = usuarios.Where(u => u.CodigoExterno == int.Parse(filtroCodigoUsuario));
                }
                if (!string.IsNullOrEmpty(filtroNomeUsuario) && !ignorarFiltro)
                {
                    usuarios = usuarios.Where(u => u.NomeUsuario.Contains(filtroNomeUsuario));
                }
                return View(usuarios);
            }
            else
            {
                var usuarios = from u in userManager.Users
                               join ur in _context.UserRoles on u.Id equals ur.UserId
                               join r in _context.Roles on ur.RoleId equals r.Id
                               join r2 in _context.Roles on
                               new { r.Id, r.Name }
                                      equals new { r2.Id, Name = "Analista" }
                               select u;
                if (!string.IsNullOrEmpty(filtroCodigoUsuario) && !ignorarFiltro)
                {
                    usuarios = usuarios.Where(u => u.CodigoExterno == int.Parse(filtroCodigoUsuario));
                }
                if (!string.IsNullOrEmpty(filtroNomeUsuario) && !ignorarFiltro)
                {
                    usuarios = usuarios.Where(u => u.NomeUsuario.Contains(filtroNomeUsuario));
                }
                return View(usuarios);
            }
        }

        [Authorize(Roles = "Administrador, Gerente")]
        [HttpGet]
        public async Task<IActionResult> EditarPerfil(string id)
        {
            // Localiza a role pelo ID
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Perfil com Id = {id} não foi localizado.";
                return View("NotFound");
            }
            var model = new EditRole
            {
                Id = role.Id,
                RoleName = role.Name
            };
            var listaUsuarios = userManager.Users.ToList();
            // Retorna todos os usuários
            foreach (var user in listaUsuarios)
            {
                // Se o usuário existir na role, inclui o nome do usuário
                // para a propriedade Users de EditRoleViewModel
                // Este objeto model é então passado para ser exibido
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Administrador, Gerente")]
        [HttpPost]
        public async Task<IActionResult> EditarPerfil(EditRole model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Perfil com Id = {model.Id} não foi encontrado.";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                // Atualiza a role usando UpdateAsync
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListaPerfis");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [Authorize(Roles = "Administrador, Gerente")]
        [HttpGet]
        public async Task<IActionResult> EditarUsuariosPerfil(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Perfil com Id = {roleId} não foi encontrado.";
                return View("NotFound");
            }
            var model = new List<UserRole>();
            var listaUsuarios = userManager.Users.ToList();
            foreach (var user in listaUsuarios)
            {
                var userRoleViewModel = new UserRole
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [Authorize(Roles = "Administrador, Gerente")]
        [HttpPost]
        public async Task<IActionResult> EditarUsuariosPerfil(List<UserRole> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Perfil com Id = {roleId} não foi encontrado.";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    var userRoleExistente = from p in _context.UserRoles
                                           select p;
                    userRoleExistente = userRoleExistente.Where(ur => ur.UserId == user.Id);

                    if (userRoleExistente != null && userRoleExistente.Count() > 0)
                    {
                        TempData["msgErroAtribuicaoPerfil"] = "<script>alert('Usuário já pertencente à outro perfil!');</script>";
                        return RedirectToAction("EditarPerfil", new { Id = roleId });
                    }
                    
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditarPerfil", new { Id = roleId });
                }
            }
            return RedirectToAction("EditarPerfil", new { Id = roleId });
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> ExcluirPerfil(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Perfil com Id = {id} não foi encontrado";
                return View("NotFound");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    TempData["msgRetornosPerfil"] = "<script>alert('Perfil excluído com sucesso.');</script>";
                    return RedirectToAction("ListaPerfis");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListaPerfis");
            }
        }

        [Authorize(Roles = "Administrador, Gerente")]
        public IActionResult ExportarExcel()
        {
            var roles = roleManager.Roles;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ListaPerfis");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Perfil";

                foreach (var perfil in roles)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = perfil.Name;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "PerfisAcesso.xlsx");
                }
            }
        }

        // GET: PontoEletronicos/Edit/5
        public async Task<IActionResult> EditarUsuario(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = userManager.Users.FirstOrDefault(u => u.CodigoExterno == id);
            if (usuario == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Supervisor"))
            {
                var usuarios = from u in userManager.Users
                               join ur in _context.UserRoles on u.Id equals ur.UserId
                               join r in _context.Roles on ur.RoleId equals r.Id
                               join r2 in _context.Roles on
                               new { r.Id, r.Name }
                                      equals new { r2.Id, Name = "Analista" }
                               select u;
                usuarios = usuarios.Where(u => u.CodigoExterno == usuario.CodigoExterno);

                if (usuarios == null || usuarios.Count() == 0)
                {
                    TempData["msgListaUsuarios"] = "<script>alert('Operação Inválida.');</script>";
                    return RedirectToAction(nameof(ListaUsuarios));
                }
            }

            return View(usuario);
        }

        // POST: PontoEletronicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarUsuario(int Id, [Bind("CodigoExterno,NomeUsuario,CPF,Telefone")] ApplicationUser usuario)
        {
            if (string.IsNullOrEmpty(usuario.NomeUsuario))
            {
                TempData["msgEditaUsuarios"] = "<script>alert('Preencha o nome do usuário.');</script>";
                return View(usuario);
            }
            if (string.IsNullOrEmpty(usuario.CPF))
            {
                TempData["msgEditaUsuarios"] = "<script>alert('Preencha o CPF do usuário.');</script>";
                return View(usuario);
            }
            
            var usuarioAtualizacao = userManager.Users.FirstOrDefault(u => u.CodigoExterno == Id);

            if (User.IsInRole("Supervisor"))
            {
                var usuarios = from u in userManager.Users
                           join ur in _context.UserRoles on u.Id equals ur.UserId
                           join r in _context.Roles on ur.RoleId equals r.Id
                           join r2 in _context.Roles on
                           new { r.Id, r.Name }
                                  equals new { r2.Id, Name = "Analista" }
                           select u;
                usuarios = usuarios.Where(u => u.CodigoExterno == usuarioAtualizacao.CodigoExterno);

                if (usuarios == null || usuarios.Count() == 0)
                {
                    TempData["msgListaUsuarios"] = "<script>alert('Operação Inválida.');</script>";
                    return RedirectToAction(nameof(ListaUsuarios));
                }
            }

            usuarioAtualizacao.NomeUsuario = usuario.NomeUsuario;
            usuarioAtualizacao.Telefone = usuario.Telefone;
            usuarioAtualizacao.CPF = usuario.CPF;

            _context.Update(usuarioAtualizacao);
            await _context.SaveChangesAsync();

            TempData["msgListaUsuarios"] = "<script>alert('Dados Atualizados com Sucesso.');</script>";

            return RedirectToAction(nameof(ListaUsuarios));
        }

        public IActionResult ExportarUsuariosExcel()
        {
            string filtroCodigoUsuario;
            string filtroNomeUsuario;
            RecuperarParametrosConsulta(out filtroCodigoUsuario, out filtroNomeUsuario);

            var usuarios = userManager.Users;

            if (User.IsInRole("Administrador") || User.IsInRole("Gerente"))
            {
                if (!string.IsNullOrEmpty(filtroCodigoUsuario))
                {
                    usuarios = usuarios.Where(u => u.CodigoExterno == int.Parse(filtroCodigoUsuario));
                }
                if (!string.IsNullOrEmpty(filtroNomeUsuario))
                {
                    usuarios = usuarios.Where(u => u.NomeUsuario.Contains(filtroNomeUsuario));
                }
            }

            if (User.IsInRole("Supervisor"))
            {
                usuarios = from u in userManager.Users
                               join ur in _context.UserRoles on u.Id equals ur.UserId
                               join r in _context.Roles on ur.RoleId equals r.Id
                               join r2 in _context.Roles on
                               new { r.Id, r.Name }
                                      equals new { r2.Id, Name = "Analista" }
                               select u;
                if (!string.IsNullOrEmpty(filtroCodigoUsuario))
                {
                    usuarios = usuarios.Where(u => u.CodigoExterno == int.Parse(filtroCodigoUsuario));
                }
                if (!string.IsNullOrEmpty(filtroNomeUsuario))
                {
                    usuarios = usuarios.Where(u => u.NomeUsuario.Contains(filtroNomeUsuario));
                }
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Usuarios");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Codigo";
                worksheet.Cell(currentRow, 2).Value = "Nome";
                worksheet.Cell(currentRow, 3).Value = "CPF";
                worksheet.Cell(currentRow, 4).Value = "Telefone";
                worksheet.Cell(currentRow, 5).Value = "Email";

                foreach (var usuario in usuarios)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = usuario.CodigoExterno;
                    worksheet.Cell(currentRow, 2).Value = usuario.NomeUsuario;
                    worksheet.Cell(currentRow, 3).Value = usuario.CPF;
                    worksheet.Cell(currentRow, 4).Value = usuario.Telefone;
                    worksheet.Cell(currentRow, 5).Value = usuario.Email;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Usuarios.xlsx");
                }
            }
        }

        public IActionResult EspelhoPontoExcel(int id)
        {
            var usuario = userManager.Users.FirstOrDefault(u => u.CodigoExterno == id);

            var pontoEletronicos = from p in _context.PontoEletronico
                                   select p;
            pontoEletronicos = pontoEletronicos.Where(u => u.UserId == usuario.Id);
            pontoEletronicos = pontoEletronicos.OrderByDescending(u => u.DataHoraPrimeiraEntrada);

            if (pontoEletronicos == null || pontoEletronicos.Count() == 0)
            {
                TempData["msgListaUsuarios"] = "<script>alert('Este usuário não possui registros de ponto eletrônico.');</script>";
                return RedirectToAction(nameof(ListaUsuarios));
            }
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("EspelhoPonto_Usuario_Codigo_"+usuario.CodigoExterno.ToString());
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
                        "PontoEletronico_Usuario.xlsx");
                }
            }
        }

        private void SetarParametrosDeConsulta(string filtroCodigoUsuario, string filtroNomeUsuario)
        {
            if (!string.IsNullOrEmpty(filtroCodigoUsuario))
            {
                HttpContext.Session.SetString("paramFiltroCodigoUsuario", filtroCodigoUsuario);
            }
            if (!string.IsNullOrEmpty(filtroNomeUsuario))
            {
                HttpContext.Session.SetString("paramFiltroNomeUsuario", filtroNomeUsuario);
            }
        }

        private void RecuperarParametrosConsulta(out string filtroCodigoUsuario, out string filtroNomeUsuario)
        {
            filtroCodigoUsuario = HttpContext.Session.GetString("paramFiltroCodigoUsuario");
            filtroNomeUsuario = HttpContext.Session.GetString("paramFiltroNomeUsuario");
        }
    }
}