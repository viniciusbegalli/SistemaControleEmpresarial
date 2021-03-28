using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SistemaControleEmpresarial.Models;

namespace SistemaControleEmpresarial.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Campo Obrigatório")]
            [Display(Name = "Nome")]
            public string NomeUsuario { get; set; }

            [Display(Name = "CPF")]
            [Required(ErrorMessage = "Campo Obrigatório")]
            //[RegularExpression("/^(([0-9]{3}.?[0-9]{3}.?[0-9]{3}-?[0-9]{2})|([0-9]{11}))$/", ErrorMessage = "CPF inválido.")]
            public string CPF { get; set; }

            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Telefone")]
            public string Telefone { get; set; }

            [Required(ErrorMessage = "Campo Obrigatório")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Campo Obrigatório")]
            [StringLength(100, ErrorMessage = "A {0} dever ter no mínio {2} caracteres(letras, números e, se possível, caracteres especiais).", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            [RegularExpression(@"^.*(?=.{8,})(?=.*[@#$%^&+=])(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$", ErrorMessage = "Senha fora do Padrão: Utilize no minimo 8 caracteres, sendo no mínimo uma letra maiúscula, uma letra minúscula, um dígito (0..9) e um caracter especial (@#$%^&+=).")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Campo Obrigatório")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirma Senha")]
            [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email,
                                                 Email = Input.Email,
                                                 NomeUsuario = Input.NomeUsuario,
                                                 CPF = Input.CPF,
                                                 Telefone = Input.Telefone };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                TempData["msgLogin"] = "<script>alert('Este e-mail já está sendo utilizado!');</script>";
                return Redirect("Login");
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
