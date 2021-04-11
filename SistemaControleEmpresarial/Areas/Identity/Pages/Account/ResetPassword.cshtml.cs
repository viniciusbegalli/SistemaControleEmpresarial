using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaControleEmpresarial.Models;

namespace SistemaControleEmpresarial.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Campo Obrigatório")]
            [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Campo Obrigatório")]
            [StringLength(100, ErrorMessage = "A {0} dever ter no mínio {2} caracteres(letras, números e, se possível, caracteres especiais).", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [RegularExpression(@"^.*(?=.{8,})(?=.*[@#$%^&+=])(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$", ErrorMessage = "Senha fora do Padrão: Utilize no minimo 8 caracteres, sendo no mínimo uma letra maiúscula, uma letra minúscula, um dígito (0..9) e um caracter especial (@#$%^&+=).")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Campo Obrigatório")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirma Senha")]
            [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = code
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
