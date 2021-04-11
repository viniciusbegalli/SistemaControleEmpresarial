using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaControleEmpresarial.Models
{
    [Table("TreinamentoParticipantes")]
    public class TreinamentoParticipante
    {
        [Key]
        [Display(Name = "Código")]
        public int CodigoTreinamentoParticipante { get; set; }

        [Display(Name = "Treinamento")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int CodigoTreinamento { get; set; }
        [ForeignKey("CodigoTreinamento")]
        public Treinamento Treinamento { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(50, ErrorMessage = "Nome não pode exceder 50 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "O email não está num formato válido.")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Email { get; set; }

    }
}
