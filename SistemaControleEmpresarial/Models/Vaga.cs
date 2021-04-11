using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaControleEmpresarial.Models
{
    [Table("Vagas")]
    public class Vaga
    {
        [Key]
        [Display(Name = "Código")]
        public int CodigoVaga { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Titulo { get; set; }

        [Display(Name = "Descricao")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(500, ErrorMessage = "Descrição não pode exceder 500 caracteres.")]
        public string Descricao { get; set; }

        [Display(Name = "Usuário de Criação")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }

        [Display(Name = "Situação")]
        public string SituacaoVaga { get; set; }

        [Display(Name = "Código Usuário Ultima Alteração")]
        public string CodigoUsuarioUltimaAtualizacao { get; set; }

        [Display(Name = "Usuário Ultima Alteração")]
        public string NomeUsuarioUltimaAtualizacao { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data de Última Alteração")]
        public DateTime ? DataHoraUltimaAlteracao { get; set; }
    }
}
