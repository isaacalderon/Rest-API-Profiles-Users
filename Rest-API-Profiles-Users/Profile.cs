using System.ComponentModel.DataAnnotations;

namespace Rest_API_Profiles_Users
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
