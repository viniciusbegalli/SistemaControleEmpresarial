using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaControleEmpresarial.Data;
using SistemaControleEmpresarial.Models;
using SistemaControleEmpresarial.Services;

namespace SistemaControleEmpresarial.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender, ApplicationDbContext context)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Campo Obrigatório")]
            [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user4 = _context.Users.FirstOrDefault(u => u.Email == Input.Email);

                var pontoEletronicos = from p in _context.Users
                                       select p;

                int contador = pontoEletronicos.Count();
                pontoEletronicos = pontoEletronicos.Where(u => u.Email == Input.Email);


                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Reset Password",
                    $"Por favor altere sua senha no seguinte link: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Clique aqui</a>.");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
