using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaControleEmpresarial.Models
{
    [Table("PontoEletronicos")]
    public class PontoEletronico
    {
        [Key]
        [Display(Name = "Código")]
        public int CodigoPontoEletronico { get; set; }

        [Display(Name = "Código Usuario")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Primeira Entrada")]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DataHoraPrimeiraEntrada { get; set; }

        [Display(Name = "Primeira Saida")]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DataHoraPrimeiraSaida { get; set; }

        [Display(Name = "Segunda Entrada")]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DataHoraSegundaEntrada { get; set; }

        [Display(Name = "Segunda Saida")]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DataHoraSegundaSaida { get; set; }

        public string Data
        {
            get { return DataHoraPrimeiraEntrada?.ToShortDateString(); }
        }

        public string HoraPrimeiraEntrada { get { return DataHoraPrimeiraEntrada?.ToShortTimeString(); } }
        public string HoraPrimeiraSaida { get { return DataHoraPrimeiraSaida?.ToShortTimeString(); } }
        public string HoraSegundaEntrada { get { return DataHoraSegundaEntrada?.ToShortTimeString(); } }
        public string HoraSegundaSaida { get { return DataHoraSegundaSaida?.ToShortTimeString(); } }
    }
}
