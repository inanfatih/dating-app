using System;
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
        [Required]
        public string Gender { get; set; }
        [Required]
        public string KnownAs { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        // Created ve LastActive alanlarini default olarak su an olarak cekebilmek icin constructor olusturup oradan cektik
        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}