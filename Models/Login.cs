using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Turnos.Models
{
    public class Login
    {           
        [Key]   
        public int LoginId { get; set; }
        [Required (ErrorMessage = "Debe ingresar un usuario")]
        [Display (Prompt = "Ingrese una descripción")]                
        public string Usuario { get; set; }
        [Required (ErrorMessage = "Debe ingresar una contraseña")]  
        [Display (Prompt = "Ingrese una contraseña")]                
        public string Password { get; set; }  
    }
}