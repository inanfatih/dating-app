using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        // Asagidaki validation larin calismasi icin Controller'da [ApiController] olmak zorunda.  ya da manuel olarak "ModelState.IsValid" ile validation yapilmasi gerekiyor controller'da. Yoksa validation calismaz
        [Required]
        public string Username { get; set; }

        // [EmailAddress]
        // [Phone]
        // [MaxLength]
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters")]
        public string Password { get; set; }
    }
}