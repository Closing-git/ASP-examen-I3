using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Auth
{
    public class LoginForm
    {
        [DisplayName("E-mail : ")]
        [EmailAddress(ErrorMessage = "E-mail non valide.")]
        [Required(ErrorMessage = "E-mail obligatoire.")]
        public string Email { get; set; }
        [DisplayName("Mot de passe : ")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mot de passe obligatoire.")]
        [MaxLength(32, ErrorMessage = "Le mot de passe doit faire au moins 32 caractères.")]
        public string Password { get; set; }
    }
}
