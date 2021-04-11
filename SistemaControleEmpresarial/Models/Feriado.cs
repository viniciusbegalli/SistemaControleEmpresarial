using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaControleEmpresarial.Models;

namespace SistemaControleEmpresarial.Models
{
    [Table("Feriados")]
    public class Feriado
    {
        [Key]
        [Display(Name = "Código")]
        public int CodigoFeriado { get; set; }

        [Display(Name = "Feriado")]
        [StringLength(30, ErrorMessage = "Descrição do Feriado não pode exceder 30 caracteres.")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string DescricaoFeriado { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime Data { get; set; }

        [Display(Name = "Usuário de Criação")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }

        [Display(Name = "Código Usuário Ultima Alteração")]
        public string CodigoUsuarioUltimaAtualizacao { get; set; }

        [Display(Name = "Usuário Ultima Alteração")]
        public string NomeUsuarioUltimaAtualizacao { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data de Última Alteração")]
        public DateTime DataHoraUltimaAlteracao { get; set; }
    }

    public class FeriadosGrafico
    {
        public string AnoMes { get; set; }
        public int TotalFeriados { get; set; }
        public int TotalJornadas { get; set; }
    }
}
