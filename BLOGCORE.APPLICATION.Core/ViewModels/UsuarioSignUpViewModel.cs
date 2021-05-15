using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.ViewModels
{
    public class UsuarioSignUpViewModel
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Username { get; set; }


        [Required(ErrorMessage = "El correo electónico es requerido")]
        [EmailAddress(ErrorMessage = "El correo electónico es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Compare("Password", ErrorMessage = "La contraseña no coincide")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
