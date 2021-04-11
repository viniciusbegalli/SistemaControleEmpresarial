using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaControleEmpresarial.Models
{
    [Table("LogAlteracaoItems")]
    public class LogAlteracaoItem
    {
        [Key]
        [Display(Name = "Código")]
        public int CodigoLogAlteracaoItem { get; set; }

        [Display(Name = "CodigoLogAlteracao")]
        public int CodigoLogAlteracao { get; set; }
        [ForeignKey("CodigoLogAlteracao")]
        public LogAlteracao LogAlteracao { get; set; }

        [Display(Name = "Objeto")]
        public string Objeto { get; set; }

        [Display(Name = "Valor Antigo")]
        public string ValorAntigo { get; set; }

        [Display(Name = "Valor Novo")]
        public string ValorNovo { get; set; }
    }
}
