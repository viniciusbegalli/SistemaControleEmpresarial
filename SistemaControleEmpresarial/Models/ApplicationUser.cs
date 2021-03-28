using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SistemaControleEmpresarial.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NomeUsuario { get; set; }

        public string CPF { get; set; }

        public string Telefone { get; set; }
        /*
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }*/
    }
}
