using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.Roles
{
    public partial class CatRoles
    {
        
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese el Rol.")]
        [Display(Name = "Rol")]
        public string Rol { get; set; }  
     
    }
}