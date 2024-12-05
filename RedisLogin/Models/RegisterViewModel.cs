using System.ComponentModel.DataAnnotations;

namespace RedisLogin.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmación de contraseña es requerida")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
    }
}