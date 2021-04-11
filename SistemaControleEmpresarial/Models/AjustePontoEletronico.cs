using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaControleEmpresarial.Models
{
    [Table("AjustePontoEletronicos")]
    public class AjustePontoEletronico
    {
        [Key]
        [Display(Name = "Código")]
        public int CodigoAjuste { get; set; }

        [Display(Name = "Usuario")]
        public string UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Data Ajuste")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime DataAjuste { get; set; }

        [Display(Name = "Primeira Entrada")]
        [DataType(DataType.Time, ErrorMessage = "Hora em formato inválido")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime HoraPrimeiraEntrada { get; set; }

        [Display(Name = "Primeira Saida")]
        [DataType(DataType.Time, ErrorMessage = "Hora em formato inválido")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime HoraPrimeiraSaida { get; set; }

        [Display(Name = "Segunda Entrada")]
        [DataType(DataType.Time, ErrorMessage = "Hora em formato inválido")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime HoraSegundaEntrada { get; set; }

        [Display(Name = "Segunda Saida")]
        [DataType(DataType.Time, ErrorMessage = "Hora em formato inválido")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime HoraSegundaSaida { get; set; }

        [Display(Name = "Justificativa")]
        [StringLength(100, ErrorMessage = "Justificativa não pode exceder 100 caracteres.")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Justificativa { get; set; }

        [Display(Name = "SituacaoAjuste")]
        public string SituacaoAjuste { get; set; }

        [Display(Name = "Observações")]
        [StringLength(100, ErrorMessage = "Observações não pode exceder 100 caracteres.")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Observacoes { get; set; }

        [Display(Name = "CodigoUsuarioAprovador")]
        public int CodigoUsuarioAprovador { get; set; }

    }

    public class AjustePontoEletronicoGrafico
    {
        public string AnoMes { get; set; }
        public int TotalSolicitado { get; set; }
        public int TotalAprovado { get; set; }
        public int TotalReprovado { get; set; }
        public int TotalPendente { get; set; }
    }
}
