using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.ViewModels
{
    public class UsuarioSignInViewModel
    {
        [Required(ErrorMessage = "El correo electónico es requerido")]
        [EmailAddress(ErrorMessage = "El correo electónico es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
