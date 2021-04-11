using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaControleEmpresarial.Models
{
    [Table("SolicitacaoJornadas")]
    public class SolicitacaoJornada
    {
        [Key]
        [Display(Name = "Código")]
        public int CodigoSolicitacaoJornada { get; set; }

        [Display(Name = "Feriado")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int CodigoFeriado { get; set; }
        [ForeignKey("CodigoFeriado")]
        public Feriado Feriado { get; set; }

        [Display(Name = "Usuário de Criação")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }

        [Display(Name = "Código Usuário Jornada")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int CodigoUsuarioJornada { get; set; }

        [Display(Name = "Usuário Jornada")]
        public string NomeUsuarioJornada { get; set; }

        [Display(Name = "Justificativa")]
        [StringLength(100, ErrorMessage = "Justificativa não pode exceder 100 caracteres.")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Justificativa { get; set; }

        [Display(Name = "Situação da Solicitação")]
        public string SituacaoSolicitacao { get; set; }

        [Display(Name = "Observações")]
        [StringLength(100, ErrorMessage = "Campo Observações não pode exceder 100 caracteres.")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Observacoes { get; set; }

        [Display(Name = "CodigoUsuarioAprovador")]
        public int CodigoUsuarioAprovador { get; set; }
    }
}
