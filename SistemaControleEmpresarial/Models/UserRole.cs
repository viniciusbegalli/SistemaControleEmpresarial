using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaControleEmpresarial.Models
{

    public class UserRole
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }

    public class UsuariosRolesGrafico
    {
        public string Perfil { get; set; }
        public int Total { get; set; }
    }
}
