using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaControleEmpresarial.Models
{
    [Table("TreinamentoInstrutores")]
    public class TreinamentoInstrutor
    {
        [Key]
        [Display(Name = "Código")]
        public int CodigoTreinamentoInstrutor { get; set; }

        [Display(Name = "Treinamento")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int CodigoTreinamento { get; set; }
        [ForeignKey("CodigoTreinamento")]
        public Treinamento Treinamento { get; set; }

        [Display(Name = "Instrutor")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Instrutor { get; set; }

        [Display(Name = "CodigoExterno")]
        public int CodigoExterno { get; set; }

        [Display(Name = "NomeInstrutor")]
        public string NomeInstrutor { get; set; }
    }
}
