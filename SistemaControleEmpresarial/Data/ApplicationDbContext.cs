using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaControleEmpresarial.Models;

namespace SistemaControleEmpresarial.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Funcionario> Funcionario { get; set; }

        public DbSet<SistemaControleEmpresarial.Models.PontoEletronico> PontoEletronico { get; set; }

        public DbSet<SistemaControleEmpresarial.Models.AjustePontoEletronico> AjustePontoEletronico { get; set; }
    }
}
