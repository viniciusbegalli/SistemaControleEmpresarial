using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaControleEmpresarial.Models
{
    [Table("VagaCandidatos")]
    public class VagaCandidato
    {
        [Key]
        [Display(Name = "Código")]
        public int CodigoVagaCandidato { get; set; }

        [Display(Name = "Vaga")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int CodigoVaga { get; set; }
        [ForeignKey("CodigoVaga")]
        public Vaga Vaga { get; set; }

        [Display(Name = "Nome do Candidato")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(50, ErrorMessage = "Nome não pode exceder 50 caracteres.")]
        public string NomeCandidato { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "O email não está num formato válido.")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Email { get; set; }

        [Display(Name = "LinkedIn")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(50, ErrorMessage = "Link não pode exceder 50 caracteres.")]
        public string LinkedIn { get; set; }

        [Display(Name = "Observações")]
        [StringLength(100, ErrorMessage = "Campo Observações não pode exceder 100 caracteres.")]
        public string Observacoes { get; set; }
    }
}