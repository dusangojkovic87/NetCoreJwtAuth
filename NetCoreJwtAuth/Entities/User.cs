
using System.ComponentModel.DataAnnotations;


namespace NetCoreJwtAuth.Entities
{
    public class User
    {
        public User()
        {
            Role = "Customer";

        }


        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

    }
}