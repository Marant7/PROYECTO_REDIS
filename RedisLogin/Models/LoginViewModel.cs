using System.ComponentModel.DataAnnotations;

namespace RedisLogin.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}