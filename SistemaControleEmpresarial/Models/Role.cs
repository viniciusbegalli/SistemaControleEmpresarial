using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaControleEmpresarial.Models
{
    public class Role
    {
        [Display(Name = "Role")]
        [Required(ErrorMessage = "O nome do perfil é obrigatório")]
        public string RoleName { get; set; }
    }
}
