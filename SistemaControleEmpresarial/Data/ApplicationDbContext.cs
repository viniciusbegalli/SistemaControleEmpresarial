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

        public DbSet<PontoEletronico> PontoEletronico { get; set; }

        public DbSet<AjustePontoEletronico> AjustePontoEletronico { get; set; }

        public DbSet<Feriado> Feriado { get; set; }
        public DbSet<LogAlteracao> LogAlteracao { get; set; }
        public DbSet<LogAlteracaoItem> LogAlteracaoItem { get; set; }
        public DbSet<SolicitacaoJornada> SolicitacaoJornada { get; set; }
        public DbSet<Treinamento> Treinamento { get; set; }
        public DbSet<TreinamentoInstrutor> TreinamentoInstrutor { get; set; }
        public DbSet<Vaga> Vaga { get; set; }
        public DbSet<VagaCandidato> VagaCandidato { get; set; }
        public DbSet<Chamado> Chamado { get; set; }
        public DbSet<TreinamentoParticipante> TreinamentoParticipante { get; set; }
    }
}
