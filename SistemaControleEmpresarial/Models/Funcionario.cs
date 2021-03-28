using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SistemaControleEmpresarial.Models
{
    [Table("Funcionarios")]
    public class Funcionario
    {
        [Key]
        [Display(Name = "Código")]
        public int CodigoFuncionario { get; set; }

        [Display(Name = "Código Usuario")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string CodigoUsuario { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string NomeUsuario { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Sexo { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        //[RegularExpression("/^(([0-9]{3}.?[0-9]{3}.?[0-9]{3}-?[0-9]{2})|([0-9]{11}))$/", ErrorMessage = "CPF inválido.")]
        public string CPF { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
    }

    public class FuncionarioBusca
    {
        //[LookupColumn]
        [Display(Name = "CodigoFuncionario")]
        public int? CodigoFuncionario { get; set; }

        [Display(Name = "Nome")]
        public string NomeUsuario { get; set; }

        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [Display(Name = "CPF")]
        public string CPF { get; set; }
    }
}