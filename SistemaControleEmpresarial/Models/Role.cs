﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaControleEmpresarial.Models
{
    public class Role
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
