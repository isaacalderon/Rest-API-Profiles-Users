using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rest_API_Profiles_Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Profile Profile { get; set; }
        [Required]
        public int IdEmployee { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        [Required]
        public string Login { get; set; }
    }

    public class BasicUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int ProfileId { get; set; }
        [Required]
        public int IdEmployee { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Login { get; set; }
    }
}
