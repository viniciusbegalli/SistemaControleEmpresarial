using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaControleEmpresarial.Models
{
    [Table("Chamados")]
    public class Chamado
    {
        [Key]
        [Display(Name = "Código")]
        public int CodigoChamado { get; set; }

        [Display(Name = "Usuário de Criação")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Titulo { get; set; }

        [Display(Name = "Descricao")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, ErrorMessage = "Descrição não pode exceder 100 caracteres.")]
        public string Descricao { get; set; }

        [Display(Name = "Situação")]
        public string SituacaoChamado { get; set; }

        [Display(Name = "Fila")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Fila { get; set; }

        [Display(Name = "IDPrioridade")]
        public int IDPrioridade { get; set; }

        [Display(Name = "Prioridade")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Prioridade { get; set; }

        public ICollection<ChamadoNota> Notas { get; set; }
    }


    public class ChamadosGrafico
    {
        public string AnoMes { get; set; }
        public int TotalAberto { get; set; }
        public int TotalFechado { get; set; }
    }
}
