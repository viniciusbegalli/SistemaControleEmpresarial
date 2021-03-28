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

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Descricao { get; set; }

        [Display(Name = "SituacaoAjuste")]
        public string SituacaoAjuste { get; set; }

        [Display(Name = "Observações")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Observacoes { get; set; }

    }
}
