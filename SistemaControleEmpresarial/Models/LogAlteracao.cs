using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaControleEmpresarial.Models;

namespace SistemaControleEmpresarial.Models
{
    [Table("LogAlteracoes")]
    public class LogAlteracao
    {
        [Key]
        [Display(Name = "Código")]
        public int CodigoLogAlteracao { get; set; }

        [Display(Name = "Código Usuario")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }

        [Display(Name = "Tipo de Operação do Log")]
        public string TipoOperacaoLog { get; set; }

        [Display(Name = "Entidade")]
        public string Entidade { get; set; }

        [Display(Name = "CodigoEntidade")]
        public string CodigoEntidade { get; set; }

        public ICollection<LogAlteracaoItem> Items { get; set; }
    }
}
