using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaControleEmpresarial.Models
{
    [Table("ChamadoNotas")]
    public class ChamadoNota
    {
        [Key]
        [Display(Name = "Código")]
        public int CodigoChamadoNota { get; set; }

        [Display(Name = "Chamado")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int CodigoChamado { get; set; }
        [ForeignKey("CodigoChamado")]
        public Chamado Chamado { get; set; }

        [Display(Name = "Usuário")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data")]
        public DateTime DataNota { get; set; }

        [Display(Name = "Nota")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, ErrorMessage = "Descrição não pode exceder 100 caracteres.")]
        public string DescricaoNota { get; set; }

        [Display(Name = "CodigoExterno")]
        public int CodigoExterno { get; set; }

        [Display(Name = "NomeUsuarioNota")]
        public string NomeUsuarioNota { get; set; }
    }
}
