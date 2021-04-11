using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaControleEmpresarial.Models
{
    [Table("Treinamentos")]
    public class Treinamento
    {
        [Key]
        [Display(Name = "Código")]
        public int CodigoTreinamento { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(50, ErrorMessage = "Título não pode exceder 50 caracteres.")]
        public string Titulo { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(500, ErrorMessage = "Descrição não pode exceder 500 caracteres.")]
        public string Descricao { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Data Início")]
        public DateTime DataInicioTreinamento { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Data Fim")]
        public DateTime DataFimTreinamento { get; set; }

        [DataType(DataType.Time, ErrorMessage = "Hora em formato inválido")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Hora Início")]
        public DateTime HoraInicioTreinamento { get; set; }

        [DataType(DataType.Time, ErrorMessage = "Hora em formato inválido")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Hora Fim")]
        public DateTime HoraFimTreinamento { get; set; }

        [Display(Name = "Usuário de Criação")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }

        [Display(Name = "Situação")]
        public string SituacaoTreinamento { get; set; }

        public ICollection<TreinamentoInstrutor> InstrutoresTreinamento { get; set; }
    }

    public class TreinamentosGrafico
    {
        public string AnoMes { get; set; }
        public int Total { get; set; }
    }
}